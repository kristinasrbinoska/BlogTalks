﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogTalks.Application.BlogPosts.Commands
{
    public record AddRequest(string Title,string Text, List<string> Tags) : IRequest<AddResponse>;
}
