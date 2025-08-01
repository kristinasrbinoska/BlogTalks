using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogTalks.Application.Comments.Comands
{
    public record EditCommentRequest(int id,EditCommentResponse Comment) : IRequest<EditCommentResponse>;

}
