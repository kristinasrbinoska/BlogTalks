using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogTalks.Domain.DTOs
{
    public class FakeDataStore
    {
        private static List<CommentDTO> _comments;
        private static List<BlogPostDto> _blogPosts;
        public FakeDataStore()
        {
            _comments = new List<CommentDTO>
            {
                new CommentDTO { Id = 1, Text = "This is the first comment", CreatedAt = DateTime.Now, CreatedBy = 1 },
                new CommentDTO { Id = 2, Text = "This is the second comment", CreatedAt = DateTime.Now, CreatedBy = 2 }
            };
            _blogPosts = new List<BlogPostDto>
            {
                new BlogPostDto { Id = 1, Title = "First Post", Text = "This is the first post", CreatedBy = 1, Timestamp = DateTime.Now, Tags = new List<string> { "tag1", "tag2" }, Comments = _comments },
                new BlogPostDto { Id = 2, Title = "Second Post", Text = "This is the second post", CreatedBy = 2, Timestamp = DateTime.Now, Tags = new List<string> { "tag3" }, Comments = _comments }
            };
        }
        public async Task AddComment (CommentDTO comment)
        {
            _comments.Add(comment);
            await Task.CompletedTask;
        }
        public async Task<IEnumerable<CommentDTO>> GetAllComments()
        {
            return await Task.FromResult(_comments);
        }
        public async Task<CommentDTO> GetCommentById(int id) =>  await Task.FromResult(_comments.Single(p => p.Id == id));

    }
}
