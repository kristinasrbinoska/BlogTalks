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
    public class GetBlogPostsHandler : IRequestHandler<GetBlogPostsRequest, IEnumerable<GetBlogPostsResponse>>
    {
        private readonly FakeDataStore _dataStore;
        public GetBlogPostsHandler(FakeDataStore dataStore)
        {
            _dataStore = dataStore;
        }
        public async Task<IEnumerable<GetBlogPostsResponse>> Handle(GetBlogPostsRequest request, CancellationToken cancellationToken)
        {
            var blogPosts = await _dataStore.GetAllBlogPosts();

            var response = blogPosts.Select(b => new GetBlogPostsResponse
            {
                Id = b.Id,
                Title = b.Title,
                Text = b.Text,
                Timestamp = b.Timestamp,
                CreatedBy = b.CreatedBy,
                
                Comments = b.Comments.Select(c => new GetCommentsResponse
                {
                    Id = c.Id,
                    Text = c.Text,
                    CreatedAt = c.CreatedAt,
                    CreatedBy = c.CreatedBy,
                    BlogPostId = c.BlogPostId
                }).ToList()
            });
            return response;
        }
    }
}
