using Sunridge.Models;
namespace Sunridge.DataAccess.Data.Repository.IRepository
{
    public interface IFormResponseRepository: IRepository<FormResponse>
    {
        public void Update(FormResponse formResponse);

    }
}