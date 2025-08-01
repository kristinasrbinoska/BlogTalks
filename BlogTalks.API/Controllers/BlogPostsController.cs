using BlogTalks.Application.BlogPosts.Commands;
using BlogTalks.Application.BlogPosts.Queries;
using BlogTalks.Domain.DTOs;
using MediatR;
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
        public async Task<ActionResult> Get()
        {
            var blogPosts = await _mediator.Send(new GetBlogPostsRequest());
            return Ok(blogPosts);
        }

        // GET api/<BlogPostsController>/5
        [HttpGet("{id}",Name = "GetBlogPostById")]
        public async Task<ActionResult> Get([FromRoute] GetBlogPostByIdRequest request )
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
        public async Task<ActionResult> Add([FromBody] AddBlogPostResponse response)
        {
            var blogPosts = await _mediator.Send(new AddBlogPostRequest(response));
            return CreatedAtRoute("GetBlogPostById", new { id = blogPosts.Id }, blogPosts); ;
        }

        // PUT api/<BlogPostsController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] EditBlogPostResponse response)
        {
            var blogPosts = await _mediator.Send(new EditBlogPostRequest(id,response));
            if (blogPosts == null)
            {
                return NotFound();
            }
            return Ok(blogPosts);
        }

        // DELETE api/<BlogPostsController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var blogPosts = await _mediator.Send(new DeleteBlogPostRequest(id));
            if (blogPosts == null)
            {
                return NotFound();
            }
            return Ok(blogPosts);
        }
    }

  
}
