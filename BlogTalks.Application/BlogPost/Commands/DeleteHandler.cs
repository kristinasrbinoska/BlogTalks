using BlogTalks.Domain.DTOs;
using BlogTalks.Domain.Reposotories;
using MediatR;
using Microsoft.AspNetCore.Http;
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
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DeleteBlogPostHandler(IBlogPostRepository blogPostRepository, IHttpContextAccessor httpContextAccessor)
        {
            _blogPostRepository = blogPostRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public Task<DeleteResponse> Handle(DeleteRequest request, CancellationToken cancellationToken)
        {
            var blogPost = _blogPostRepository.GetById(request.id);
            if (blogPost == null)
            {
                return null;
            }
            var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst("userId")?.Value;
            if (!int.TryParse(userIdClaim, out int currentUserId))
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
