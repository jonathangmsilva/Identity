using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProfileApi.Database;
using ProfileApi.Extensions;
using ProfileApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthorization();
builder.Services.AddAuthentication().AddCookie(IdentityConstants.ApplicationScheme);


builder.Services.AddIdentityCore<User>()
  .AddEntityFrameworkStores<ApplicationDbContext>()
  .AddApiEndpoints();


// Add DbContext sqlite
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
  options.UseSqlite("Data Source=app.db");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();

  app.ApplyMigrations();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapIdentityApi<User>();

app.Run();
