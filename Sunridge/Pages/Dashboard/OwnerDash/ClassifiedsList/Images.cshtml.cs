using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sunridge.DataAccess.Data;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;

namespace Sunridge
{
    public class ImagesModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public ImagesModel(IUnitOfWork unitOfWork, IWebHostEnvironment hostingEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostingEnvironment = hostingEnvironment;
        }

        [BindProperty]
        public ClassifiedListing ClassifiedListing { get; set; }
        [BindProperty]
        public String singleImage { get; set; }

        public IActionResult OnGet(int? id, string singleimage)
        {
            singleImage = singleimage;
            if (id == null)
            {
                return NotFound();
            }

            ClassifiedListing = _unitOfWork.ClassifiedListing.GetFirstOrDefault(c => c.Id == id);

            if (ClassifiedListing == null)
            {
                return NotFound();
            }

            ClassifiedListing.Images = _unitOfWork.ClassifiedImage.GetAll(c => c.ClassifiedListingId == ClassifiedListing.Id).ToList();
           
            return Page();
        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.

        public IActionResult OnPostRemove(int? id)
        {
            _unitOfWork.ClassifiedImage.Remove(_unitOfWork.ClassifiedImage.GetFirstOrDefault(c => c.Id == id));
            _unitOfWork.Save();
            return RedirectToPage("Images", new { id = ClassifiedListing.Id });
        }
        public IActionResult OnPost(int? Id)
        {
            string webRootPath = _hostingEnvironment.WebRootPath;
            var files = HttpContext.Request.Form.Files;

            int count = 0;
            foreach (var item in ClassifiedListing.Images)
            {
                if (item.IsMainImage == true)
                {
                    count++;
                }
            }
            if (count > 1)
            {
                return RedirectToPage("Images", new { id = Id, singleimage = "1" });
            }

            if (files.Count > 0)
            {
                string fileName = Guid.NewGuid().ToString();
                var uploads = Path.Combine(webRootPath, @"Images\ClassifiedsImages\");
                var extension = Path.GetExtension(files[0].FileName);

                using (var fileStream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))

                {
                    files[0].CopyTo(fileStream);
                }
                var Image = new ClassifiedImage
                {
                    ClassifiedListingId = ClassifiedListing.Id,
                    ImageURL = @"Images\ClassifiedsImages\" + fileName + extension,
                    IsMainImage = false
                };

                _unitOfWork.ClassifiedImage.Add(Image);
            }

            foreach (var item in ClassifiedListing.Images)
            {
                _unitOfWork.ClassifiedImage.Update(item);
            }
            _unitOfWork.Save();
            return RedirectToPage("Images", new { id = Id });
        }

        private bool ClassifiedImageExists(int id)
        {
            return _unitOfWork.ClassifiedImage.GetFirstOrDefault(e => e.Id == id) != null;
        }
    }
}
