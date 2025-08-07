using BlogTalks.Domain.DTOs;
using BlogTalks.Domain.Reposotories;
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
        private readonly ICommentRepository _commentRepository;

        public DeleteHandler(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public  Task<DeleteResponse> Handle(DeleteRequest request, CancellationToken cancellationToken)
        {
            var comment = _commentRepository.GetById(request.id);
            if (comment == null)
            {
                return null;
            }
            _commentRepository.Delete(comment);
            return Task.FromResult(new DeleteResponse
            {
                Id = request.id,
                Text = comment.Text,
                CreatedAt = comment.CreatedAt,
                CreatedBy = comment.CreatedBy,
                BlogPostId = comment.BlogPostId
            });
        }
    }
}
