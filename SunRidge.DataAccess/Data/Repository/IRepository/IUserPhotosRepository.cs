using System;
using System.Collections.Generic;
using System.Text;
using Sunridge.Models;

namespace Sunridge.DataAccess.Data.Repository.IRepository
{
    public interface IUserPhotosRepository : IRepository<UserPhotos>
    {
        public void Update(UserPhotos userPhotos);

    }
}
