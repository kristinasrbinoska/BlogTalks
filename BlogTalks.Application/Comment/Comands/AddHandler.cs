using BlogTalks.Application.Comments.Queries;
using BlogTalks.Domain.DTOs;
using BlogTalks.Domain.Entities;
using BlogTalks.Domain.Reposotories;
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
        private readonly ICommentRepository _commentRepository;
        private readonly IBlogPostRepository _blogPostRepository;

        public AddHandler(ICommentRepository commentRepository, IBlogPostRepository blogPostRepository)
        {
            _commentRepository = commentRepository;
            _blogPostRepository = blogPostRepository;
        }

        public async Task<AddResponse> Handle(AddRequest request, CancellationToken cancellationToken)
        {
            var blogPost = _blogPostRepository.GetById(request.BlogPostId);
            if(blogPost == null)
            {
                return null; 
            }

            var comment = new Comment
            {
                Text = request.Text,
                CreatedBy = 5,
                CreatedAt = DateTime.UtcNow,
                BlogPostId = request.BlogPostId,
                BlogPost = blogPost
            };

            
            _commentRepository.Add(comment);


            return new AddResponse(comment.Id);
        }

    }

}
