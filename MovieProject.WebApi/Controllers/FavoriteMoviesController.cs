using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieProject.Application.Features.FavoriteMovie.Command;
using MovieProject.Persistance.Context;

namespace MovieProject.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FavoriteMoviesController : ControllerBase
{
    private readonly MovieContext _context;
    private readonly IMediator _mediator;

    public FavoriteMoviesController(IMediator mediator, MovieContext context)
    {
        _mediator = mediator;
        _context = context;
    }
    [HttpPost("AddFavMovies")]
    public async Task<IActionResult> AddFavMovies([FromBody] CreateFavoriteMovieCommand command)
    {
        bool isFavorited = await _mediator.Send(command);

        if (isFavorited)
        {
            return Ok("Film Favorilere Eklendi");
        }
        else
        {
            return BadRequest("Film Favorilerden Kaldırıldı");
        }
    }
    [HttpGet("CheckFavoriteStatus")]
    public async Task<IActionResult> CheckFavoriteStatus(int userId, int movieId)
    {
        var isFavorited = await _context.FavoriteMovies
            .AsNoTracking()
            .AnyAsync(x => x.AppUserId == userId && x.MovieId == movieId);

        return Ok(new { isFavorited });
    }

}
