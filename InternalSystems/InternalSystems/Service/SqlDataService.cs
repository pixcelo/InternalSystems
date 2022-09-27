using System;

namespace InternalSystems.Service
{
    public class SqlDataService
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public SqlDataService(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = configuration["ConnectionStrings:DbContext"];
        }

        public string GetConnectionString()
        {
            return _connectionString;
        }
    }
}

