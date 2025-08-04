using BlogTalks.Application.Comments.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogTalks.Application.BlogPosts.Queries
{
    public record GetRequest(): IRequest<IEnumerable<GetResponse>>;

}
