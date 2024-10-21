using Identity.Database;
using Identity.Extensions;
using Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthorization();
builder.Services.AddAuthentication()
  .AddCookie(IdentityConstants.ApplicationScheme);


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


//-------------------------------------------------------------------

app.MapGet("users/me", async (ClaimsPrincipal claims, ApplicationDbContext context) =>
{

  string userId = claims.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
  var user = await context.Users.FindAsync(userId);
  return Results.Ok(user);

}).RequireAuthorization();

//-------------------------------------------------------------------


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapIdentityApi<User>();

app.Run();
