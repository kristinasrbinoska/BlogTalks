using BlogTalks.Application.Comments.Comands;
using BlogTalks.Application.Comments.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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


        // GET api/<CommentsController>
        [HttpGet]
        [Authorize]
        public async Task<ActionResult> Get()
        {
            var comments = await _mediator.Send(new GetRequest());
            return Ok(comments);
        }

        // GET api/<CommentsController>/5
        [HttpGet("{id}", Name = "GetCommentById")]
        [Authorize]
        public async Task<ActionResult> Get([FromRoute] GetCommentByIdRequest request)
        {
            var comment = await _mediator.Send(request);
            if (comment == null)
            {
                return NotFound();
            }

            return Ok(comment);
        }
        // GET api/<CommentsController>/5

        [HttpGet("/blogPosts/{blogPostId}/comments", Name = "GetByBlogPostId")]
        [Authorize]
        public async Task<ActionResult> Get([FromRoute] int blogPostId)
        {
            var comment = await _mediator.Send(new GetByBlogPostIdRequest(blogPostId));
            if (comment == null)
            {
                return NotFound();
            }

            return Ok(comment);
        }


        // POST api/<CommentsController>
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Post([FromBody] AddRequest request)
        {
            var commentToReturn = await _mediator.Send(request);
            if (commentToReturn == null)
            {
                return NotFound();
            }
            return Ok(commentToReturn);

        }

        // PUT api/<CommentsController>/5
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Put([FromRoute]int id, [FromBody] EditRequest request)
        {
            var commentToReturn = await _mediator.Send(new EditRequest(id,request.Text));
            if (commentToReturn == null)
            {
                return NotFound();
            }
            return NoContent();
        }


        // DELETE api/<CommentsController>/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var commentToReturn = await _mediator.Send(new DeleteRequest(id));
            if (commentToReturn == null)
            {
                return NotFound();
            }
            return NoContent();

        }
    }

   
}