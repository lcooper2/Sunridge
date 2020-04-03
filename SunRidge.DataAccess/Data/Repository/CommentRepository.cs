using Microsoft.EntityFrameworkCore;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;
using System.Linq;

namespace Sunridge.DataAccess.Data.Repository
{
    internal class CommentRepository : Repository<Comment>, ICommentRepository
    {
        private readonly ApplicationDbContext _db;

        public CommentRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Comment comment)
        {
            var obJFromDb = _db.Comment.FirstOrDefault(s => s.Id == comment.Id);

            obJFromDb.Content = comment.Content;
            obJFromDb.Date = comment.Date;
            obJFromDb.Private = comment.Private;
            //obJFromDb.LotHistory = comment.LotHistory;
            //obJFromDb.LotHistoryId = comment.LotHistoryId;
            obJFromDb.FormResponse = comment.FormResponse;
            obJFromDb.FormResponseId = comment.FormResponseId;



            _db.SaveChanges();


        }
    }
}