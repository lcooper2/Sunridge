using Microsoft.AspNetCore.Mvc.Rendering;
using Sunridge.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sunridge.DataAccess.Data.Repository.IRepository
{
   public interface IApplicationUserRepository : IRepository<ApplicationUser>
    {
        public IEnumerable<SelectListItem> GetUserListForDropDown();

        public void Update(ApplicationUser applicationUser);

    }
}
