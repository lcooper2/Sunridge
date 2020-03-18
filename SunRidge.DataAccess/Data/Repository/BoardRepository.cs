using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sunridge.DataAccess.Data.Repository
{
    public class BoardRepository : Repository<Board>, IBoardRepository
    {
        private readonly ApplicationDbContext _db;

        public BoardRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Board board)
        {
            var objFromDB = _db.Board.FirstOrDefault(s => s.Id == board.Id);

            objFromDB.Title = board.Title;
            objFromDB.Name = board.Name;
            objFromDB.Image = board.Image;
            objFromDB.PhoneNumber = board.PhoneNumber;
            objFromDB.Email = board.Email;
            _db.SaveChanges();
        }
    }
}
