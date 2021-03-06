﻿using Microsoft.AspNetCore.Mvc.Rendering;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sunridge.DataAccess.Data.Repository
{
    public class KeyRepository : Repository<Key>, IKeyRepository
    {
        private readonly ApplicationDbContext _db;

        public KeyRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public IEnumerable<SelectListItem> GetKeyListForDropDown()
        {
            return _db.Key.Select(i => new SelectListItem()
            {
                Text = i.FullSerial,
                Value = i.Id.ToString()
            });
        }

        public void Update(Key key)
        {
            var obJFromDb = _db.Key.FirstOrDefault(s => s.Id == key.Id);

            obJFromDb.SerialNumber = key.SerialNumber;
            obJFromDb.Year = key.Year;
            
            _db.SaveChanges();
        }
    }
}

