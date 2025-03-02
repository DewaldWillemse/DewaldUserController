using BaseProjectApi.Models;

namespace BaseProjectApi.Services.EncrytionService
{
    public interface IEncryptionService
    {
        Task<ServiceModel> EncryptToken(UsersProfile usrm);
        Task<ServiceModel> DecryptToken(UsersModel dbusrm);
        Task GenerateKeys();
    }
}
