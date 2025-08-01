using BlogTalks.Domain.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogTalks.Application.Comments.Comands
{
    public class DeleteCommentHandler : IRequestHandler<DeleteCommentRequest, DeleteCommentResponse>
    {
        private readonly FakeDataStore _dataStore;

        public DeleteCommentHandler(FakeDataStore dataStore)
        {
            _dataStore = dataStore;
        }

        public async Task<DeleteCommentResponse> Handle(DeleteCommentRequest request, CancellationToken cancellationToken)
        {
            var comment = await _dataStore.GetCommentById(request.id);
            if (comment == null)
            {
                return null;
            }
            await _dataStore.DeleteComment(request.id);
            return new DeleteCommentResponse
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
