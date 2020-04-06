using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;
using System.Linq;

namespace Sunridge.DataAccess.Data.Repository
{
    internal class SuggestionComplaintRepository : Repository<SuggestionComplaint>, ISuggestionComplaintRepository
    {
        private ApplicationDbContext _db;

        public SuggestionComplaintRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(SuggestionComplaint suggestionComplaint)
        {

            var obJFromDb = _db.SuggestionComplaint.FirstOrDefault(s => s.Id == suggestionComplaint.Id);

            obJFromDb.Suggestion = suggestionComplaint.Suggestion;
            obJFromDb.Complaint = suggestionComplaint.Complaint;
            obJFromDb.ApplicationUserId = suggestionComplaint.ApplicationUserId;
            obJFromDb.ApplicationUser = suggestionComplaint.ApplicationUser;
            _db.SaveChanges();
        }
    }
}