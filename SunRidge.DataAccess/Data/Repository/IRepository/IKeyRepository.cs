using Sunridge.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sunridge.DataAccess.Data.Repository.IRepository
{
    public interface IKeyRepository : IRepository<Key>
    {
        public void Update(Key key);
    }
}
