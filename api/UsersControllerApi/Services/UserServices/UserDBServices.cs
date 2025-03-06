using MySql.Data.MySqlClient;  // MySQL Client for database connection
using BaseProjectApi.Models;
using BaseProjectApi.Services.ManualServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace BaseProjectApi.Services.UserServices
{
    public class UserDBServices : IUserDBServices
    {
        private readonly string? _connectionString;
        private readonly IDBManualService _dbms;
        private ServiceModel _result;
        private string? _sql;

        public UserDBServices(IConfiguration configuration, IDBManualService dbms)
        {
            _connectionString = configuration.GetValue<string>("ConnectionStrings:DefaultConnection");
            _dbms = dbms;
            _result = new ServiceModel();
        }
    // Update user details
public async Task<ServiceModel> UpdateUser(UsersModel usrm)
{
    _result = new ServiceModel();

    try
    {
        if (usrm == null || string.IsNullOrEmpty(usrm.UserId))
        {
            _result.Code = 400;
            _result.Status = false;
            _result.Message = "Invalid user data. UserId cannot be null.";
            return _result;
        }

        // SQL query to update user details
        _sql = "UPDATE users SET UserName = @UserName WHERE UserId = @UserId";

        // Execute query using parameterized SQL
        var checkRes = await _dbms.SqlFecthCommand(_sql, new MySqlParameter[] {
            new MySqlParameter("@UserName", usrm.UserName),
            new MySqlParameter("@UserId", usrm.UserId)
        });

        if (checkRes.Code == 200)
        {
            _result.Code = 200;
            _result.Status = true;
            _result.Message = "User successfully updated.";
        }
        else
        {
            _result.Code = 500;
            _result.Status = false;
            _result.Message = "Error updating user.";
        }
    }
    catch (Exception ex)
    {
        _result.Code = 500;
        _result.Status = false;
        _result.Message = "UpdateUser() Exception: " + ex.Message;
    }

    return _result;
}

        // Check if the UserName exists
        public async Task<ServiceModel> CheckIfUserNameExist(UsersModel usrm)
        {
            try
            {
                _sql = "SELECT * FROM users WHERE UserName = @UserName";
                var checkRes = await _dbms.SqlFecthCommand(_sql, new MySqlParameter("@UserName", usrm.UserName));
                var readerObj = checkRes.Payload;
                if (readerObj.HasRows)
                {
                    _result.Code = 500;
                    _result.Status = false;
                    _result.Message = $"UserName '{usrm.UserName}' already exists.";
                    _result.Payload = null;
                }
                else
                {
                    _result.Code = 200;
                    _result.Status = true;
                    _result.Message = $"UserName '{usrm.UserName}' does not exist.";
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

        // Register a new user
        public async Task<ServiceModel> RegisterUser(UsersModel usrm, UsersProfile usrp)
        {
            try
            {
                _result = await _dbms.CheckIfUserIdExist(usrm.UserId);
                if (!_result.Status) return _result;

                _result = await _dbms.CheckIfUserNameExist(usrm.UserName);
                if (!_result.Status) return _result;

                _sql = "INSERT INTO users (UserId, UserName, DateTouched) VALUES (@UserId, @UserName, NOW())";
                var checkRes = await _dbms.SqlFecthCommand(_sql, 
                    new MySqlParameter("@UserId", usrm.UserId),
                    new MySqlParameter("@UserName", usrm.UserName));

                if (checkRes.Code == 200)
                {
                    _result.Code = 200;
                    _result.Status = true;
                    _result.Message = "User successfully registered.";
                }
                else
                {
                    _result.Code = 500;
                    _result.Status = false;
                    _result.Message = "Error registering user.";
                }
            }
            catch (Exception ex)
            {
                _result.Code = 500;
                _result.Status = false;
                _result.Message = "RegisterUser() Exception: " + ex.Message;
            }
            return _result;
        }

        // User login based on username
        public async Task<ServiceModel> UserLogin(string UserName)
        {
            try
            {
                _sql = "SELECT * FROM users WHERE UserName = @UserName";
                var checkRes = await _dbms.SqlFecthCommand(_sql, new MySqlParameter("@UserName", UserName));
                var readerObj = checkRes.Payload;
                if (readerObj.HasRows)
                {
                    _result.Code = 200;
                    _result.Status = true;
                    _result.Message = "User found!";
                }
                else
                {
                    _result.Code = 404;
                    _result.Status = false;
                    _result.Message = "User not found!";
                }
                readerObj.Close();
            }
            catch (Exception ex)
            {
                _result.Code = 500;
                _result.Status = false;
                _result.Message = "UserLogin() Exception: " + ex.Message;
            }
            return _result;
        }

        // Get a single user by UserId
        public async Task<ServiceModel> GetSingleUser(string UserId)
        {
            try
            {
                _sql = "SELECT * FROM users WHERE UserId = @UserId";
                var checkRes = await _dbms.SqlFecthCommand(_sql, new MySqlParameter("@UserId", UserId));
                var readerObj = checkRes.Payload;
                if (readerObj.HasRows)
                {
                    _result.Code = 200;
                    _result.Status = true;
                    _result.Message = "User found!";
                    while (await readerObj.ReadAsync())
                    {
                        var user = new UsersModel
                        {
                            UserId = readerObj.GetString(readerObj.GetOrdinal("UserId")),
                            UserName = readerObj.GetString(readerObj.GetOrdinal("UserName")),
                            DateTouched = readerObj.GetDateTime(readerObj.GetOrdinal("DateTouched"))
                        };
                        _result.Payload = user;
                    }
                }
                else
                {
                    _result.Code = 404;
                    _result.Status = false;
                    _result.Message = "User not found!";
                }
                readerObj.Close();
            }
            catch (Exception ex)
            {
                _result.Code = 500;
                _result.Status = false;
                _result.Message = "GetSingleUser() Exception: " + ex.Message;
            }
            return _result;
        }

        // Get all users using a SelectionFilterModel
        public async Task<ServiceModel> GetAllUsers(SelectionFilterModel filter)
        {
            _result = new ServiceModel();
            try
            {
                _sql = "SELECT * FROM users WHERE UserName LIKE @UserName LIMIT @PageSize OFFSET @Offset";
                var checkRes = await _dbms.SqlFecthCommand(_sql, 
                    new MySqlParameter("@UserName", "%" + filter.UserName + "%"),
                    new MySqlParameter("@PageSize", filter.PageSize),
                    new MySqlParameter("@Offset", (filter.PageNumber - 1) * filter.PageSize)
                );

                var readerObj = checkRes.Payload;
                var userList = new List<UsersModel>();
                while (await readerObj.ReadAsync())
                {
                    userList.Add(new UsersModel
                    {
                        UserId = readerObj.GetString(readerObj.GetOrdinal("UserId")),
                        UserName = readerObj.GetString(readerObj.GetOrdinal("UserName")),
                        DateTouched = readerObj.GetDateTime(readerObj.GetOrdinal("DateTouched"))
                    });
                }
                readerObj.Close();

                if (userList.Count > 0)
                {
                    _result.Code = 200;
                    _result.Status = true;
                    _result.Message = "Users retrieved successfully.";
                    _result.Payload = userList;
                }
                else
                {
                    _result.Code = 404;
                    _result.Status = false;
                    _result.Message = "No users found!";
                }
            }
            catch (Exception ex)
            {
                _result.Code = 500;
                _result.Status = false;
                _result.Message = "GetAllUsers() Exception: " + ex.Message;
            }
            return _result;
        }

        // Delete a single user by userId
        public async Task<ServiceModel> DeleteSingleUser(string userId)
        {
            _result = new ServiceModel();
            try
            {
                if (string.IsNullOrEmpty(userId))
                {
                    _result.Code = 400;
                    _result.Status = false;
                    _result.Message = "Invalid user ID.";
                    return _result;
                }
                _sql = "DELETE FROM users WHERE UserId = @UserId";
                var checkRes = await _dbms.SqlCommand(_sql, new MySqlParameter("@UserId", userId));

                if (checkRes.Code == 200)
                {
                    _result.Code = 200;
                    _result.Status = true;
                    _result.Message = "User successfully deleted.";
                }
                else
                {
                    _result.Code = 500;
                    _result.Status = false;
                    _result.Message = "Error deleting user.";
                }
            }
            catch (Exception ex)
            {
                _result.Code = 500;
                _result.Status = false;
                _result.Message = "DeleteSingleUser() Exception: " + ex.Message;
            }
            return _result;
        }

        // Delete all users
        public async Task<ServiceModel> DeleteAllUsers()
        {
            try
            {
                _sql = "DELETE FROM users";
                var checkRes = await _dbms.SqlFecthCommand(_sql);
                if (checkRes.Code == 200)
                {
                    _result.Code = 200;
                    _result.Status = true;
                    _result.Message = "All users successfully deleted.";
                }
                else
                {
                    _result.Code = 500;
                    _result.Status = false;
                    _result.Message = "Error deleting all users.";
                }
            }
            catch (Exception ex)
            {
                _result.Code = 500;
                _result.Status = false;
                _result.Message = "DeleteAllUsers() Exception: " + ex.Message;
            }
            return _result;
        }
    }
}
