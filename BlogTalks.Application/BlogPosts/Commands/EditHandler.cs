using BlogTalks.Application.Comments.Queries;
using BlogTalks.Domain.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogTalks.Application.BlogPosts.Commands
{
    public class EditHandler : IRequestHandler<EditRequest, EditBlogPostResponse>
    {
        private readonly FakeDataStore _dataStore;
        public EditHandler(FakeDataStore dataStore)
            {
                _dataStore = dataStore;
            }
        public async Task<EditBlogPostResponse> Handle(EditRequest request, CancellationToken cancellationToken)
        {
            var blogPost = await _dataStore.GetBlogPostById(request.BlogPost.Id);

            if (blogPost == null)
            {
                return null;
            }
            blogPost.Title = request.BlogPost.Title;
            blogPost.Text = request.BlogPost.Text;
            blogPost.Timestamp = DateTime.Now;

            await _dataStore.UpdateBlogPost(request.BlogPost.Id, blogPost);

            
            return new EditBlogPostResponse
            {
                Id = blogPost.Id,
                Title = blogPost.Title,
                Text = blogPost.Text,
                CreatedBy = blogPost.CreatedBy,
                Timestamp = blogPost.Timestamp,
                Tags = blogPost.Tags,
                Comments = blogPost.Comments?.Select(c => new GetResponse
                {
                    Id = c.Id,
                    Text = c.Text,
                    CreatedBy = c.CreatedBy,
                    CreatedAt = c.CreatedAt
                }).ToList() ?? new List<GetResponse>()
            };
        }
    }
}
