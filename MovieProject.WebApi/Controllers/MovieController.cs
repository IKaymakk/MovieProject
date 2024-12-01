using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieProject.Application.Features.Movie.Commands;
using MovieProject.Application.Features.Movie.Queries;
using MovieProject.Application.Features.MovieGenre.Queries;

namespace MovieProject.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MovieController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> MoviesList()
        {
            var movies = await _mediator.Send(new GetAllMoviesQuery());
            return Ok(movies);
        }
        [HttpGet("id")]
        public async Task<IActionResult> GetMovieById(int id)
        {
            var movie = await _mediator.Send(new GetMovieByIdQuery(id));
            return Ok(movie);
        }
        [HttpGet("Last24Movies")]
        public async Task<IActionResult> GetLast24Movies()
        {
            var movies = await _mediator.Send(new GetLast15MoviesQuery());
            return Ok(movies);
        }
        [HttpGet("MovieDetails")]
        public async Task<IActionResult> GetMovieDetails(int id)
        {
            var movies = await _mediator.Send(new GetMovieDetailsQuery(id));
            return Ok(movies);
        }
        [HttpPost]
        public async Task<IActionResult> CreateMovie(CreateMovieCommand command)
        {
            await _mediator.Send(command);
            return Ok("Film Eklendi");
        }
        [HttpPut]
        public async Task<IActionResult> UpdateMovie(UpdateMovieCommand command)
        {
            await _mediator.Send(command);
            return Ok("Film Güncellendi");
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            await _mediator.Send(new DeleteMovieCommand(id));
            return Ok("Film Silindi");
        }

    }
}
