using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Sunridge.DataAccess.Data.Repository.IRepository;

namespace Sunridge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LostAndFoundController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public LostAndFoundController(IUnitOfWork unitOfWork, IWebHostEnvironment hostingEnvironment, UserManager<IdentityUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _hostingEnvironment = hostingEnvironment;
            _userManager = userManager;
        }

        public IActionResult Get()
        {
            return Json(new { data = _unitOfWork.LostAndFound.GetAll() });
        }

        //public async Task<IActionResult> Get(int id)
        //{
        //    //var user = await _userManager.GetUserAsync(User);
        //    //return Json(new { data = _unitOfWork.LostAndFound.GetAll(x => x.UserId == user.Id) });
        //    return Json(new { data = _unitOfWork.LostAndFound.GetAll() });
        //}

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var ObjFromDb = _unitOfWork.LostAndFound.GetFirstOrDefault(u => u.Id == id);
                if (ObjFromDb == null)
                {
                    return Json(new { success = false, message = "Error while deleting" });
                }

                var images = _unitOfWork.LostAndFoundImage.GetAll(x => x.LostAndFoundId == ObjFromDb.Id).ToList();
                // physically Remove any uploaded images
                
                foreach (var image in images)
                {
                    var imagePath = Path.Combine(_hostingEnvironment.WebRootPath, image.ImageURL.TrimStart('\\'));


                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }
                }
                _unitOfWork.LostAndFound.Remove(ObjFromDb);

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
