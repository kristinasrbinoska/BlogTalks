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

        public IEnumerable<BlogPost> GetAllWithComments(int? pageNumber, int? pageSize, string? searchWord, string? tag)
        {
            var query = _dbSet.AsQueryable();

            if (!string.IsNullOrEmpty(searchWord))
            {
                query = query.Where(bp => bp.Title.Contains(searchWord) || bp.Text.Contains(searchWord));
            }
            if (!string.IsNullOrEmpty(tag))
            {
                query = query.Where(bp => bp.Tags.Contains(tag));
            }
            if (pageNumber.HasValue && pageSize.HasValue)
            {
                query = query.Skip((pageNumber.Value - 1) * pageSize.Value).Take(pageSize.Value);
            }
            return query;

        }
        public int GetTotalNumber()
        {
            return _dbSet.Count();
        }
    }
}
