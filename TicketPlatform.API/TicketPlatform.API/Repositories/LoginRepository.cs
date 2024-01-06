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

        public Person GetLoginByCredentials(QueryParameters parameters)
        {
            var searchInAdminTable = _sqlConnection.Query<Person>("SELECT * FROM Admin WHERE Email = @email AND Password = @password",
                new { email = parameters.Email, password = parameters.Password })
                .ToList();

            if (searchInAdminTable.Count > 0)
            {
                searchInAdminTable.FirstOrDefault().IsAdmin = true;
                return searchInAdminTable.FirstOrDefault();
            }
            else
            {
                return _sqlConnection.Query<Person>("SELECT * FROM [User] WHERE email = @email AND password = @password",
                    new { email = parameters.Email, password = parameters.Password })
                    .ToList().FirstOrDefault();
            }
        }
    }
}
