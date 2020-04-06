using Sunridge.Models;

namespace Sunridge.DataAccess.Data.Repository.IRepository
{
    public interface ISuggestionComplaintRepository : IRepository<SuggestionComplaint>
    {
        public void Update(SuggestionComplaint suggestionComplaint);

    }
}