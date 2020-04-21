using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;

namespace Sunridge.Pages.Dashboard.OwnerDash.ClassifiedsList
{
    [BindProperties]
    public class UpsertModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly UserManager<IdentityUser> _userManager;

        public UpsertModel(IUnitOfWork unitOfWork, IWebHostEnvironment hostingEnvironment
            , UserManager<IdentityUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _hostingEnvironment = hostingEnvironment;
            _userManager = userManager;
        }

        public ClassifiedListing Listing { get; set; }
        public IEnumerable<SelectListItem> CategoryList { get; set; }
        public ClassifiedImage Image { get; set; }
        public IActionResult OnGet(int? id)
        {
            Listing = new ClassifiedListing
            {
                ItemName = "",
                Description = "",
                ClassifiedCategoryId = 1,
                UserId = _userManager.GetUserId(User)
            };
            CategoryList = _unitOfWork.ClassifiedCategory.GetCategoryListForDropDown();

            if (CategoryList.Count() == 0)
            {
                _unitOfWork.ClassifiedCategory.Add(new ClassifiedCategory { Description = "Lots" });
                _unitOfWork.ClassifiedCategory.Add(new ClassifiedCategory { Description = "Cabins" });
                _unitOfWork.ClassifiedCategory.Add(new ClassifiedCategory { Description = "ATVs" });
                _unitOfWork.ClassifiedCategory.Add(new ClassifiedCategory { Description = "Trailers" });
                _unitOfWork.ClassifiedCategory.Add(new ClassifiedCategory { Description = "Service" });
                _unitOfWork.Save();
            }

            Image = new ClassifiedImage();

            if (id != null)
            {
                Listing = _unitOfWork.ClassifiedListing.GetFirstOrDefault(s => s.Id == id);
                if (Listing == null)
                {
                    return NotFound();
                }

                Image = _unitOfWork.ClassifiedImage.GetAll(u => u.IsMainImage == true).FirstOrDefault(c => c.ClassifiedListingId == Listing.Id);

            }
            return Page();
        }

        public IActionResult OnPost()
        {
            string webRootPath = _hostingEnvironment.WebRootPath;
            var files = HttpContext.Request.Form.Files;

            Listing.ListingDate = DateTime.Now;
            Listing.UserId = _userManager.GetUserId(User);

            if (Listing.Phone == null)
            {
                Listing.Phone = _unitOfWork.ApplicationUser.GetFirstOrDefault(c => c.Id == Listing.UserId).Phone;
            }

            if (Listing.Email == null)
            {
                Listing.Email = _unitOfWork.ApplicationUser.GetFirstOrDefault(c => c.Id == Listing.UserId).Email;
            }

            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}

            if (Listing.Id == 0)
            {
                Listing.UserId = _userManager.GetUserId(User);
                _unitOfWork.ClassifiedListing.Add(Listing);
                _unitOfWork.Save();
                //Now that the listing is in the DB, we can get its Id to match to a new
                //classifiedimage obj
                if (files.Count > 0)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(webRootPath, @"Images\ClassifiedsImages\");
                    var extension = Path.GetExtension(files[0].FileName);

                    using (var fileStream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))

                    {
                        files[0].CopyTo(fileStream);
                    }
                    Image = new ClassifiedImage
                    {
                        ClassifiedListingId = _unitOfWork.ClassifiedListing.GetFirstOrDefault(u => u.ListingDate == Listing.ListingDate).Id,
                        ImageURL = @"Images\ClassifiedsImages\" + fileName + extension,
                        IsMainImage = true
                    };

                    _unitOfWork.ClassifiedImage.Add(Image);
                }
            }
            else
            {
                _unitOfWork.ClassifiedListing.Update(Listing);
            }

            _unitOfWork.Save();
            return RedirectToPage("./Index");
        }

    }
}