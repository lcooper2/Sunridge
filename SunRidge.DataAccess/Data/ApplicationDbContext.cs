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

        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<Banner> Banner { get; set; }
        public DbSet<LostAndFound> LostAndFound { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<Key> Key { get; set; }
        public DbSet<KeyHistory> KeyHistory { get; set; }
        public DbSet<Lot> Lot { get; set; }
        public DbSet<Board> Board { get; set; }
        public DbSet<BlogThread> BlogThread { get; set; }
        public DbSet<BlogComment> BlogComment { get; set; }
        public DbSet<BlogImage> BlogImage { get; set; }
        public DbSet<BlogLike> BlogLike { get; set; }
        public DbSet<BlogReply> BlogReply { get; set; }
        //public DbSet<InKindWorkHours> InKindWorkHours { get; set; }
        //public DbSet<CommonAreaAsset> CommonAreaAsset { get; set; }
    }
}