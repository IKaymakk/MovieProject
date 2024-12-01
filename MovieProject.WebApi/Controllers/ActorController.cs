using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieProject.Application.Features.Actor.Queries;

namespace MovieProject.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ActorController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("MovieActors")]
        public async Task<IActionResult> GetMovieActors(int id)
        {
            var values = await _mediator.Send(new GetMovieActorsQuery(id));
            return Ok(values);
        }
    }
}
