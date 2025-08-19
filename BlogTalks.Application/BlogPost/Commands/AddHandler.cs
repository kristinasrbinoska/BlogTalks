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
    public class AddBlogPostHandler : IRequestHandler<AddRequest, AddResponse>
    {
        private readonly IBlogPostRepository _blogPostRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AddBlogPostHandler(IBlogPostRepository blogPostRepository, IHttpContextAccessor httpContextAccessor)
        {
            _blogPostRepository = blogPostRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<AddResponse> Handle(AddRequest request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirst("userId")?.Value;
            int userIdValue = 0;
            if (int.TryParse(userId, out int parsedUserId))
            {
                userIdValue = parsedUserId;
            }
            var blogPost = new Domain.Entities.BlogPost
            {
                Title = request.Title,
                Text = request.Text,
                CreatedBy = userIdValue,
                CreatedAt = DateTime.UtcNow,
                Tags = request.Tags,
                
            };

            _blogPostRepository.Add(blogPost);

            return new AddResponse(blogPost.Id);
        }


    }
}
