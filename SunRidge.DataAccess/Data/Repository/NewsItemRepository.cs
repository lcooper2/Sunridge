using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sunridge.DataAccess.Data.Repository
{
    public class NewsItemRepository : Repository<NewsItem>, INewsItemRepository
    {
        private readonly ApplicationDbContext _db;

        public NewsItemRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(NewsItem newsItem)
        {
            var objFromDB = _db.NewsItem.FirstOrDefault(s => s.NewsItemId == newsItem.NewsItemId);

            objFromDB.Content = newsItem.Content;
            objFromDB.FileName = newsItem.FileName;
            objFromDB.FilePath = newsItem.FilePath;
            objFromDB.Header = newsItem.Header;
            objFromDB.Year = newsItem.Year;
            
            _db.SaveChanges();
           
        }
    }
}
