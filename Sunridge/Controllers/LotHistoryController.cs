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
    public class LotHistoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public LotHistoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
        [HttpGet]
        public IActionResult Get()
        {
            return Json(new { data = _unitOfWork.LotHistory.GetAll(null, null, "Lot") });
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.LotHistory.GetFirstOrDefault(u => u.Id == id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            _unitOfWork.LotHistory.Remove(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });
        }
    }
}