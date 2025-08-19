using BlogTalks.Domain.Entities;
using BlogTalks.Domain.Reposotories;
using BlogTalks.Infrastructure.Data.DataContext;
using Microsoft.EntityFrameworkCore;

namespace BlogTalks.Infrastructure.Repositories
{
    public class BlogPostRepository : GenericRepository<BlogPost>, IBlogPostRepository
    {
        public BlogPostRepository(ApplicationDbContext context) : base(context,context.BlogPosts) { }

        public BlogPost? GetBlogPostByName(string name)
        {
            return _dbSet.FirstOrDefault(p => p.Title.Equals(name));
        }

        public IQueryable<BlogPost> Query() => _context.BlogPosts.AsQueryable();

        public async Task<(int count, List<BlogPost> list)> GetPagedAsync(
            int? pageNumber,
            int? pageSize,
            string? searchWord,
            string? tag)
        {
            var pgNum = pageNumber ?? 1;  
            var pgSize = pageSize ?? 10;   

            var query = _context.BlogPosts.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchWord))
                query = query.Where(bp => bp.Title.ToLower().Contains(searchWord.ToLower()) || bp.Text.ToLower().Contains(searchWord.ToLower()));

            if (!string.IsNullOrWhiteSpace(tag))
                query = query.Where(bp => bp.Tags.Contains(tag));

            var count = await query.CountAsync();

            var list = await query
                .Skip((pgNum - 1) * pgSize)
                .Take(pgSize)
                .ToListAsync();

            return (count, list);
        }

        public int GetTotalNumber()
        {
            return _dbSet.Count();
        }
    }
}