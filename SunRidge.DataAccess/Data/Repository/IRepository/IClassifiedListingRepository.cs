using System;
using System.Collections.Generic;
using System.Text;
using Sunridge.Models;

namespace Sunridge.DataAccess.Data.Repository.IRepository
{
    public interface IClassifiedListingRepository : IRepository<ClassifiedListing>
    {
        public void Update(ClassifiedListing classifiedListing);

    }
}
