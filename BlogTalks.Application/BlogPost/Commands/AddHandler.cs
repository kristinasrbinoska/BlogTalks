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
    public class AddBlogPostHandler : IRequestHandler<AddRequest, AddResponse>
    {
        private readonly IBlogPostRepository _blogPostRepository;

        public AddBlogPostHandler(IBlogPostRepository blogPostRepository)
        {
            _blogPostRepository = blogPostRepository;
        }

        public async Task<AddResponse> Handle(AddRequest request, CancellationToken cancellationToken)
        {
            var blogPost = new Domain.Entities.BlogPost
            {
                Title = request.Title,
                Text = request.Text,
                CreatedBy = 5,
                CreatedAt = DateTime.UtcNow,
                Tags = request.Tags,
            };

             _blogPostRepository.Add(blogPost);

            return new AddResponse(blogPost.Id);
        }


    }
}
