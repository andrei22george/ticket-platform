using ErrorOr;
using TicketPlatformBackend.Model;

namespace TicketPlatformBackend.Repositories.Interfaces
{
    internal interface IUserRepository
    {
        public List<User> GetAllUsers(QueryParameters parameters);
        public ErrorOr<User> GetUserById(int id);
        public int InsertUser(User user);
        public bool UpsertUser(int id, User user);
        public bool DeleteUser(int id);
        public bool DeleteUsers(List<int> ids);
    }
}
