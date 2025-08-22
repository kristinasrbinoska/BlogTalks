using BlogTalks.EmailSenderApi.DTO;
using BlogTalks.EmailSenderApi.Services;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddOpenApi();

builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IEmailSender, EmailSender>();

builder.Services.Configure<RabbitMQSettingseEmailSender>(builder.Configuration.GetSection("RabbitMQSettings"));
builder.Services.AddSingleton(builder.Configuration
    .GetSection("RabbitMQSettings")
    .Get<RabbitMQSettingseEmailSender>());

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapPost("/send", async (EmailDTO request, IEmailSender emailSender) =>
{
    await emailSender.Send(request);
    return Results.Ok(new { message = "Email sent successfully!" });
});

app.Run();
