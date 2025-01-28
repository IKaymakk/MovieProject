using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieProject.Application.Features.Movie.Commands;
using MovieProject.Application.Features.Movie.Queries;
using MovieProject.Application.Features.Movie.Results;
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


        #region HttpGet
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
        [HttpGet("MoviesByGenre")]
        public async Task<IActionResult> GetMoviesByGenre(int id, string? sortBy, int page = 1, int pageSize = 18)
        {
            var options = new GetMoviesByGenreQuery
            {
                SortBy = sortBy,
                Page = page,
                PageSize = pageSize,
                id = id
            };
            var movies = await _mediator.Send(options);
            return Ok(movies);
        }
        [HttpGet("MoviesByFilter")]
        public async Task<IActionResult> GetMoviesByFilter(string? sortBy, int page = 1, int pageSize = 18)
        {
            // Sayfa numarası ve boyutu varsayılan değerlerle kullanılabilir.
            var query = new GetMoviesByFilterQuery
            {
                SortBy = sortBy,
                Page = page,
                PageSize = pageSize
            };

            var movies = await _mediator.Send(query);
            return Ok(movies);
        }
        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] int? categoryId, [FromQuery] string? searchTerm, [FromQuery] string? sortBy, [FromQuery] int page = 1, [FromQuery] int pageSize = 18)
        {
            var query = new SearchMoviesQuery
            {
                categoryId = categoryId,
                SearchTerm = searchTerm,
                SortBy = sortBy,
                Page = page,
                PageSize = pageSize
            };

            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpGet("GetSimiilarMovies")]
        public async Task<IActionResult> GetSimilarMovies(string Hashtag)
        {
            var similarMovies = await _mediator.Send(new GetSimilarMoviesQuery(Hashtag));
            return Ok(similarMovies);
        }


        #endregion

        #region HttpPost
        [HttpPost]
        public async Task<IActionResult> CreateMovie(CreateMovieCommand command)
        {
            await _mediator.Send(command);
            return Ok("Film Eklendi");
        }
        #endregion

        #region HttpPut
        [HttpPut]
        public async Task<IActionResult> UpdateMovie(UpdateMovieCommand command)
        {
            await _mediator.Send(command);
            return Ok("Film Güncellendi");
        }

        #endregion

        #region HttpDelete
        [HttpDelete]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            await _mediator.Send(new DeleteMovieCommand(id));
            return Ok("Film Silindi");
        }
        #endregion



    }
}
