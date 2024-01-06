using Microsoft.AspNetCore.Mvc;
using TicketPlatform.API.Model;
using TicketPlatform.API.Services;
using AutoMapper;

namespace TicketPlatform.API.Controllers
{
    [ApiController]
    [Route("login")]
    public class LoginController : ControllerBase
    {
        private readonly LoginService loginService;
        private readonly ILogger<LoginController> _logger;

        public LoginController(ILogger<LoginController> logger, LoginService service, IMapper mapper)
        {
            _logger = logger;
            loginService = service;
        }

        [HttpGet("admin")]
        public Admin GetAdminLoginByCredentials([FromQuery] QueryParameters parameters)
        {
            return loginService.GetAdminLoginByCredentials(parameters);
        }

        [HttpGet("user")]
        public User GetUserLoginByCredentials([FromQuery] QueryParameters parameters)
        {
            return loginService.GetUserLoginByCredentials(parameters);
        }
    }
}
