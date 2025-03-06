using System.Threading.Tasks;
using BaseProjectApi.Models;
using MySql.Data.MySqlClient;  // Required for MySqlDataReader and MySqlParameter

namespace BaseProjectApi.Services.ManualServices
{
    public interface IDBManualService
    {
        Task<ServiceModel> SqlCommand(string sql);
        Task<ServiceModel> SqlCommand(string sql, params MySqlParameter[] parameters); // New overload

        Task<DBServiceModel<MySqlDataReader>> SqlFecthCommand(string sql);
        Task<DBServiceModel<MySqlDataReader>> SqlFecthCommand(string sql, params MySqlParameter[] parameters);

        Task<ServiceModel> CheckIfUserIdExist(string userId);
        Task<ServiceModel> CheckIfUserNameExist(string userName);
    }
}
