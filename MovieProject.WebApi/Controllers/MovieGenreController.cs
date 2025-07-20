using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieProject.Application.Features.MovieGenre.Commands;
using MovieProject.Application.Features.MovieGenre.Queries;

namespace MovieProject.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MovieGenreController : ControllerBase
{
    private readonly IMediator _mediator;

    public MovieGenreController(IMediator mediator)
    {
        _mediator = mediator;
    }

    #region HttpGet
    [HttpGet]
    public async Task<IActionResult> GetAllMoviesWithGenres()
    {
        var movies = await _mediator.Send(new GetAllMoviesWithGenresQuery());
        return Ok(movies);
    }
    [HttpGet("Top24Movies")]
    public async Task<IActionResult> GetTop24Movies()
    {
        var movies = await _mediator.Send(new GetTop24MoviesQuery());
        return Ok(movies);
    }
    #endregion

    #region HttpPost
    [HttpPost("AddGenresToMovie")]
    public async Task<IActionResult> AddGenresToMovie(AddGenresToMoviesCommand command)
    {
        await _mediator.Send(command);
        return Ok("Başarılı Bir Şekilde Filme Tür Eklendi.");
    }
    #endregion
}
