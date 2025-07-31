using BlogTalks.Application.Comments.Comands;
using BlogTalks.Application.Comments.Queries;
using BlogTalks.Domain.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BlogTalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CommentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //  GET: api/<CommentsController>
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var comments = await _mediator.Send(new GetCommentsRequest());
            return Ok(comments);
        }



        // GET api/<CommentsController>/5
        [HttpGet("{id}", Name = "GetCommentById")]
        public async Task<ActionResult> Get([FromRoute] GetCommentByIdRequest request)
        {
            var comment = await _mediator.Send(request);
            return Ok(comment);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] AddCommentResponse response)
        {
            var commentToReturn = await _mediator.Send(new AddCommentCommand(response));

            return CreatedAtRoute("GetCommentById", new { id = commentToReturn.Id }, commentToReturn);
        }

        //[HttpPut("{id}")]
        // public IActionResult Put(int id, [FromBody] CommentDTO dto)
        // {
        //     var comment = comments.FirstOrDefault(x => x.Id == id);
        //     if (comment == null)
        //     {
        //         return NotFound();
        //     }
        //     comment.Text = dto.Text;
        //     comment.CreatedAt = dto.CreatedAt;
        //     comment.CreatedBy = dto.CreatedBy;
        //     comment.BlogPostId = dto.BlogPostId;

        //     return Ok(comments);
        // }



        // [HttpDelete("{id}")]
        // public void Delete(int id)
        // {
        //     var comment = comments.FirstOrDefault(x => id == x.Id);
        //     if (comment != null)
        //     {
        //         comments.Remove(comment);
        //     }
        // }
    }
}
