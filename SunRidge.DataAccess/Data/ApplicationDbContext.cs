using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Sunridge.Models;

namespace Sunridge.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ApplicationUser> ApplicationUser {get; set;}
        public DbSet<Banner> Banner { get; set; }
        public DbSet<LostAndFound> LostAndFound { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<Board> Board { get; set; }
    }
}
