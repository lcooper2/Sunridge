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
        IThreadRepository Thread { get; }
        ICommentRepository Comment { get; }
        IBlogImageRepository BlogImage { get; }
        ILikeRepository Like { get; }
        IReplyRepository Reply { get; }

        void Save();
    }
}
