using System;
using System.Data;
using ExpenseManagement.Model;

namespace ExpenseManagement.Controller
{
    internal class APIKeyController
    {
        private readonly APIKeyModel apiKeyModel = new APIKeyModel();

        public DataTable GetAPIKeys()
        {
            return apiKeyModel.GetAPIKeys();
        }

        public bool CreateAPIKey(int userId, string serviceName, string apiKey)
        {
            if (string.IsNullOrWhiteSpace(serviceName) || string.IsNullOrWhiteSpace(apiKey))
            {
                throw new ArgumentException("Service name and API key cannot be empty.");
            }
            return apiKeyModel.CreateAPIKey(userId, serviceName, apiKey);
        }

        public bool UpdateAPIKey(int apiKeyId, string serviceName, string apiKey)
        {
            if (string.IsNullOrWhiteSpace(serviceName) || string.IsNullOrWhiteSpace(apiKey))
            {
                throw new ArgumentException("Service name and API key cannot be empty.");
            }
            return apiKeyModel.UpdateAPIKey(apiKeyId, serviceName, apiKey);
        }

        public bool DeleteAPIKey(int apiKeyId)
        {
            return apiKeyModel.DeleteAPIKey(apiKeyId);
        }
    }
}
