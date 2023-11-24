using ErrorOr;
using TicketPlatform.API.Model;

namespace TicketPlatform.API.Repositories.Interfaces
{
    public interface IUserRepository
    {
        public List<User> GetAllUsers(QueryParameters parameters);
        public ErrorOr<User> GetUserById(int id);
        public int InsertUser(User user);
        public bool UpsertUser(int id, User user);
        public bool DeleteUser(int id);
        public bool DeleteUsers(List<int> ids);
    }
}
