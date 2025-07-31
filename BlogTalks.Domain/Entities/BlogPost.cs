using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace BlogTalks.Domain.Entities
{
    public class BlogPost
    {
        private Guid Id { get; set; }
        private string Title { get; set; }
        private string Text { get; set; }

        private User CreatedBy { get; set; }
        private DateTime Timestamp { get; set; }
        private List<Tag> Tags { get; set; }

    }
}
