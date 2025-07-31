using BlogTalks.Domain.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogTalks.Application.Comments.Queries
{
    public class GetCommentByIdHandler : IRequestHandler<GetCommentByIdRequest, GetCommentByIdResponse>
    {
        private readonly FakeDataStore _dataStore;

        public GetCommentByIdHandler(FakeDataStore dataStore)
        {
            _dataStore = dataStore;
        }

        public async Task<GetCommentByIdResponse> Handle(GetCommentByIdRequest request, CancellationToken cancellationToken)
        {
            var comment = await _dataStore.GetCommentById(request.id);

            return new GetCommentByIdResponse
            {
                Id = comment.Id,
                Text = comment.Text,
                CreatedAt = comment.CreatedAt,
                CreatedBy = comment.CreatedBy,
                BlogPostId = comment.BlogPostId
            };
        }
    
    }
}
