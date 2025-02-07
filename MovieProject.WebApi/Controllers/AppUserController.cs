using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieProject.Application.DTOS;
using MovieProject.Application.Features.AppUser.Commands;
using MovieProject.Application.Features.AppUser.Queries;
using MovieProject.Application.Interfaces;
using MovieProject.Application.JWT;

namespace MovieProject.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppUserController : ControllerBase
    {
        private readonly IAppUserRepository _repository;
        private readonly IMediator _mediator;

        public AppUserController(IAppUserRepository repository, IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
        }
        [Authorize]
        [HttpGet("protected-data")]
        public IActionResult GetProtectedData()
        {
            // Authenticated user can access this
            return Ok("This is a protected data");
        }
        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            return Ok(await _mediator.Send(new GetAllAppUsersQuery()));
        }
        [HttpGet("GetUser")]
        public async Task<IActionResult> GetUser(int id)
        {
            return Ok(await _mediator.Send(new GetAppUserByIdQuery(id)));
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] CheckUserDto dto)
        {
            var user = await _repository.CheckUser(dto);
            if (user == null || user.IsExist == false) return NotFound("Hatalı Kullanıcı Adı Veya Şifre");
            var token = TokenGenerator.GenerateToken(user);
            return Ok(token);
        }
        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser([FromBody] CreateAppUserCommand command)
        {
            await _mediator.Send(command);
            return Ok("Kullanıcı Eklendi");
        }
    }
}