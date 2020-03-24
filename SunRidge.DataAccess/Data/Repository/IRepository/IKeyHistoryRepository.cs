using Sunridge.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sunridge.DataAccess.Data.Repository.IRepository
{
    public interface IKeyHistoryRepository : IRepository<KeyHistory>
    {
        public void Update(KeyHistory keyHistory);

    }
   
}
