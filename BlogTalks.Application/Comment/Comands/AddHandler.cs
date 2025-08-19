using BlogTalks.Application.Comments.Queries;
using BlogTalks.Domain.DTOs;
using BlogTalks.Domain.Entities;
using BlogTalks.Domain.Reposotories;
using BlogTalks.EmailSenderApi.DTO;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Json;


namespace BlogTalks.Application.Comments.Comands
{
    public class AddHandler : IRequestHandler<AddRequest, AddResponse>
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IBlogPostRepository _blogPostRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IUserRepository _userRepository;

        public AddHandler(ICommentRepository commentRepository, IBlogPostRepository blogPostRepository, IHttpContextAccessor httpContextAccessor, IHttpClientFactory httpClientFactory, IUserRepository userRepository)
        {
            _commentRepository = commentRepository;
            _blogPostRepository = blogPostRepository;
            _httpContextAccessor = httpContextAccessor;
            _httpClientFactory = httpClientFactory;
            _userRepository = userRepository;
        }

        public async Task<AddResponse> Handle(AddRequest request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirst("userId")?.Value;
            int userIdValue = 0;
            if (int.TryParse(userId, out int parsedUserId))
            {
                userIdValue = parsedUserId;
            }
            var blogPost = _blogPostRepository.GetById(request.BlogPostId);
            if (blogPost == null)
            {
                return null;
            }

            var comment = new Domain.Entities.Comment
            {
                Text = request.Text,
                CreatedBy = userIdValue,
                CreatedAt = DateTime.UtcNow,
                BlogPostId = request.BlogPostId,
                BlogPost = blogPost
            };

            _commentRepository.Add(comment);
            var httpClient = _httpClientFactory.CreateClient("EmailSenderApi");
            var blogpostCreator = _userRepository.GetById(blogPost.CreatedBy);
            var commentCreator = _userRepository.GetById(userIdValue);

            EmailDTO dto = new EmailDTO
            {
                From = commentCreator.Email,
                To = blogpostCreator.Email,
                Subject = "New Comment Added",
                Body = $"A new comment has been added to the blog post '{blogPost.Title}' by user {commentCreator?.Name}."
            };

            await httpClient.PostAsJsonAsync("/send", dto, cancellationToken);

            return new AddResponse(comment.Id);
        }

    }

}
