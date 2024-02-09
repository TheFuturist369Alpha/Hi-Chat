using Hubs;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Services.ChatService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSingleton<ChatServe>();
builder.Services.AddSignalR();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(build =>
    {
        build.WithOrigins("http://localhost:4200");
        build.WithHeaders("Authorization", "origin", "accept", "content-type", "Access-Control-Allow-Origin");
        build.WithMethods("GET", "POST", "PUT", "DELETE");
        build.AllowCredentials();
    });

});



var app = builder.Build();

//app.UseCors(cors => cors.WithHeaders("Authorization", "origin", "accept", "content-type")
//.WithMethods("GET", "POST", "PUT", "DELETE").AllowCredentials().WithOrigins("http://localhost:4200"));
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}

app.UseCors();
app.UseRouting();

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();
app.MapHub<ChatHub>("/chat");

app.Run();
