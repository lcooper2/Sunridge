using Microsoft.AspNetCore.Mvc.Rendering;
using Sunridge.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sunridge.DataAccess.Data.Repository.IRepository
{
    public interface ILotInventoryRepository : IRepository<LotInventory>
    {
   
        public void Update(LotInventory lotInventory);
    }
}
