using Sunridge.DataAccess.Data.Repository.IRepository;
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

        }

        public IApplicationUserRepository ApplicationUser { get; private set; }
        public IBannerRepository Banner { get; private set; }


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
