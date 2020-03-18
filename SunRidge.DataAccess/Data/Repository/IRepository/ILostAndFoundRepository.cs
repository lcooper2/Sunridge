using Sunridge.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sunridge.DataAccess.Data.Repository.IRepository
{
   public interface ILostAndFoundRepository : IRepository<LostAndFound>
    {
        public void Update(LostAndFound lostAndFound);
    }
}
