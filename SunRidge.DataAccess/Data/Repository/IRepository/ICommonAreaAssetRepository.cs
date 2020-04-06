using Microsoft.AspNetCore.Mvc.Rendering;
using Sunridge.Models;
using System.Collections.Generic;

namespace Sunridge.DataAccess.Data.Repository.IRepository
{
    public interface ICommonAreaAssetRepository : IRepository<CommonAreaAsset>
    {
        public IEnumerable<SelectListItem> GetCommonAssetListForDropDown();
        public void Update(CommonAreaAsset commonAreaAsset);
    }
}