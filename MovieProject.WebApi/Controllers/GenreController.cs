using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieProject.Application.Features.Genre.Commands;
using MovieProject.Application.Features.Genre.Queries;

namespace MovieProject.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GenreController : ControllerBase
{
    private readonly IMediator _mediator;

    public GenreController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllGenres()
    {
        var genres = await _mediator.Send(new GetAllGenresQuery());
        return Ok(genres);
    }

    [HttpPost]
    public async Task<IActionResult> CreateGenre(CreateGenreCommand command)
    {
        await _mediator.Send(command);
        return Ok("Kayıt Başarılı Bir Şekilde Eklendi");
    }
}
