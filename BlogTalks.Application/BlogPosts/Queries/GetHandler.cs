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
    public class GetHandler : IRequestHandler<GetRequest, IEnumerable<GetResponse>>
    {
        private readonly FakeDataStore _dataStore;
        public GetHandler(FakeDataStore dataStore)
        {
            _dataStore = dataStore;
        }
        public async Task<IEnumerable<GetResponse>> Handle(GetRequest request, CancellationToken cancellationToken)
        {
            var blogPosts = await _dataStore.GetAllBlogPosts();

            var response = blogPosts.Select(b => new GetResponse
            {
                Id = b.Id,
                Title = b.Title,
                Text = b.Text,
                Timestamp = b.Timestamp,
                CreatedBy = b.CreatedBy,
                Tags = b.Tags,
                Comments = b.Comments
     
            });
            return response;
        }
    }
}
