using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieProject.Application.Features.Comment.Queries;

namespace MovieProject.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CommentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetCommentsByMovieId")]
        public async Task<IActionResult> GetCommentsByMovieId(int id)
        {
            var values = await _mediator.Send(new GetCommentsByMovieQuery(id));
            return Ok(values);
        }
    }
}
