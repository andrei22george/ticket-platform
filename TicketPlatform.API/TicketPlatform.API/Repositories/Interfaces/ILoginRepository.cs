using TicketPlatform.API.Model;

namespace TicketPlatform.API.Repositories.Interfaces
{
    public interface ILoginRepository
    {
        public List<Person> GetLoginByCredentials(QueryParameters parameters);
    }
}