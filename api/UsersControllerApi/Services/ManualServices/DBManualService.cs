using MySql.Data.MySqlClient;
using BaseProjectApi.Models;
using System;
using System.Data;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace BaseProjectApi.Services.ManualServices
{
    public class DBManualService : IDBManualService
    {
        private readonly string _connectionString;
        private ServiceModel _result;
        private string _sql;

        public DBManualService(IConfiguration configuration)
        {
            _connectionString = configuration.GetValue<string>("ConnectionStrings:DefaultConnection");
            _result = new ServiceModel();
        }

        public async Task<ServiceModel> SqlCommand(string sql)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        _result.Code = 200;
                        _result.Status = true;
                        _result.Message = "SqlCommand() Completed Success";
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                _result.Code = 500;
                _result.Status = false;
                _result.Message = "SqlCommand() Exception: " + ex.Message;
            }
            return _result;
        }

        // New overload that accepts parameters
        public async Task<ServiceModel> SqlCommand(string sql, params MySqlParameter[] parameters)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {
                        if (parameters != null && parameters.Length > 0)
                        {
                            command.Parameters.AddRange(parameters);
                        }
                        connection.Open();
                        command.ExecuteNonQuery();
                        _result.Code = 200;
                        _result.Status = true;
                        _result.Message = "SqlCommand() Completed Success";
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                _result.Code = 500;
                _result.Status = false;
                _result.Message = "SqlCommand() Exception: " + ex.Message;
            }
            return _result;
        }

        public async Task<DBServiceModel<MySqlDataReader>> SqlFecthCommand(string sql)
        {
            DBServiceModel<MySqlDataReader> dbServiceModel = new DBServiceModel<MySqlDataReader>();
            try
            {
                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {
                        connection.Open();
                        dbServiceModel.Payload = await command.ExecuteReaderAsync(CommandBehavior.CloseConnection);
                    }
                }
                dbServiceModel.Code = 200;
                dbServiceModel.Status = true;
                dbServiceModel.Message = "DataFetch Completed Success";
            }
            catch (Exception ex)
            {
                dbServiceModel.Code = 500;
                dbServiceModel.Status = false;
                dbServiceModel.Message = "SqlFecthCommand() Exception: " + ex.Message;
            }
            return dbServiceModel;
        }

        public async Task<DBServiceModel<MySqlDataReader>> SqlFecthCommand(string sql, params MySqlParameter[] parameters)
        {
            DBServiceModel<MySqlDataReader> dbServiceModel = new DBServiceModel<MySqlDataReader>();
            try
            {
                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {
                        if (parameters != null && parameters.Length > 0)
                        {
                            command.Parameters.AddRange(parameters);
                        }
                        connection.Open();
                        dbServiceModel.Payload = await command.ExecuteReaderAsync(CommandBehavior.CloseConnection);
                    }
                }
                dbServiceModel.Code = 200;
                dbServiceModel.Status = true;
                dbServiceModel.Message = "DataFetch Completed Success";
            }
            catch (Exception ex)
            {
                dbServiceModel.Code = 500;
                dbServiceModel.Status = false;
                dbServiceModel.Message = "SqlFecthCommand() Exception: " + ex.Message;
            }
            return dbServiceModel;
        }

        public async Task<ServiceModel> CheckIfUserIdExist(string userId)
        {
            try
            {
                _sql = "SELECT * FROM users WHERE UserId = @UserId";
                var checkRes = await SqlFecthCommand(_sql, new MySqlParameter("@UserId", userId));
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

        public async Task<ServiceModel> CheckIfUserNameExist(string userName)
        {
            try
            {
                _sql = "SELECT * FROM users WHERE UserName = @UserName";
                var checkRes = await SqlFecthCommand(_sql, new MySqlParameter("@UserName", userName));
                var readerObj = checkRes.Payload;
                if (readerObj.HasRows)
                {
                    _result.Code = 500;
                    _result.Status = false;
                    _result.Message = $"UserName '{userName}' already exists.";
                    _result.Payload = null;
                }
                else
                {
                    _result.Code = 200;
                    _result.Status = true;
                    _result.Message = $"UserName '{userName}' does not exist.";
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
    }
}
