using BlogTalks.Application.BlogPosts.Queries;
using BlogTalks.Application.Comments.Comands;
using BlogTalks.Application.Comments.Queries;
using BlogTalks.Domain.DTOs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(GetCommentsResponse).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(EditCommentResponse).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(DeleteCommentResponse).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(GetBlogPostsResponse).Assembly);
});
builder.Services.AddSingleton<FakeDataStore>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
