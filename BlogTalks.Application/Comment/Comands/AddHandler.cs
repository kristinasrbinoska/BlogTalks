using BlogTalks.Application.Comments.Queries;
using BlogTalks.Domain.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogTalks.Application.Comments.Comands
{
    public class AddHandler : IRequestHandler<AddRequest, AddResponse>
    {
        private readonly FakeDataStore _dataStore;

        public AddHandler(FakeDataStore dataStore)
        {
            _dataStore = dataStore;
        }

        public async Task<AddResponse> Handle(AddRequest request, CancellationToken cancellationToken)
        {
           
            var commentDto = new CommentDTO
            {
                Id = request.Comment.Id,
                Text = request.Comment.Text,
                CreatedAt = request.Comment.CreatedAt,
                CreatedBy = request.Comment.CreatedBy,
                BlogPostId = request.Comment.BlogPostId
            };

            await _dataStore.AddComment(commentDto);

            return request.Comment;
        }

    }

}
