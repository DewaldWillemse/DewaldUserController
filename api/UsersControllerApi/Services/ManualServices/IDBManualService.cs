using BaseProjectApi.Models;
using System.Data.SqlClient;

namespace BaseProjectApi.Services.ManualServices
{
    public interface IDBManualService
    {
        Task<ServiceModel> SqlCommand(string sql);
        Task<DBServiceModel<SqlDataReader>> SqlFecthCommand(string sql);
    }
}
