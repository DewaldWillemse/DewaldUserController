using MySql.Data.MySqlClient;  // Ensure this is used for MySQL connection
using BaseProjectApi.Models;
using BaseProjectApi.Services.ManualServices;
using System;
using System.Threading.Tasks;

namespace BaseProjectApi.Services.UserService
{
    public class DBUsersChecks : IDBUsersChecks
    {
        private readonly IDBManualService _dbms;
        private ServiceModel _result;
        private string? _sql;

        public DBUsersChecks(IDBManualService dbms)
        {
            _dbms = dbms;
            _result = new ServiceModel();
        }

        // Check if a UserId exists in the database
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
                    _result.Message = $"UserId '{userId}' already exists.";
                    _result.Payload = null;
                }
                else
                {
                    _result.Code = 200;
                    _result.Status = true;
                    _result.Message = $"UserId '{userId}' does not exist.";
                    _result.Payload = null;
                }

                readerObj.Close();
            }
            catch (Exception ex)
            {
                _result.Code = 500;
                _result.Status = false;
                _result.Message = "CheckIfUserIdExist() Exception: " + ex.Message;
            }

            return _result;
        }

        // Check if a UserName exists in the database
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
                    _result.Message = $"UserName '{UserName}' already exists.";
                    _result.Payload = null;
                }
                else
                {
                    _result.Code = 200;
                    _result.Status = true;
                    _result.Message = $"UserName '{UserName}' does not exist.";
                    _result.Payload = null;
                }

                readerObj.Close();
            }
            catch (Exception ex)
            {
                _result.Code = 500;
                _result.Status = false;
                _result.Message = "CheckIfUserNameExist() Exception: " + ex.Message;
            }

            return _result;
        }

        // Check if the username is duplicated during an update operation
        public async Task<ServiceModel> CheckUsernameOnUpdateDuplicate(UsersModel usrm)
        {
            try
            {
                _sql = $"SELECT * FROM users WHERE UserName = '{usrm.UserName}' AND UserId != '{usrm.UserId}'";
                var checkRes = await _dbms.SqlFecthCommand(_sql);

                var readerObj = checkRes.Payload;
                if (readerObj.HasRows)
                {
                    _result.Code = 500;
                    _result.Status = false;
                    _result.Message = $"UserName '{usrm.UserName}' is already taken by another user.";
                    _result.Payload = null;
                }
                else
                {
                    _result.Code = 200;
                    _result.Status = true;
                    _result.Message = $"UserName '{usrm.UserName}' is available for update.";
                    _result.Payload = null;
                }

                readerObj.Close();
            }
            catch (Exception ex)
            {
                _result.Code = 500;
                _result.Status = false;
                _result.Message = "CheckUsernameOnUpdateDuplicate() Exception: " + ex.Message;
            }

            return _result;
        }

        // Get the total number of users in the database
        public async Task<ServiceModel> GetUserTotalCount()
        {
            try
            {
                _sql = "SELECT COUNT(*) FROM users"; // Query to get total user count
                var checkRes = await _dbms.SqlFecthCommand(_sql);

                var readerObj = checkRes.Payload;
                int totalCount = 0;

                if (readerObj.HasRows)
                {
                    await readerObj.ReadAsync();
                    totalCount = readerObj.GetInt32(0); // Get the count from the query result
                }

                _result.Code = 200;
                _result.Status = true;
                _result.Message = "Total user count fetched successfully.";
                _result.Payload = totalCount;

                readerObj.Close();
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
