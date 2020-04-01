using Sunridge.Models;

namespace Sunridge.DataAccess.Data.Repository.IRepository
{
    public interface IOwnerLotRepository : IRepository<OwnerLot>
    {
        
        public void Update(OwnerLot ownerLot);
    }
}