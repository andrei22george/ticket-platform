using ErrorOr;
using TicketPlatform.API.Model;

namespace TicketPlatform.API.Repositories.Interfaces
{
    public interface IAdminRepository
    {
        public List<Admin> GetAllAdmins(QueryParameters parameters);
        public ErrorOr<Admin> GetAdminById(int id);
        public int InsertAdmin(Admin admin);
        public bool UpsertAdmin(int id, Admin admin);
        public bool DeleteAdmin(int id);
        public bool DeleteAdmins(List<int> ids);
    }
}