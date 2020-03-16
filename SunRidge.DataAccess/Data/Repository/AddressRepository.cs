using System;
using System.Collections.Generic;
using System.Text;
using Sunridge.Models;
using Sunridge.DataAccess.Data.Repository.IRepository;
using System.Linq;

namespace Sunridge.DataAccess.Data.Repository
{
    public class AddressRepository : Repository<Address>, IAddressRepository
    {
        private readonly ApplicationDbContext _db;

        public AddressRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Address address)
        {
            var obJFromDb = _db.Address.FirstOrDefault(s => s.Id == address.Id);

            obJFromDb.StreetAddress = address.StreetAddress;
            obJFromDb.Apartment = address.Apartment;
            obJFromDb.City = address.City;
            obJFromDb.State = address.State;
            obJFromDb.Zip = address.Zip;
            obJFromDb.IsArchive = address.IsArchive;
            obJFromDb.LastModifiedBy = address.LastModifiedBy;
            obJFromDb.LastModifiedDate = address.LastModifiedDate;

            _db.SaveChanges();



        }
    }
}
