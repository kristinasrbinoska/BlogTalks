using BlogTalks.Domain.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogTalks.Application.Comments.Comands
{
    public class EditHandler : IRequestHandler<EditRequest, EditResponse>
    {
        private readonly FakeDataStore _dataStore;

        public EditHandler(FakeDataStore dataStore)
        {
            _dataStore = dataStore;
        }

        public async Task<EditResponse> Handle(EditRequest request, CancellationToken cancellationToken)
        {
            var comment = await _dataStore.GetCommentById(request.Comment.Id);
            if (comment == null)
            {
                return null;
            }
            comment.Text = request.Comment.Text;
            comment.CreatedAt = DateTime.Now;

            await _dataStore.UpdateComment(request.Comment.Id, comment);


            return new EditResponse
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
