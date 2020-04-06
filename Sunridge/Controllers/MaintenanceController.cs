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
    public class MaintenanceController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public MaintenanceController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
        [HttpGet]
        public IActionResult Get()
        {
            return Json(new { data = _unitOfWork.Maintenance.GetAll(null, null, "CommonAreaAsset") });
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.Maintenance.GetFirstOrDefault(u => u.Id == id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            _unitOfWork.Maintenance.Remove(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });
        }
    }
}