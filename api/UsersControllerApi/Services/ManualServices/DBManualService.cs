using BaseProjectApi.Models;
using System.Data;
using System.Data.SqlClient;

namespace BaseProjectApi.Services.ManualServices
{
    public class DBManualService : IDBManualService
    {
        public ServiceModel _result;
        public DBServiceModel<SqlDataReader> _dbresult;
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public DBManualService(IConfiguration configuration)
        {
            _configuration = configuration;
            _result = new ServiceModel();
            _dbresult = new DBServiceModel<SqlDataReader>();
            _connectionString = _configuration.GetValue<string>("ConnectionStrings:DefaultConnection");
        }            

        public Task<ServiceModel> SqlCommand(string sql)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        connection.Open();
                        command.ExecuteReader();

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
                _result.Message = "AddNewShiptoDatabase() Exception: " + ex.Message;

            }


            return Task.FromResult(_result);
        }

        public Task<DBServiceModel<SqlDataReader>> SqlFecthCommand(string sql)
        {            
            try
            {
                SqlConnection connection = new SqlConnection(_connectionString);
                SqlCommand command = new SqlCommand(sql, connection);

                connection.Open();
                _dbresult.Payload = command.ExecuteReader(CommandBehavior.CloseConnection);

                _dbresult.Code = 200;
                _dbresult.Status = true;
                _dbresult.Message = "DataFetch Completed Success";
                

            }
            catch (Exception ex)
            {
                _dbresult.Code = 500;
                _dbresult.Status = false;
                _dbresult.Message = "SqlFecthCommand() Exception: " + ex.Message;
            }

            return Task.FromResult(_dbresult);

            // To use it do this, the using will open and close all connections and readers for you
            /*
                string selectQuery = "SELECT * FROM YourTable";
                using (SqlDataReader reader = dataAccess.ExecuteReader(selectQuery))
                {
                    while (reader.Read())
                    {
                        // Process the data
                        int id = reader.GetInt32(0); // Assuming the first column is an integer
                        string name = reader.GetString(1); // Assuming the second column is a string
                        // Retrieve other columns as needed
                    }
                }
             */
        }
    }
}
