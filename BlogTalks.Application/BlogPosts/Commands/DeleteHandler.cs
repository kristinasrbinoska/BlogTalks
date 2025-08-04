using BlogTalks.Domain.DTOs;
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
        private readonly FakeDataStore _dataStore;

        public DeleteBlogPostHandler(FakeDataStore dataStore)
        {
            _dataStore = dataStore;
        }

        public async Task<DeleteResponse> Handle(DeleteRequest request, CancellationToken cancellationToken)
        {
            var blogPost = await _dataStore.GetBlogPostById(request.id);
            if (blogPost == null)
            {
                return null;
            }
            _dataStore.DeleteBlogPost(request.id);
            return new DeleteResponse
            {
                Id = request.id,
                Title = blogPost.Title,
                Text = blogPost.Text,
                Timestamp = blogPost.Timestamp,
                CreatedBy = blogPost.CreatedBy
            };
        }
    }
}
