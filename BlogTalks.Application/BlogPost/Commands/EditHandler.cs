using BlogTalks.Application.Comments.Queries;
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
    public class EditHandler : IRequestHandler<EditRequest, EditBlogPostResponse>
    {
        private readonly IBlogPostRepository _blogPostRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public EditHandler(IBlogPostRepository blogPostRepository, IHttpContextAccessor httpContextAccessor)
        {
            _blogPostRepository = blogPostRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public Task<EditBlogPostResponse> Handle(EditRequest request, CancellationToken cancellationToken)
        {
            var blogPost =_blogPostRepository.GetById(request.Id);

            if (blogPost == null)
            {
                return null;
            }
            var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst("userId")?.Value;
            if (!int.TryParse(userIdClaim, out int currentUserId))
            {
                return null;
            }

           
            if (blogPost.CreatedBy != currentUserId)
            {
                return null;
            }
            blogPost.Title = request.Title;
            blogPost.Text = request.Text;
            blogPost.CreatedAt = DateTime.UtcNow;


            _blogPostRepository.Update(blogPost);


            return Task.FromResult(new EditBlogPostResponse
            {
                Id = blogPost.Id,
                Title = blogPost.Title,
                Text = blogPost.Text,
                CreatedBy = blogPost.CreatedBy,
                Timestamp = DateTime.UtcNow,
                Tags = blogPost.Tags,
            });
        }
    }
}
