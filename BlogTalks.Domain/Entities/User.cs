using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogTalks.Domain.Entities
{
    public class User
    {
        private Guid Id { get; set; }
        private string surname { get; set; }
        private string Username { get; set; }
        private string Password { get; set; }
        private string Email { get; set; }
        private List<BlogPost> BlogPosts { get; set; }

    }
}
