using System;
using System.Collections.Generic;
using System.Text;
using Sunridge.Models;

namespace Sunridge.DataAccess.Data.Repository.IRepository
{
    public interface IClassifiedCategoryRepository : IRepository<ClassifiedCategory>
    {
        public void Update(ClassifiedCategory classifiedCategory);

    }
}
