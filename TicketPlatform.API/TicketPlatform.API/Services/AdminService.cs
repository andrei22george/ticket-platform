using AutoMapper;
using ErrorOr;
using TicketPlatform.API.Model.In;
using TicketPlatform.API.Model;
using TicketPlatform.API.Repositories.Interfaces;

namespace TicketPlatform.API.Services
{
    public class AdminService
    {
        private IAdminRepository _repository;
        private readonly IMapper _mapper;

        public AdminService(IAdminRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public List<Admin> GetAllAdmins(QueryParameters parameters)
        {
            return _repository.GetAllAdmins(parameters);
        }

        public ErrorOr<Admin> GetAdminById(int id)
        {
            return _repository.GetAdminById(id);
        }

        public int InsertAdmin(AdminIn adminIn)
        {
            var admin = _mapper.Map<Admin>(adminIn);

            return _repository.InsertAdmin(admin);
        }

        public bool UpsertAdmin(int id, AdminIn adminIn)
        {
            var admin = _mapper.Map<Admin>(adminIn);

            return _repository.UpsertAdmin(id, admin);
        }

        public bool DeleteAdmin(int id)
        {
            return _repository.DeleteAdmin(id);
        }

        public bool DeleteAdmins(List<int> ids)
        {
            return _repository.DeleteAdmins(ids);
        }
    }
}
