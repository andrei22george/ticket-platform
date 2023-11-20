using AutoMapper;
using ErrorOr;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TicketPlatformBackend.Model;
using TicketPlatformBackend.Model.In;
using TicketPlatformBackend.Services;
using TicketPlatformBackend.Utilities;

namespace TicketPlatformBackend.Controllers
{
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
                return 0; // Errors.Director.FailedValidation;
            }

            if (StringValidationHelper.IsEmail(request.Email))
            {
                return 0; // Errors.Director.InvalidEmail;
            }
            // password validation
            /*else if (StringValidationHelper.IsPhone(request.Phone))
            {
                return Errors.Director.InvalidPhone;
            }*/
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
                return false; // Errors.Director.FailedValidation;
            }

            if (StringValidationHelper.IsEmail(request.Email))
            {
                return false; // Errors.Director.InvalidEmail;
            }
            // password validation
            /*else if (StringValidationHelper.IsPhone(request.Phone))
            {
                return false; // Errors.Director.InvalidPhone;
            }*/
            else
            {
                return adminService.UpsertAdmin(id, request); // == false ? Errors.Director.NotFound : true;
            }
        }

        [HttpDelete("{id}")]
        public ErrorOr<bool> DeleteAdmin(int id)
        {
            return adminService.DeleteAdmin(id); // == false ? Errors.Director.NotFound : true;
        }

        [HttpDelete]
        public bool DeleteAdmins([FromQuery] List<int> ids)
        {
            return adminService.DeleteAdmins(ids);
        }
    }
}
