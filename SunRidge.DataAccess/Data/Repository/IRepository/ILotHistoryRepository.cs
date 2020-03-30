using Microsoft.AspNetCore.Mvc.Rendering;
using Sunridge.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sunridge.DataAccess.Data.Repository.IRepository
{
    public interface ILotHistoryRepository : IRepository<LotHistory>
    {
        //IEnumerable<SelectListItem> GetLotListForDropDown();
        public void Update(LotHistory lotHistory);
    }
}
