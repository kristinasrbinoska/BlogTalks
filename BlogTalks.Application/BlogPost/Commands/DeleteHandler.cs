using BlogTalks.Domain.DTOs;
using BlogTalks.Domain.Reposotories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogTalks.Application.BlogPosts.Commands
{
    public class DeleteBlogPostHandler : IRequestHandler<DeleteRequest, DeleteResponse>
    {
        private readonly IBlogPostRepository _blogPostRepository;

        public DeleteBlogPostHandler(IBlogPostRepository blogPostRepository)
        {
            _blogPostRepository = blogPostRepository;
        }

        public Task<DeleteResponse> Handle(DeleteRequest request, CancellationToken cancellationToken)
        {
            var blogPost = _blogPostRepository.GetById(request.id);
            if (blogPost == null)
            {
                return null;
            }
            _blogPostRepository.Delete(blogPost);
            return Task.FromResult(new DeleteResponse
            {
                Id = request.id,
                Title = blogPost.Title,
                Text = blogPost.Text,
                Timestamp = blogPost.CreatedAt,
                CreatedBy = blogPost.CreatedBy
            });
        }
    }
}
