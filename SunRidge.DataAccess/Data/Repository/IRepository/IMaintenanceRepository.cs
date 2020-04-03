using Sunridge.Models;

namespace Sunridge.DataAccess.Data.Repository.IRepository
{
    public interface IMaintenanceRepository : IRepository<Maintenance>
    {
        
        public void Update(Maintenance maintenance);
    }
}