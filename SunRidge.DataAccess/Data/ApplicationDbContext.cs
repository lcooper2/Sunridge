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
        public DbSet<LostAndFoundImage> LostAndFoundImage { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<Key> Key { get; set; }
        public DbSet<KeyHistory> KeyHistory { get; set; }
        public DbSet<Lot> Lot { get; set; }
        public DbSet<LotHistory> LotHistory { get; set; }
        public DbSet<Board> Board { get; set; }
        public DbSet<InKindWorkHours> InKindWorkHours { get; set; }
        public DbSet<FormResponse> FormResponse { get; set; }
        public DbSet<File> File { get; set; }
       // public DbSet<Comment> Comment { get; set; }
        public DbSet<OwnerLot> OwnerLot { get; set; }
        public DbSet<CommonAreaAsset> CommonAreaAssets { get; set; }
        public DbSet<ClassifiedListing> ClassifiedListing { get; set; }
        public DbSet<ClassifiedCategory> ClassifiedCategory { get; set; }
        public DbSet<ClassifiedImage> ClassifiedImage { get; set; }
        public DbSet<BlogThread> BlogThread { get; set; }
        public DbSet<BlogComment> BlogComment { get; set; }
        public DbSet<BlogImage> BlogImage { get; set; }
        public DbSet<BlogLike> BlogLike { get; set; }
        public DbSet<BlogReply> BlogReply { get; set; }
        public DbSet<Maintenance> Maintenance { get; set; }
        public DbSet<ClaimLoss> ClaimLoss { get; set; }
        public DbSet<SuggestionComplaint> SuggestionComplaint { get; set; }
        public DbSet<ScheduledEvent> ScheduledEvent { get; set; }
        public DbSet<FormSubmissions> FormSubmissions { get; set; }
        public DbSet<NewsItem> NewsItem { get; set; }
    }
}
