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
using Sunridge.Models;

namespace Sunridge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassifiedsController : Controller
    {
        public readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public ClassifiedsController(IUnitOfWork unitOfWork, IWebHostEnvironment hostingEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Get()
        {
            List<ApplicationUser> AU = _unitOfWork.ApplicationUser.GetAll().ToList();
            List<ClassifiedListing> CL = _unitOfWork.ClassifiedListing.GetAll().ToList();
            List<ClassifiedCategory> CC = _unitOfWork.ClassifiedCategory.GetAll().ToList();
            foreach (var item in CL)
            {
                //item.UserId = AU.FirstOrDefault(c => c.Id == item.UserId).FullName;
                //item.ClassifiedCategoryId = CC.FirstOrDefault(c => c.Id == item.ClassifiedCategoryId).Description;
                item.ApplicationUser = AU.FirstOrDefault(c => c.Id == item.UserId);
                item.ClassifiedCategory = CC.FirstOrDefault(c => c.Id == item.ClassifiedCategoryId);
            }
            return Json(new { data = _unitOfWork.ClassifiedListing.GetAll() });
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var ObjFromDb = _unitOfWork.ClassifiedListing.GetFirstOrDefault(u => u.Id == id);
                if (ObjFromDb == null)
                {
                    return Json(new { success = false, message = "Error while deleting" });
                }
                // physically remove any uploaded images
                var imagesFromDb = _unitOfWork.ClassifiedImage.GetAll(u => u.ClassifiedListingId == id).ToList();
                foreach (var item in imagesFromDb)
                {
                    var imagePath = Path.Combine(_hostingEnvironment.WebRootPath, item.ImageURL.TrimStart('\\'));

                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }

                    _unitOfWork.ClassifiedImage.Remove(item);
                }

                _unitOfWork.ClassifiedListing.Remove(ObjFromDb);
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