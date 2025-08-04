using BlogTalks.Domain.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogTalks.Application.BlogPosts.Commands
{
    public class AddBlogPostHandler : IRequestHandler<AddRequest, AddResponse>
    {
        private readonly FakeDataStore _dataStore;

        public AddBlogPostHandler(FakeDataStore dataStore)
        {
            _dataStore = dataStore;
        }

        public async Task<AddResponse> Handle(AddRequest request, CancellationToken cancellationToken)
        {
            var blogPostDto = new BlogPostDto
            {
                Id = request.BlogPost.Id,
                Title = request.BlogPost.Title,
                Text = request.BlogPost.Text,
                CreatedBy = request.BlogPost.CreatedBy,
                Timestamp = request.BlogPost.Timestamp,
                Tags = request.BlogPost.Tags,
                Comments = request.BlogPost.Comments
                    .Select(c => new CommentDTO
                    {
                        Id = c.Id,
                        Text = c.Text,
                        CreatedAt = c.CreatedAt,
                        CreatedBy = c.CreatedBy,
                        BlogPostId = c.BlogPostId
                    })
                    .ToList()
            };

            var addedBlogPost = await _dataStore.AddBlogPost(blogPostDto);

            return request.BlogPost;
        }


    }
}
