using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieProject.Application.DTOS;
using MovieProject.Application.Interfaces;
using MovieProject.Application.JWT;

namespace MovieProject.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppUserController : ControllerBase
    {
        private readonly IAppUserRepository _repository;

        public AppUserController(IAppUserRepository repository)
        {
            _repository = repository;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] CheckUserDto dto)
        {
            var user = await _repository.CheckUser(dto);
            if (user == null || user.IsExist == false) return NotFound("Hatalı Kullanıcı Adı Veya Şifre");
            var token = TokenGenerator.GenerateToken(user);
            return Ok(token);
        }
        [Authorize]
        [HttpGet("protected-data")]
        public IActionResult GetProtectedData()
        {
            // Authenticated user can access this
            return Ok("This is a protected data");
        }
    }
}