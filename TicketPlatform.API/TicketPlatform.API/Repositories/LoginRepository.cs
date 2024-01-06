using Dapper;
using Microsoft.Extensions.Options;
using System.Data.SqlClient;
using TicketPlatform.API.Model;
using TicketPlatform.API.Repositories.Interfaces;
using TicketPlatform.API.Services;

namespace TicketPlatform.API.Repositories
{
    public class LoginRepository : ILoginRepository
    {
        private readonly SqlConnection _sqlConnection;
        private readonly Configurations _configuration;

        public LoginRepository(IOptions<Configurations> configurations)
        {
            _configuration = configurations.Value;
            _sqlConnection = new SqlConnection(_configuration.ConnectionString);
        }

        public Admin GetAdminLoginByCredentials(QueryParameters parameters)
        {
            return _sqlConnection.Query<Admin>("SELECT * FROM Admin WHERE Email = @email AND Password = @password",
                new { email = parameters.Email, password = parameters.Password })
                .ToList().FirstOrDefault();
        }

        public User GetUserLoginByCredentials(QueryParameters parameters)
        {
            return _sqlConnection.Query<User>("SELECT * FROM [User] WHERE Email = @email AND Password = @password",
                new { email = parameters.Email, password = parameters.Password })
                .ToList().FirstOrDefault();
        }
    }
}
