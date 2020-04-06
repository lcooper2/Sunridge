using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sunridge.Models;

namespace Sunridge.DataAccess.Data.Repository.IRepository
{
    public interface IClassifiedImageRepository : IRepository<ClassifiedImage>
    {
        IEnumerable<SelectListItem> GetImageListForDropDown(int? id);
        public void Update(ClassifiedImage classifiedImage);

    }
}
