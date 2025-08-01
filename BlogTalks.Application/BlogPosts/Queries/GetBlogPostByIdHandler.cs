using BlogTalks.Application.Comments.Queries;
using BlogTalks.Domain.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogTalks.Application.BlogPosts.Queries
{
    public class GetBlogPostByIdHandler : IRequestHandler<GetBlogPostByIdRequest, GetBlogPostByIdResponse>
    {
        private readonly FakeDataStore _dataStore;

        public GetBlogPostByIdHandler(FakeDataStore dataStore)
        {
            _dataStore = dataStore;
        }

        public async Task<GetBlogPostByIdResponse> Handle(GetBlogPostByIdRequest request, CancellationToken cancellationToken)
        {
            var blogPost = await _dataStore.GetBlogPostById(request.id);
            if (blogPost == null)
            {
                return null;
            }
            return new GetBlogPostByIdResponse
            {
                Id = blogPost.Id,
                Title = blogPost.Title,
                Text = blogPost.Text,
                CreatedBy = blogPost.CreatedBy,
                Timestamp = blogPost.Timestamp,
                Tags = blogPost.Tags,
                Comments = blogPost.Comments.Select(c => new GetCommentsResponse
                {
                    Id = c.Id,
                    Text = c.Text,
                    CreatedAt = c.CreatedAt,
                    CreatedBy = c.CreatedBy,
                    BlogPostId = c.BlogPostId
                }).ToList()
            };
        }
    }
}
