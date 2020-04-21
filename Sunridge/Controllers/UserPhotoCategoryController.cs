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
    public class UserPhotoCategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserPhotoCategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Get()
        {
            return Json(new { data = _unitOfWork.UserPhotoCategory.GetAll()});
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var ObjFromDb = _unitOfWork.UserPhotoCategory.GetFirstOrDefault(u => u.Id == id);
            if (ObjFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            _unitOfWork.UserPhotoCategory.Remove(ObjFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Deletion was Successful" });
        }
    }
}