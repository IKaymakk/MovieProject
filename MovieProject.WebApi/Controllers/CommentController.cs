using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieProject.Application.Features.Comment.Commands;
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
        public async Task<IActionResult> GetCommentsByMovieId(int id, string? sortBy, int page, int pageSize)
        {
            var values = await _mediator.Send(new GetCommentsByMovieQuery(id, sortBy, page, pageSize));
            return Ok(values);
        }
        [HttpGet("GetCommentsByUserId")]
        public async Task<IActionResult> GetCommentsByUserId(int id)
        {
            var values = await _mediator.Send(new GetCommentsByUserIdQuery(id));
            return Ok(values);
        }
        [HttpPost("CreateComment")]
        public async Task<IActionResult> CreateComment(AddCommentCommand command)
        {
            try
            {
                await _mediator.Send(command);
                return Ok("Yorum Eklendi");

            }
            catch (Exception ex)
            {
                throw new Exception("Hata Oluştu", ex);
            }
        }

    }
}
