using BaseProjectApi.Models;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;

namespace BaseProjectApi.Services.EncrytionService
{
    public class EncryptionService : IEncryptionService
    {
        public ServiceModel _result;
        public EncryptionClass _ecm;
        private readonly IConfiguration _configuration;
        private readonly string _passphrase;
        private readonly string _epoc;
        public EncryptionService(IConfiguration configuration)
        {
            _configuration = configuration;
            _result = new ServiceModel();
            _ecm = new EncryptionClass();
            _passphrase = _configuration.GetValue<string>("UrlsLinks:PrimaryLinkSection")!;
        }

        public async Task<ServiceModel> EncryptToken(UsersProfile usrm)
        {
            try
            {
                _ecm.epocString = usrm.UserId.Replace("USR", "");
                GenerateKeys();

                if(!_result.Status)
                {
                    return _result;
                }

                var jsonString = JsonConvert.SerializeObject(usrm);

                using (Aes aesAlg = Aes.Create())
                {
                    aesAlg.Key = _ecm.key;
                    aesAlg.IV = _ecm.iv;

                    ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                    using (MemoryStream msEncrypt = new MemoryStream())
                    {
                        using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                        {
                            using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                            {
                                swEncrypt.Write(jsonString);
                            }
                            _result.Payload = Convert.ToBase64String(msEncrypt.ToArray());
                        }

                        _result.Code = 200;
                        _result.Status = true;
                        _result.Message = "Token Generation Success";

                    }
                }
            }
            catch (Exception ex)
            {
                _result.Code = 500;
                _result.Status = false;
                _result.Message = "EncryptionService() Exception: " + ex.Message;
            }

            return _result;

        }

        public async Task<ServiceModel> DecryptToken(UsersModel dbusrm)
        {
            try
            {
                _ecm.epocString = dbusrm.UserId.Replace("USR", "");
                GenerateKeys();

                if (!_result.Status)
                {
                    return _result;
                }

                using (Aes aesAlg = Aes.Create())
                {
                    aesAlg.Key = _ecm.key;
                    aesAlg.IV = _ecm.iv;

                    ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                    using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(dbusrm.UserToken)))
                    {
                        using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                            {
                                _result.Payload = srDecrypt.ReadToEnd();
                            }
                        }
                    }
                    _result.Code = 200;
                    _result.Status = true;
                    _result.Message = "Token Descryption Success";
                }
            }
            catch (Exception ex)
            {
                _result.Code = 500;
                _result.Status = false;
                _result.Message = "DecryptToken() Exception: " + ex.Message;
            }

            return _result;
        }

        public async Task GenerateKeys()
        {
            try
            {
                string combinedString = _ecm.epocString + _passphrase;
                using (SHA256 sha256 = SHA256.Create())
                {
                    byte[] hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(combinedString));
                    _ecm.key = new byte[32];
                    _ecm.iv = new byte[16];
                    Array.Copy(hash, 0, _ecm.key, 0, 32);
                    Array.Copy(hash, 0, _ecm.iv, 0, 16);

                    _result.Code = 200;
                    _result.Status = true;
                    _result.Message = "key Generation Success";
                }
            }
            catch (Exception ex)
            {
                _result.Code = 500;
                _result.Status = false;
                _result.Message = "GenerateKeys() Exception: " + ex.Message;
            }

        }
    }
}
