using BlogTalks.Domain.Reposotories;
using BlogTalks.Infrastructure.Data.DataContext;
using BlogTalks.Infrastructure.Reposotories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BlogTalks.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration  configuration)
        {
            var connectionString = configuration.GetConnectionString("Database");

            services.AddDbContext<ApplicationDbContext>(
                options => options
                .UseNpgsql(connectionString, npgsqlOptions =>
                npgsqlOptions.MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.Default)));

            services.AddDbContext<ApplicationDbContext>();
            
            services.AddTransient<IBlogPostRepository, BlogPostRepository>();
            services.AddTransient<ICommentRepository, CommentRepository>();
            return services;
        }
    }
}
