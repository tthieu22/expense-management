using System;
using System.Data;
using System.Data.SqlClient;

namespace ExpenseManagement.Model
{
    internal class APIKeyModel
    {
        private readonly Connect db = new Connect();

        public DataTable GetAPIKeys()
        {
            string query = "SELECT TOP 1000 * FROM APIKeys ORDER BY api_key_id DESC";
            return db.ExecuteQuery(query);
        }

        public bool CreateAPIKey(int userId, string serviceName, string apiKey)
        {
            string query = "INSERT INTO APIKeys (user_id, service_name, api_key) VALUES (@UserId, @ServiceName, @ApiKey)";
            SqlParameter[] parameters = {
                new SqlParameter("@UserId", userId),
                new SqlParameter("@ServiceName", serviceName),
                new SqlParameter("@ApiKey", apiKey)
            };
            return db.ExecuteNonQuery(query, parameters);
        }

        public bool UpdateAPIKey(int apiKeyId, string serviceName, string apiKey)
        {
            string query = "UPDATE APIKeys SET service_name = @ServiceName, api_key = @ApiKey WHERE api_key_id = @ApiKeyId";
            SqlParameter[] parameters = {
                new SqlParameter("@ApiKeyId", apiKeyId),
                new SqlParameter("@ServiceName", serviceName),
                new SqlParameter("@ApiKey", apiKey)
            };
            return db.ExecuteNonQuery(query, parameters);
        }

        public bool DeleteAPIKey(int apiKeyId)
        {
            string query = "DELETE FROM APIKeys WHERE api_key_id = @ApiKeyId";
            SqlParameter[] parameters = { new SqlParameter("@ApiKeyId", apiKeyId) };
            return db.ExecuteNonQuery(query, parameters);
        }
    }
}
