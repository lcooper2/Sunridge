using Microsoft.AspNetCore.Mvc.Rendering;
using Sunridge.Models;
using System.Collections.Generic;

namespace Sunridge.DataAccess.Data.Repository.IRepository
{
    public interface IFormSubmissionsRepository : IRepository<FormSubmissions>
    {
        IEnumerable<SelectListItem> GetFormSubmissionsListForDropDown();
        public void Update(FormSubmissions FormSubmissions);
    }
}