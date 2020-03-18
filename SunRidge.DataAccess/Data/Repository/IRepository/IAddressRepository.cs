using System;
using System.Collections.Generic;
using System.Text;
using Sunridge.Models;

namespace Sunridge.DataAccess.Data.Repository.IRepository
{
    public interface IAddressRepository : IRepository<Address>
    {
        public void Update(Address address);

    }
}
