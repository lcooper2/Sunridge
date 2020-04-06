using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;
using System.Linq;

namespace Sunridge.DataAccess.Data.Repository
{
    internal class ScheduledEventRepository : Repository<ScheduledEvent>, IScheduledEventRepository
    {
        private ApplicationDbContext _db;

        public ScheduledEventRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(ScheduledEvent scheduledEvent)
        {
            var obJFromDb = _db.ScheduledEvent.FirstOrDefault(s => s.Id == scheduledEvent.Id);

            obJFromDb.Description = scheduledEvent.Description;
            obJFromDb.Subject = scheduledEvent.Subject;
            obJFromDb.Image = scheduledEvent.Image;
            obJFromDb.Location = scheduledEvent.Location;
            obJFromDb.Start = scheduledEvent.Start;
            obJFromDb.End = scheduledEvent.End;
            obJFromDb.IsFullDay = scheduledEvent.IsFullDay;

            _db.SaveChanges();

        }
    }
}