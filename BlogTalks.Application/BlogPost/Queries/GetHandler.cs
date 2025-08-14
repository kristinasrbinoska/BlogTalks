using BlogTalks.Application.Comments.Queries;
using BlogTalks.Application.Contracts;
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
    public class GetHandler : IRequestHandler<GetRequest, GetResponse>
    {
        private readonly IBlogPostRepository _blogPostRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICommentRepository _commentRepository;

        public GetHandler(IBlogPostRepository blogPostRepository, IUserRepository userRepository, ICommentRepository commentRepository)
        {
            _blogPostRepository = blogPostRepository;
            _userRepository = userRepository;
            _commentRepository = commentRepository;
        }

        public Task<GetResponse> Handle(GetRequest request, CancellationToken cancellationToken)
        {
            var blogPosts = _blogPostRepository.GetAllWithComments(
                request.PageNumber,
                request.PageSize,
                request.SearchWord,
                request.Tag
            );

            var blogPostModels = blogPosts.Select(bp => new BlogPostModel
            {
                Id = bp.Id,
                Title = bp.Title,
                Text = bp.Text,
                Tags = bp.Tags,
                Comments = bp.Comments.Select(c => new CommentModel
                {
                    Text = c.Text,
                    CreatedAt = c.CreatedAt,
                   CreatedBy = c.CreatedBy
                }).ToList()

            }).ToList();

            var userIds = blogPostModels.Select(bp => bp.Id).Distinct().ToList();
            var userList = _userRepository.GetUsersByIds(userIds);
            foreach (var blog in blogPostModels)
            {
                blog.CreatorName = userList.FirstOrDefault(u => u.Id == blog.Id)?.Name ?? string.Empty;
            }

            var count = _blogPostRepository.GetTotalNumber();

            var response = new GetResponse
            {
                BlogPosts = blogPostModels,
                Metadata = new Metadata
                {
                    PageNumber = request.PageNumber ?? 1,
                    PageSize = request.PageSize ?? blogPostModels.Count,
                    TotalCount = count,
                    TotalPages = (int)Math.Ceiling(((double)count / (request.PageSize ?? blogPostModels.Count + count - 1)))
                }
            };

            return Task.FromResult(response);
        }

    }
}
