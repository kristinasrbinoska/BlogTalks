using BlogTalks.Application.BlogPosts.Commands;
using BlogTalks.Application.BlogPosts.Queries;
using BlogTalks.Domain.DTOs;
using BlogTalks.Infrastructure.Atuhenticatin;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BlogTalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BlogPostsController(IMediator mediator)
        {
            _mediator = mediator;
        }



        // GET: api/<BlogPostsController>
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> Get()
        {
            var blogPosts = await _mediator.Send(new GetRequest());
            return Ok(blogPosts);
        }

        // GET api/<BlogPostsController>/5
        [HttpGet("{id}",Name = "GetBlogPostById")]
        [Authorize]
        public async Task<ActionResult> Get([FromRoute] GetByIdRequest request )
        {
            var blogPost = await _mediator.Send(request);
            if (blogPost == null)
            {
                return NotFound();
            }
            return Ok(blogPost);
                       
        }
        

        // POST api/<BlogPostsController>
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Add([FromBody] AddRequest request)
        {
            var blogPosts = await _mediator.Send(request);
            if (blogPosts == null)
            {
                return NotFound();
            }
            return Ok(blogPosts);
        
        }

        // PUT api/<BlogPostsController>/5
        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult> Put([FromRoute]int id, [FromBody] EditRequest request)
        {
            var blogPosts = await _mediator.Send(new EditRequest(id, request.Title, request.Text, request.Tags));
            if (blogPosts == null)
            {
                return NotFound();
            }
            return NoContent();
        }

        // DELETE api/<BlogPostsController>/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var blogPosts = await _mediator.Send(new DeleteRequest(id));
            if (blogPosts == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }

  
}
