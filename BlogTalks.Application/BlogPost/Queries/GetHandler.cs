using BlogTalks.Application.Comments.Queries;
using BlogTalks.Domain.DTOs;
using BlogTalks.Domain.Reposotories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogTalks.Application.BlogPosts.Queries
{
    public class GetHandler : IRequestHandler<GetRequest, IEnumerable<GetResponse>>
    {
        private readonly IBlogPostRepository _blogPostRepository;

        public GetHandler(IBlogPostRepository blogPostRepository)
        {
            _blogPostRepository = blogPostRepository;
        }

        public Task<IEnumerable<GetResponse>> Handle(GetRequest request, CancellationToken cancellationToken)
        {
            var blogPosts = _blogPostRepository.GetAllWithComments();
        
            var response = blogPosts.Select(b => new GetResponse
            {
                Id = b.Id,
                Title = b.Title,
                Text = b.Text,
                Timestamp = b.CreatedAt,
                CreatedBy = b.CreatedBy,
                Tags = b.Tags,
                Comments = b.Comments.Select(c => new CommentDTO
                {
                    Id = c.Id,
                    Text = c.Text,
                    CreatedAt = c.CreatedAt,
                    CreatedBy = c.CreatedBy,
                    BlogPostId = c.BlogPostId
                }).ToList()
            });
            return Task.FromResult(response);
        }
    }
}
