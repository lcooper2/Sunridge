using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sunridge.DataAccess.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        protected readonly ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            ApplicationUser = new ApplicationUserRepository(_db);
            Banner = new BannerRepository(_db);
            LostAndFound = new LostAndFoundRepository(_db);
            Key = new KeyRepository(_db);
            KeyHistory = new KeyHistoryRepository(_db);
            Lot = new LotRepository(_db);
            Board = new BoardRepository(_db);
            BlogThread = new BlogThreadRepository(_db);
            BlogComment = new BlogCommentRepository(_db);
            BlogImage = new BlogImageRepository(_db);
            BlogLike = new BlogLikeRepository(_db);
            BlogReply = new BlogReplyRepository(_db);
        }

        public IApplicationUserRepository ApplicationUser { get; private set; }
        public IBannerRepository Banner { get; private set; }
        public ILostAndFoundRepository LostAndFound { get; private set; }
        public IAddressRepository Address { get; private set; }
        public IBoardRepository Board { get; private set; }

        public IKeyRepository Key { get; private set; }
        public IKeyHistoryRepository KeyHistory { get; private set; }
        public ILotRepository Lot { get; private set; }
        public IBlogThreadRepository BlogThread { get; private set; }
        public IBlogCommentRepository BlogComment { get; private set; }
        public IBlogImageRepository BlogImage { get; private set; }
        public IBlogLikeRepository BlogLike { get; private set; }
        public IBlogReplyRepository BlogReply { get; private set; }

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
