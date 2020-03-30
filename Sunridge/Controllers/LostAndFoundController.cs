using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sunridge.DataAccess.Data.Repository.IRepository;

namespace Sunridge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LostAndFoundController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public LostAndFoundController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Get()
        {
            return Json(new { data = _unitOfWork.LostAndFound.GetAll() });
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.LostAndFound.GetFirstOrDefault(u => u.Id == id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            _unitOfWork.LostAndFound.Remove(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Deletion was Successful" });
        }
    
 }
}