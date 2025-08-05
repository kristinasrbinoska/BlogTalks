using BlogTalks.Domain.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogTalks.Application.Comments.Comands
{
    public class DeleteHandler : IRequestHandler<DeleteRequest, DeleteResponse>
    {
        private readonly FakeDataStore _dataStore;

        public DeleteHandler(FakeDataStore dataStore)
        {
            _dataStore = dataStore;
        }

        public async Task<DeleteResponse> Handle(DeleteRequest request, CancellationToken cancellationToken)
        {
            var comment = await _dataStore.GetCommentById(request.id);
            if (comment == null)
            {
                return null;
            }
            await _dataStore.DeleteComment(request.id);
            return new DeleteResponse
            {
                Id = request.id,
                Text = comment.Text,
                CreatedAt = comment.CreatedAt,
                CreatedBy = comment.CreatedBy,
                BlogPostId = comment.BlogPostId
            };
        }
    }
}
