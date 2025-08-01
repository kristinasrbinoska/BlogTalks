using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogTalks.Application.BlogPosts.Commands
{
    public record AddBlogPostRequest(AddBlogPostResponse BlogPost) : IRequest<AddBlogPostResponse>;
}
