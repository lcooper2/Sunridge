using System;
using System.Collections.Generic;
using System.Text;
using Sunridge.Models;
namespace Sunridge.DataAccess.Data.Repository.IRepository
{
    public interface IClaimLossRepository : IRepository<ClaimLoss>
    {
        public void Update(ClaimLoss claimLoss);

    }
}
