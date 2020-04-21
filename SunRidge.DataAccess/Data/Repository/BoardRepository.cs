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
            if (board.Image != null)
            {
                objFromDB.Image = board.Image;
            }
            objFromDB.Phone = board.Phone;
            objFromDB.Email = board.Email;
            objFromDB.DisplayOrder = board.DisplayOrder;
            objFromDB.FirstName = board.FirstName;
            objFromDB.LastName = board.LastName;
            
            _db.SaveChanges();
           
        }
    }
}
