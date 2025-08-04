using BlogTalks.Domain.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogTalks.Application.Comments.Queries
{
    public class GetByBlogPostIdHandler : IRequestHandler<GetByBlogPostIdRequest, IEnumerable<GetByBlogPostIdResponse>>
    {
        private readonly FakeDataStore _dataStore;

        public GetByBlogPostIdHandler(FakeDataStore dataStore)
        {
            _dataStore = dataStore;
        }

        public async Task<IEnumerable<GetByBlogPostIdResponse>> Handle(GetByBlogPostIdRequest request, CancellationToken cancellationToken)
        {
            var comments = await _dataStore.GetByBlogPostId(request.blogPostId);

            return comments.Select(c => new GetByBlogPostIdResponse
            {
                Id = c.Id,
                Text = c.Text,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = c.CreatedBy,
                BlogPostId = c.BlogPostId,
            });
        }
    }
}
