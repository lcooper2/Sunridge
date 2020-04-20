using Microsoft.AspNetCore.Hosting;
using Sunridge.DataAccess.Data.Repository.IRepository;

namespace Sunridge.DataAccess.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        protected readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public UnitOfWork(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
            ApplicationUser = new ApplicationUserRepository(_db);
            Banner = new BannerRepository(_db);
            LostAndFound = new LostAndFoundRepository(_db);
            LostAndFoundImage = new LostAndFoundImageRepository(_db);
            Key = new KeyRepository(_db);
            KeyHistory = new KeyHistoryRepository(_db);
            Lot = new LotRepository(_db);
            LotHistory = new LotHistoryRepository(_db);
            Board = new BoardRepository(_db);
            InKindWorkHours = new InKindWorkHoursRepository(_db);
            FormResponse = new FormResponseRepository(_db);
            File = new FileRepository(_db);
            Comment = new CommentRepository(_db);
            OwnerLot = new OwnerLotRepository(_db);
            ClassifiedCategory = new ClassifiedCategoryRepository(_db);
            ClassifiedListing = new ClassifiedListingRepository(_db);
            ClassifiedImage = new ClassifiedImageRepository(_db);
            BlogThread = new BlogThreadRepository(_db, _webHostEnvironment);
            BlogComment = new BlogCommentRepository(_db);
            BlogImage = new BlogImageRepository(_db, _webHostEnvironment);
            BlogLike = new BlogLikeRepository(_db);
            BlogReply = new BlogReplyRepository(_db);
            ClaimLoss = new ClaimLossRepository(_db);
            CommonAreaAsset = new CommonAreaAssetRepository(_db);
            Maintenance = new MaintenanceRepository(_db);
            SuggestionComplaint = new SuggestionComplaintRepository(_db);
            ScheduledEvent = new ScheduledEventRepository(_db);
            FormSubmissions = new FormSubmissionsRepository(_db);
            NewsItem = new NewsItemRepository(_db);
        }

        public IApplicationUserRepository ApplicationUser { get; private set; }
        public IBannerRepository Banner { get; private set; }
        public ILostAndFoundRepository LostAndFound { get; private set; }
        public ILostAndFoundImageRepository LostAndFoundImage { get; private set; }
        public IAddressRepository Address { get; private set; }
        public IBoardRepository Board { get; private set; }

        public IKeyRepository Key { get; private set; }
        public IKeyHistoryRepository KeyHistory { get; private set; }
        public ILotRepository Lot { get; private set; }
        public ILotHistoryRepository LotHistory { get; private set; }

        public IInKindWorkHoursRepository InKindWorkHours { get; private set; }
        public IFormResponseRepository FormResponse {get; private set; }
        public IFileRepository File { get; private set; }
        public ICommentRepository Comment { get; private set; }
        public IOwnerLotRepository OwnerLot { get; private set; }
        public IClassifiedCategoryRepository ClassifiedCategory { get; private set; }
        public IClassifiedListingRepository ClassifiedListing { get; private set; }
        public IClassifiedImageRepository ClassifiedImage { get; private set; }
        public ICommonAreaAssetRepository CommonAreaAsset { get; private set; }
        public IBlogThreadRepository BlogThread { get; private set; }
        public IBlogCommentRepository BlogComment { get; private set; }
        public IBlogImageRepository BlogImage { get; private set; }
        public IBlogLikeRepository BlogLike { get; private set; }
        public IBlogReplyRepository BlogReply { get; private set; }

        public IClaimLossRepository ClaimLoss { get; private set; }

        public IMaintenanceRepository Maintenance { get; private set; }
        public ISuggestionComplaintRepository SuggestionComplaint { get; private set; }
        public IScheduledEventRepository ScheduledEvent { get; private set; }
        public IFormSubmissionsRepository FormSubmissions { get; private set; }
        public INewsItemRepository NewsItem { get; private set; }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
