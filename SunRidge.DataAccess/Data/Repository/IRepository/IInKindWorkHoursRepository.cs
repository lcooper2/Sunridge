using Sunridge.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sunridge.DataAccess.Data.Repository.IRepository
{
    public interface IInKindWorkHoursRepository : IRepository<InKindWorkHours>
    {
        public void Update(InKindWorkHours inKindWorkHours);

    }
}
