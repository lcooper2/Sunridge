using System;
using System.Collections.Generic;
using System.Text;

namespace Sunridge.DataAccess.Data.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        
        void Save();
    }
}
