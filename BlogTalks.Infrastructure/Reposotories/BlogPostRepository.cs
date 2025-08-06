using BlogTalks.Domain.Entities;
using BlogTalks.Domain.Reposotories;
using BlogTalks.Infrastructure.Data.DataContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogTalks.Infrastructure.Reposotories
{
    public class BlogPostRepository : GenericRepository<BlogPost>, IBlogPostRepository
    {
        public BlogPostRepository(ApplicationDbContext context) : base(context, context.BlogPosts)
        {
           
        }
        public IEnumerable<BlogPost> GetAllWithComments()
        {
            return _dbSet
                .Include(b => b.Comments)
                .ToList();
        }
    }
}
