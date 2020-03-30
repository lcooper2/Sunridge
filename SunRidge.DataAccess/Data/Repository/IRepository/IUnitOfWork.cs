using System;
using System.Collections.Generic;
using System.Text;

namespace Sunridge.DataAccess.Data.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IApplicationUserRepository ApplicationUser { get; }
        IBannerRepository Banner { get; }
        ILostAndFoundRepository LostAndFound { get; }
        IAddressRepository Address { get; }
        IKeyRepository Key { get; }
        IKeyHistoryRepository KeyHistory { get; }
        ILotRepository Lot { get; }
        IBoardRepository Board { get; }
        IInKindWorkHoursRepository InKindWorkHours { get; }
        
        IFormResponseRepository FormResponse { get ;}

        ILotHistoryRepository LotHistory { get; }

        void Save();
    }
}
