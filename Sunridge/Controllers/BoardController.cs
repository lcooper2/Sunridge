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
    public class BoardController : Controller
    {
        public readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly UserManager<IdentityUser> _userManager;

        public BoardController(IUnitOfWork unitOfWork, IWebHostEnvironment hostingEnvironment, UserManager<IdentityUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _hostingEnvironment = hostingEnvironment;
            _userManager = userManager;
        }

        public IActionResult Get()
        {
            return Json(new { data = _unitOfWork.Board.GetAll(null, null, "ApplicationUser") });
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var ObjFromDb = _unitOfWork.Board.GetFirstOrDefault(u => u.Id == id);
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

                _userManager.RemoveFromRoleAsync(ObjFromDb.ApplicationUser, SD.AdminRole);
                _unitOfWork.Board.Remove(ObjFromDb);

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