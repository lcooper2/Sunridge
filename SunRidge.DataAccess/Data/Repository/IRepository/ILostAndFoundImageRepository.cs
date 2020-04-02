using System;
using System.Collections.Generic;
using System.Text;
using Sunridge.Models;

namespace Sunridge.DataAccess.Data.Repository.IRepository
{
    public interface ILostAndFoundImageRepository : IRepository<LostAndFoundImage>
    {
        public void Update(LostAndFoundImage lostAndFoundImage);

    }
}
