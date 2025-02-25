using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieProject.Application.Features.FavoriteMovie.Command;

namespace MovieProject.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoriteMoviesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FavoriteMoviesController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("AddFavMovies")]
        public async Task<IActionResult> AddFavMovies([FromBody] CreateFavoriteMovieCommand command)
        {
            await _mediator.Send(command);
            return Ok("Film Favorilere Eklendi");
        }
    }
}
