using BlogTalks.Application.BlogPosts.Queries;
using BlogTalks.Application.Comments.Comands;
using BlogTalks.Application.Comments.Queries;
using BlogTalks.Domain.DTOs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen(options =>
{
    options.CustomSchemaIds(type => type.FullName);
});
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(BlogTalks.Application.Comments.Queries.GetResponse).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(EditResponse).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(DeleteResponse).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(BlogTalks.Application.BlogPosts.Queries.GetResponse).Assembly);
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
