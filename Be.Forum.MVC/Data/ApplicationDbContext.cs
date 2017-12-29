using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Be.Forum.MVC.Models;
using Be.Forum.MVC.Data.Entities;

namespace Be.Forum.MVC.Data {
  public class ApplicationDbContext : IdentityDbContext<ApplicationUser> {
    public DbSet<Post> Posts { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) {
    }

    protected override void OnModelCreating(ModelBuilder builder) {
      base.OnModelCreating(builder);
      // Customize the ASP.NET Identity model and override the defaults if needed.
      // For example, you can rename the ASP.NET Identity table names and more.
      // Add your customizations after calling base.OnModelCreating(builder);
    }
  }
}
