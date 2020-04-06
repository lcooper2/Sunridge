using System;
using System.Collections.Generic;
using System.Text;

namespace Sunridge.DataAccess.Data.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IApplicationUserRepository ApplicationUser { get; }
        IBannerRepository Banner { get; }
        ILostAndFoundRepository LostAndFound { get; }
        ILostAndFoundImageRepository LostAndFoundImage { get; }
        IAddressRepository Address { get; }
        IKeyRepository Key { get; }
        IKeyHistoryRepository KeyHistory { get; }
        ILotRepository Lot { get; }
        IBoardRepository Board { get; }
        IInKindWorkHoursRepository InKindWorkHours { get; }
        IFormResponseRepository FormResponse { get ;}
        ILotHistoryRepository LotHistory { get; }
        IFileRepository File { get; }
        ICommentRepository Comment { get; }
        IOwnerLotRepository OwnerLot { get; }
        IClassifiedCategoryRepository ClassifiedCategory { get; }
        IClassifiedListingRepository ClassifiedListing { get; }
        IClassifiedImageRepository ClassifiedImage { get; }

        IClaimLossRepository ClaimLoss { get; }
        ICommonAreaAssetRepository CommonAreaAsset { get; }
        IBlogThreadRepository BlogThread { get; }
        IBlogCommentRepository BlogComment { get; }
        IBlogImageRepository BlogImage { get; }
        IBlogLikeRepository BlogLike { get; }
        IBlogReplyRepository BlogReply { get; }
        IMaintenanceRepository Maintenance { get; }
        ISuggestionComplaintRepository SuggestionComplaint { get; }
        IScheduledEventRepository ScheduledEvent { get; }
        void Save();
    }
}
