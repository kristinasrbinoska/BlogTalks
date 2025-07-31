using BlogTalks.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BlogTalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostsController : ControllerBase
    {
        private static   List<BlogPostDto> blogPosts = new List<BlogPostDto>
        {
            new BlogPostDto {Id = 1, Title = "Title 1",Text = "Text 1",CreatedBy = 1, Timestamp = DateTime.Now,Tags = new List<string> {"f","a"}
            ,Comments = new List<CommentDTO> {
                new CommentDTO {Id = 1, Text = "Some text 1",CreatedAt = DateTime.Now,CreatedBy = 1,BlogPostId = 3},
                new CommentDTO {Id = 2, Text = "Some text 2",CreatedAt = DateTime.Now,CreatedBy = 2,BlogPostId = 4}
            } },
            new BlogPostDto {Id = 2, Title = "Title 2",Text = "Text 2",CreatedBy = 2, Timestamp = DateTime.Now,Tags = new List<string> {"s","h"}
            ,Comments = new List<CommentDTO> {
                new CommentDTO {Id = 3, Text = "Some text 3",CreatedAt = DateTime.Now,CreatedBy =3,BlogPostId = 4},
                new CommentDTO {Id = 4, Text = "Some text 4",CreatedAt = DateTime.Now,CreatedBy = 4,BlogPostId = 4},
            }}
        };

        // GET: api/<BlogPostsController>
        [HttpGet]
        public ActionResult<BlogPostDto> Get()
        {
            return Ok(blogPosts);
        }

        // GET api/<BlogPostsController>/5
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            var dto = new BlogPostDto
            {
                Id = id,
                Title = "Fashion",
                Text = "This is a blog post about fashion.",
                CreatedBy = id,
                Timestamp = DateTime.Now,
                Tags = new List<string> { "Fashion", "Style", "Trends" },
                Comments = new List<CommentDTO>
                {
                    new CommentDTO { Id = 1, Text = "Text 1", CreatedAt = DateTime.Now, CreatedBy = 1, BlogPostId = id },
                    new CommentDTO { Id = 2, Text = "Text 2", CreatedAt = DateTime.Now, CreatedBy = 2, BlogPostId = id }
                }
            };
            return Ok(dto);
        }

        // POST api/<BlogPostsController>
        [HttpPost]
        public ActionResult Add([FromBody] BlogPostDto dto)
        {
            BlogPostDto post = new BlogPostDto
            {
                Id = dto.Id,
                Title = dto.Text,
                Text = dto.Text,
                CreatedBy = dto.CreatedBy,
                Timestamp = dto.Timestamp,
                Tags = dto.Tags,
                Comments = dto.Comments
            };
            blogPosts.Add(post);
            return Ok(blogPosts);

        }

        // PUT api/<BlogPostsController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] BlogPostDto dto)
        {
            var post = blogPosts.FirstOrDefault(x => x.Id == id);
            if (post == null)
            {
                return NotFound();
            }
            post.Title = dto.Title;
            post.Text = dto.Text;
            post.CreatedBy = dto.CreatedBy;
            post.Timestamp = dto.Timestamp;
            post.Tags = dto.Tags;
            post.Comments = dto.Comments;

            return Ok(blogPosts);
        }

        // DELETE api/<BlogPostsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var post = blogPosts.FirstOrDefault(x => x.Id == id);
            if (post != null)
            {
                blogPosts.Remove(post);
            }
        }
    }
}
