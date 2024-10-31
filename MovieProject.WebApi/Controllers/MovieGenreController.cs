using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieProject.Application.Features.MovieGenre.Queries;

namespace MovieProject.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieGenreController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MovieGenreController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]

        public async Task<IActionResult> GetAllMoviesWithGenres()
        {
            var movies = await _mediator.Send(new GetAllMoviesWithGenresQuery());
            return Ok(movies);
        }
    }
}
