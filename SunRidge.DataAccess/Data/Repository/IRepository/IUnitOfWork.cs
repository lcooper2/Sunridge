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
        IAddressRepository Address { get; }
        IKeyRepository Key { get; }
        IKeyHistoryRepository KeyHistory { get; }
        ILotRepository Lot { get; }
        IBoardRepository Board { get; }
        IBlogThreadRepository BlogThread { get; }
        IBlogCommentRepository BlogComment { get; }
        IBlogImageRepository BlogImage { get; }
        IBlogLikeRepository BlogLike { get; }
        IBlogReplyRepository BlogReply { get; }

        void Save();
    }
}
