using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;
using Sat.Recruitment.Models.DTOs;
using Sat.Recruitment.Services.Services.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;
        private readonly HealthCheckService _health;

        public UserController(ILogger<UserController> logger, HealthCheckService healthCheck, IUserService service)
        {
            _logger = logger;
            _health = healthCheck;
            _userService = service;
        }

        [HttpPost]
        [Route("/create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateUser(
            [Required, FromBody] UserDto user
            )
        {
            await _health.CheckHealthAsync();

            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }
            _logger.Log(LogLevel.Information, "model is valid");

            try
            {
                _logger.Log(LogLevel.Information, $"trying to create a user");
                var result = await _userService.CreateUser(user);
                return Created(Request.Path, result);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong while calling {nameof(CreateUser)}");
                return Problem(ex.Message);
            }
        }
    }
}