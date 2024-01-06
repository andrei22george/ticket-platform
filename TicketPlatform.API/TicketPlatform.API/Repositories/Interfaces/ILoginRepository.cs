using TicketPlatform.API.Model;

namespace TicketPlatform.API.Repositories.Interfaces
{
    public interface ILoginRepository
    {
        public Admin GetAdminLoginByCredentials(QueryParameters parameters);
        public User GetUserLoginByCredentials(QueryParameters parameters);
    }
}