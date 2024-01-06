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

        public List<Person> GetLoginByCredentials(QueryParameters parameters)
        {
            return _repository.GetLoginByCredentials(parameters);
        }
    }
}
