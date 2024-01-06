using Dapper.Contrib.Extensions;
using ErrorOr;
using Microsoft.Extensions.Options;
using System.Data.SqlClient;
using TicketPlatform.API.Model;
using TicketPlatform.API.Repositories.Interfaces;
using TicketPlatform.API.ServiceErrors;
using TicketPlatform.API.Services;

namespace TicketPlatform.API.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly SqlConnection _sqlConnection;
        private readonly Configurations _configuration;

        public AdminRepository(IOptions<Configurations> configurations)
        {
            _configuration = configurations.Value;
            _sqlConnection = new SqlConnection(_configuration.ConnectionString);
        }

        public List<Admin> GetAllAdmins(QueryParameters parameters)
        {
            return _sqlConnection.GetAll<Admin>().ToList();
        }

        public ErrorOr<Admin> GetAdminById(int id)
        {
            var admin = _sqlConnection.Get<Admin>(id);

            return admin == null ? Errors.Admin.NotFound : admin;
        }

        public int InsertAdmin(Admin admin)
        {
            return (int)_sqlConnection.Insert(admin);
        }

        public bool UpsertAdmin(int id, Admin admin)
        {
            admin.Id = id;

            return _sqlConnection.Update(admin);
        }

        public bool DeleteAdmin(int id)
        {
            return _sqlConnection.Delete(new Admin { Id = id });
        }

        public bool DeleteAdmins(List<int> ids)
        {
            var adminsToDelete = ids.Select(id => new Admin { Id = id }).ToList();

            return _sqlConnection.Delete(adminsToDelete);
        }
    }
}
