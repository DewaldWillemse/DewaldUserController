using BaseProjectApi.Models;
using BaseProjectApi.Services.ManualServices;

namespace BaseProjectApi.Services.UserService
{
    public class DBUsersChecks : IDBUsersChecks
    {
        private ServiceModel _result;
        private readonly IDBManualService _dbms;
        private string _sql;
        public DBUsersChecks(IDBManualService dbms)
        {
            _result = new ServiceModel();
            _dbms = dbms;
        }

        public async Task<ServiceModel> CheckIfUserIdExist(string userId)
        {
            try
            {
                _sql = $"SELECT * FROM users WHERE UserId = '{userId}'";
                var checkRes = await _dbms.SqlFecthCommand(_sql);

                var readerObj = checkRes.Payload;
                if (readerObj.HasRows)
                {
                    _result.Code = 500;
                    _result.Status = false;
                    _result.Message = $"CheckIfUserIdExist() UserId: {userId} already exist in the table, please check it we can't have a duplicate.";
                    _result.Payload = null;
                    return _result;
                }

                readerObj.Close();

                _result.Code = 200;
                _result.Status = true;
                _result.Message = $"CheckIfUserIdExist() UserId: {userId} does not exist in the table - Proceed1";
                _result.Payload = null;

            }
            catch (Exception ex)
            {
                _result.Code = 500;
                _result.Status = false;
                _result.Message = "AddNewShiptoDatabase() Exception: " + ex.Message;

            }

            return _result;
        }

        public async Task<ServiceModel> CheckIfUserNameExist(string UserName)
        {
            try
            {
                _sql = $"SELECT * FROM users WHERE UserName = '{UserName}'";
                var checkRes = await _dbms.SqlFecthCommand(_sql);

                var readerObj = checkRes.Payload;
                if (readerObj.HasRows)
                {
                    _result.Code = 500;
                    _result.Status = false;
                    _result.Message = $"CheckIfUserNameExist() UserName: {UserName} already exist in the table, please check it we can't have a duplicate.";
                    _result.Payload = null;
                    return _result;
                }

                readerObj.Close();

                _result.Code = 200;
                _result.Status = true;
                _result.Message = $"CheckIfUserNameExist() UserName: {UserName} does not exist in the table - Proceed2";
                _result.Payload = null;

            }
            catch (Exception ex)
            {
                _result.Code = 500;
                _result.Status = false;
                _result.Message = "CheckIfUserNameExist() Exception: " + ex.Message;

            }

            return _result;
        }

        public async Task<ServiceModel> CheckUsernameOnUpdateDuplicate(UsersModel usrm)
        {
            try
            {
                _sql = $"SELECT * FROM users";
                var checkRes = await _dbms.SqlFecthCommand(_sql);

                var readerObj = checkRes.Payload;
                if (readerObj.HasRows)
                {
                    while (await readerObj.ReadAsync())
                    {
                        var id = readerObj.GetInt32(readerObj.GetOrdinal("id"));
                        var userId = readerObj.GetString(readerObj.GetOrdinal("UserId"));
                        var userName = readerObj.GetString(readerObj.GetOrdinal("UserName"));

                        if (id != usrm.id)
                        {
                            continue;
                        }

                        if (userName == usrm.UserName )
                        {
                            _result.Code = 500;
                            _result.Status = false;
                            _result.Message = $"CheckUsernameOnUpdateDuplicate() UserName: {usrm.UserName} already exist in the table, please check it we can't have a duplicate.";
                            _result.Payload = null;
                            return _result;
                        }
                    }                    
                }

                readerObj.Close();

                _result.Code = 200;
                _result.Status = true;
                _result.Message = $"CheckUsernameOnUpdateDuplicate() UserName: {usrm.UserName} does not exist in the table - Proceed";
                _result.Payload = null;

            }
            catch (Exception ex)
            {
                _result.Code = 500;
                _result.Status = false;
                _result.Message = "CheckUsernameOnUpdateDuplicate() Exception: " + ex.Message;

            }

            return _result;
        }

        public async Task<ServiceModel> GetUserTotalCount()
        {
            try
            {
                _sql = $"SELECT COUNT(*) AS theCount FROM users";
                var checkRes = await _dbms.SqlFecthCommand(_sql);

                var readerObj = checkRes.Payload;
                if (readerObj.HasRows)
                {
                    await readerObj.ReadAsync();
                    _result.Payload = readerObj.GetInt32(readerObj.GetOrdinal("theCount"));
                }

                readerObj.Close();

                _result.Code = 200;
                _result.Status = true;
                _result.Message = $"GetUserTotalCount() Success";

            }
            catch (Exception ex)
            {
                _result.Code = 500;
                _result.Status = false;
                _result.Message = "GetUserTotalCount() Exception: " + ex.Message;

            }

            return _result;
        }

    }
}
