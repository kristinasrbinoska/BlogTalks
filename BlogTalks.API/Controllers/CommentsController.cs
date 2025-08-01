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

        // GET api/<CommentsController>
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
            if (comment == null)
            {
                return NotFound();
            }

            return Ok(comment);
        }

        // POST api/<CommentsController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] AddCommentResponse response)
        {
            var commentToReturn = await _mediator.Send(new AddCommentRequest(response));
            return Ok(commentToReturn);

          //  return CreatedAtRoute("GetCommentById", new { id = commentToReturn.Id }, commentToReturn);
        }

        // PUT api/<CommentsController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] EditCommentResponse response)
        {
            var commentToReturn = await _mediator.Send(new EditCommentRequest(id, response));
            if (commentToReturn == null)
            {
                return NotFound();
            }
            return NoContent();
        }


        // DELETE api/<CommentsController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var commentToReturn = await _mediator.Send(new DeleteCommentRequest(id));
            if (commentToReturn == null)
            {
                return NotFound();
            }
            return NoContent();

        }
    }

   
}