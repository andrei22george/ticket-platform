using TicketPlatform.API.Model;

namespace TicketPlatform.API.Repositories.Interfaces
{
    public interface ILoginRepository
    {
        public Person GetLoginByCredentials(QueryParameters parameters);
    }
}