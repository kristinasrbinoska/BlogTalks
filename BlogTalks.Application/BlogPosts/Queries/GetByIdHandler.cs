﻿using BlogTalks.Application.Comments.Queries;
using BlogTalks.Domain.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogTalks.Application.BlogPosts.Queries
{
    public class GetByIdHandler : IRequestHandler<GetByIdRequest, GetByIdResponse>
    {
        private readonly FakeDataStore _dataStore;

        public GetByIdHandler(FakeDataStore dataStore)
        {
            _dataStore = dataStore;
        }

        public async Task<GetByIdResponse> Handle(GetByIdRequest request, CancellationToken cancellationToken)
        {
            var blogPost = await _dataStore.GetBlogPostById(request.id);
            if (blogPost == null)
            {
                return null;
            }
            return new GetByIdResponse
            {
                Id = blogPost.Id,
                Title = blogPost.Title,
                Text = blogPost.Text,
                CreatedBy = blogPost.CreatedBy,
                Timestamp = blogPost.Timestamp,
                Tags = blogPost.Tags,
                
            };
        }
    }
}
