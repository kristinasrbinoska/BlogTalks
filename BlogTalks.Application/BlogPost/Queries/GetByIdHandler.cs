﻿using BlogTalks.Application.Comments.Queries;
using BlogTalks.Domain.DTOs;
using BlogTalks.Domain.Exceptions.BlogTalks.Domain.Exceptions;
using BlogTalks.Domain.Reposotories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BlogTalks.Application.BlogPosts.Queries
{
    public class GetByIdHandler : IRequestHandler<GetByIdRequest, GetByIdResponse>
    {
        private readonly IBlogPostRepository _blogPostRepository;

        public GetByIdHandler(IBlogPostRepository blogPostRepository)
        {
            _blogPostRepository = blogPostRepository;
        }

        public Task<GetByIdResponse> Handle(GetByIdRequest request, CancellationToken cancellationToken)
        {
            var blogPost = _blogPostRepository.GetById(request.id);
            if (blogPost == null)
            {
               throw new BlogTalksException($"Blog post with Id {request.id} not found.", HttpStatusCode.NotFound);
            }
            return Task.FromResult(new GetByIdResponse
            {
                Id = blogPost.Id,
                Title = blogPost.Title,
                Text = blogPost.Text,
                CreatedBy = blogPost.CreatedBy,
                Timestamp = blogPost.CreatedAt,
                Tags = blogPost.Tags,              
            });
        }
    }
}
