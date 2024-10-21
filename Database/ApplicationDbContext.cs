using Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Identity.Database
{
  public class ApplicationDbContext : IdentityDbContext<User>
  {
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
      base.OnModelCreating(builder);

      builder.Entity<User>(entity =>
      {
        entity.Property(e => e.FirstName).HasMaxLength(100);
        entity.Property(e => e.LastName).HasMaxLength(100);
      });
    }
  }
}
