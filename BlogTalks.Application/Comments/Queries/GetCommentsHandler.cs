using BlogTalks.Domain.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogTalks.Application.Comments.Queries
{
    public class GetCommentsHandler : IRequestHandler<GetCommentsRequest, IEnumerable<GetCommentsResponse>>
    {
        private readonly FakeDataStore _dataStore;

        public GetCommentsHandler(FakeDataStore dataStore)
        {
            _dataStore = dataStore;
        }

        public async Task<IEnumerable<GetCommentsResponse>> Handle(GetCommentsRequest request, CancellationToken cancellationToken)
        {
            var comments = await _dataStore.GetAllComments();

            var response = comments.Select(c => new GetCommentsResponse
            {
                Id = c.Id,
                Text = c.Text,
                CreatedAt = c.CreatedAt,
                CreatedBy = c.CreatedBy,
                BlogPostId = c.BlogPostId
            });

            return response;
        }

    }

}
