using System;
using System.Collections.Generic;
using System.Text;
using Sunridge.Models;

namespace Sunridge.DataAccess.Data.Repository.IRepository
{
    public interface IClassifiedImageRepository : IRepository<ClassifiedImage>
    {
        public void Update(ClassifiedImage classifiedImage);

    }
}
