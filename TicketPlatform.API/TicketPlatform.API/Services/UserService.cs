using AutoMapper;
using ErrorOr;
using TicketPlatform.API.Model.In;
using TicketPlatform.API.Model;
using TicketPlatform.API.Repositories.Interfaces;

namespace TicketPlatform.API.Services
{
    public class UserService
    {
        private IUserRepository _repository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public List<User> GetAllUser(QueryParameters parameters)
        {
            return _repository.GetAllUsers(parameters);
        }

        public ErrorOr<User> GetEventById(int id)
        {
            return _repository.GetUserById(id);
        }

        public int InsertUser(UserIn userIn)
        {
            var user = _mapper.Map<User>(userIn);

            return _repository.InsertUser(user);
        }

        public bool UpsertUser(int id, UserIn userIn)
        {
            var user = _mapper.Map<User>(userIn);

            return _repository.UpsertUser(id, user);
        }

        public bool DeleteUser(int id)
        {
            return _repository.DeleteUser(id);
        }

        public bool DeleteUsers(List<int> ids)
        {
            return _repository.DeleteUsers(ids);
        }
    }
}
