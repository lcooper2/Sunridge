using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Sunridge.Utility;
using Microsoft.AspNetCore.Identity;

namespace Sunridge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserPhotosController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public UserPhotosController(IUnitOfWork unitOfWork, IWebHostEnvironment hostingEnvironment, UserManager<IdentityUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _hostingEnvironment = hostingEnvironment;
            _userManager = userManager;
        }

      
        public async Task<IActionResult> Get(int id)
        {
            if (id == 0)
            {
                return Json(new { data = _unitOfWork.UserPhotos.GetAll(null, null, "UserPhotoCategory,ApplicationUser") });
            }
            else if (id == 1)
            {
                var user = await _userManager.GetUserAsync(User);
                return Json(new { data = _unitOfWork.UserPhotos.GetAll(x => x.UserId == user.Id, null, "UserPhotoCategory,ApplicationUser") });
            }

            return Json(new { success = false, message = "Error while Getting" });
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var ObjFromDb = _unitOfWork.UserPhotos.GetFirstOrDefault(u => u.Id == id);
                if (ObjFromDb == null)
                {
                    return Json(new { success = false, message = "Error while deleting" });
                }
                // physically Remove any uploaded images
                var imagePath = Path.Combine(_hostingEnvironment.WebRootPath, ObjFromDb.Image.TrimStart('\\'));


                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }

               // _userManager.RemoveFromRoleAsync(ObjFromDb.ApplicationUser, SD.AdminRole);
                _unitOfWork.UserPhotos.Remove(ObjFromDb);

                _unitOfWork.Save();
            }
            catch (Exception)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            return Json(new { success = true, message = "Deletion was Successful" });
        }


    }
}