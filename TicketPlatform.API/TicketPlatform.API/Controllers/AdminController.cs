using AutoMapper;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using TicketPlatform.API.Model.In;
using TicketPlatform.API.Model;
using TicketPlatform.API.Services;
using TicketPlatform.API.Utilities;
using FluentValidation.Results;
using TicketPlatform.API.ServiceErrors;

namespace TicketPlatform.API.Controllers
{
    [ApiController]
    [Route("admins")]
    public class AdminController : ControllerBase
    {
        private readonly AdminService adminService;
        private readonly ILogger<AdminController> _logger;

        public AdminController(ILogger<AdminController> logger, AdminService service, IMapper mapper)
        {
            _logger = logger;
            adminService = service;
        }

        [HttpGet]
        public IEnumerable<Admin> Get([FromQuery] QueryParameters parameters)
        {
            return adminService.GetAllAdmins(parameters);
        }

        [HttpGet("{id}")]
        public ErrorOr<Admin> GetAdminById(int id)
        {
            return adminService.GetAdminById(id);
        }

        [HttpPost]
        public ErrorOr<int> CreateAdmin([FromBody] AdminIn request)
        {
            var validator = new AdminInValidator();
            ValidationResult results = validator.Validate(request);

            if (!results.IsValid)
            {
                foreach (var failure in results.Errors)
                {
                    Console.WriteLine("Property " + failure.PropertyName + " failed validation. Error was: " + failure.ErrorMessage);
                }
                return Errors.Admin.FailedValidation;
            }

            if (StringValidationHelper.IsEmail(request.Email))
            {
                return Errors.Admin.InvalidEmail;
            }
            else if (StringValidationHelper.IsAdminPassword(request.Password))
            {
                return Errors.Admin.InvalidPassword;
            }
            else
            {
                return adminService.InsertAdmin(request);
            }
        }

        [HttpPut("{id}")]
        public ErrorOr<bool> UpsertAdmin(int id, [FromBody] AdminIn request)
        {
            var validator = new AdminInValidator();
            ValidationResult results = validator.Validate(request);

            if (!results.IsValid)
            {
                foreach (var failure in results.Errors)
                {
                    Console.WriteLine("Property " + failure.PropertyName + " failed validation. Error was: " + failure.ErrorMessage);
                }
                return Errors.Admin.FailedValidation;
            }

            if (StringValidationHelper.IsEmail(request.Email))
            {
                return Errors.Admin.InvalidEmail;
            }
            else if (StringValidationHelper.IsAdminPassword(request.Password))
            {
                return Errors.Admin.InvalidPassword;
            }
            else
            {
                return adminService.UpsertAdmin(id, request) == false ? Errors.Admin.NotFound : true;
            }
        }

        [HttpDelete("{id}")]
        public ErrorOr<bool> DeleteAdmin(int id)
        {
            return adminService.DeleteAdmin(id) == false ? Errors.Admin.NotFound : true;
        }

        [HttpDelete]
        public bool DeleteAdmins([FromQuery] List<int> ids)
        {
            return adminService.DeleteAdmins(ids);
        }
    }
}
