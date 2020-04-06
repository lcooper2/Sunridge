using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sunridge.Models;

namespace Sunridge.DataAccess.Data.Repository.IRepository
{
    public interface IClassifiedCategoryRepository : IRepository<ClassifiedCategory>
    {
        IEnumerable<SelectListItem> GetCategoryListForDropDown();
        public void Update(ClassifiedCategory classifiedCategory);

    }
}
