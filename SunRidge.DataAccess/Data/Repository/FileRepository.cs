using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sunridge.DataAccess.Data.Repository
{
    public class FileRepository : Repository<File>, IFileRepository
    {
        private readonly ApplicationDbContext _db;

        public FileRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(File file)
        {
            var obJFromDb = _db.File.FirstOrDefault(s => s.Id == file.Id);

            obJFromDb.FileURL = file.FileURL;
            obJFromDb.Name = file.Name;
            obJFromDb.Description = file.Description;
            obJFromDb.LotHistoryId = file.LotHistoryId;
            obJFromDb.LotHistory = file.LotHistory;



            _db.SaveChanges();
        }
    }
}
