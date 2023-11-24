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
    [Route("users")]
    public class UserController : ControllerBase
    {
        private readonly UserService userService;
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger, UserService service, IMapper mapper)
        {
            _logger = logger;
            userService = service;
        }

        [HttpGet]
        public IEnumerable<User> Get([FromQuery] QueryParameters parameters)
        {
            return userService.GetAllUser(parameters);
        }

        [HttpGet("{id}")]
        public ErrorOr<User> GetUserById(int id)
        {
            return userService.GetUserById(id);
        }

        [HttpPost]
        public ErrorOr<int> CreateUser([FromBody] UserIn request)
        {
            var validator = new UserInValidator();
            ValidationResult results = validator.Validate(request);

            if (!results.IsValid)
            {
                foreach (var failure in results.Errors)
                {
                    Console.WriteLine("Property " + failure.PropertyName + " failed validation. Error was: " + failure.ErrorMessage);
                }
                return Errors.User.FailedValidation;
            }

            if (StringValidationHelper.IsEmail(request.Email))
            {
                return Errors.User.InvalidEmail;
            }
            else if (StringValidationHelper.IsUserPassword(request.Password))
            {
                return Errors.User.InvalidPassword;
            }
            else
            {
                return userService.InsertUser(request);
            }
        }

        [HttpPut("{id}")]
        public ErrorOr<bool> UpsertUser(int id, [FromBody] UserIn request)
        {
            var validator = new UserInValidator();
            ValidationResult results = validator.Validate(request);

            if (!results.IsValid)
            {
                foreach (var failure in results.Errors)
                {
                    Console.WriteLine("Property " + failure.PropertyName + " failed validation. Error was: " + failure.ErrorMessage);
                }
                return Errors.User.FailedValidation;
            }

            if (StringValidationHelper.IsEmail(request.Email))
            {
                return Errors.User.InvalidEmail;
            }
            else if (StringValidationHelper.IsUserPassword(request.Password))
            {
                return Errors.User.InvalidPassword;
            }
            else
            {
                return userService.UpsertUser(id, request) == false ? Errors.User.NotFound : true;
            }
        }

        [HttpDelete("{id}")]
        public ErrorOr<bool> DeleteUser(int id)
        {
            return userService.DeleteUser(id) == false ? Errors.User.NotFound : true;
        }

        [HttpDelete]
        public bool DeleteUsers([FromQuery] List<int> ids)
        {
            return userService.DeleteUsers(ids);
        }
    }
}
