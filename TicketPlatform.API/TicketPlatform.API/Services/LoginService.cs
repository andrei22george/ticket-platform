using AutoMapper;
using TicketPlatform.API.Model;
using TicketPlatform.API.Repositories.Interfaces;

namespace TicketPlatform.API.Services
{
    public class LoginService
    {
        private ILoginRepository _repository;
        private readonly IMapper _mapper;

        public LoginService(ILoginRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public Admin GetAdminLoginByCredentials(QueryParameters parameters)
        {
            return _repository.GetAdminLoginByCredentials(parameters);

        }

        public User GetUserLoginByCredentials(QueryParameters parameters)
        {
            return _repository.GetUserLoginByCredentials(parameters);

        }
    }
}
