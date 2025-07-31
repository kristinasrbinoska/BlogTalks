using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogTalks.Domain.Entities
{
    public class Comment
    {
        private Guid Id { get; set; }
        private string Text { get; set; }
        private User CreatedBy { get; set; }
        private DateTime Timestamp { get; set; }
        private BlogPost BlogPost { get; set; }
        private Guid BlogPostId { get; set; }
    }
}
