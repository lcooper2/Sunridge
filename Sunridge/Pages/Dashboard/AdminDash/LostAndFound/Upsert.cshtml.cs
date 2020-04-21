using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;

namespace Sunridge.Pages.Dashboard.AdminDash.LostAndFound
{
    public class UpsertModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public UpsertModel(IUnitOfWork unitOfWork, IWebHostEnvironment hostingEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostingEnvironment = hostingEnvironment;
        }



        [BindProperty]
        public Models.LostAndFound LAFObj { get; set; } // Lost And Found Object


        public IActionResult OnGet(int? id)
        {
            LAFObj = new Models.LostAndFound();
            
            if (id != null)// allows edit to be used
            {
                LAFObj = _unitOfWork.LostAndFound.GetFirstOrDefault(s => s.Id == id);

                if (LAFObj == null) // catches the exception if it can not find the Lost and Found object
                {
                    return NotFound();
                }

            }
            return Page();
        }


        public IActionResult OnPost()
        {
            // Find the root path
            string webRootPath = _hostingEnvironment.WebRootPath;
            // grab the file(s)
            var files = HttpContext.Request.Form.Files;

            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (LAFObj.Id == 0) // checking to see if it is a new Banner
            {

                // rename the file user submited
                LAFObj.ListedDate = DateTime.Now;
                _unitOfWork.LostAndFound.Add(LAFObj);
                _unitOfWork.Save();

                // upload to path
                var uploads = Path.Combine(webRootPath, @"Images\LostAndFoundImages");

                

                foreach (var file in files)

                {
                    string fileName = Guid.NewGuid().ToString() + LAFObj.Id.ToString();
                    

                    var extension = Path.GetExtension(file.FileName);

                    using (var filestream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))

                    {

                        file.CopyTo(filestream); // moves to server and renames

                    }

                    var image = new LostAndFoundImage()

                    {

                        LostAndFoundId = LAFObj.Id,

                        IsMainImage = (file == files.First()),



                        ImageURL = @"Images\LostAndFoundImages\" + fileName + extension

                    };




                    _unitOfWork.LostAndFoundImage.Add(image);
                }


            }

            else // if is not then it is an existing object that is being edit and updated
            {


                if (files.Count > 0)
                {
                    string fileName = Guid.NewGuid().ToString();

                    var uploads = Path.Combine(webRootPath, @"Images\LostAndFoundImages");

                    var oldImages = _unitOfWork.LostAndFoundImage.GetAll(x => x.LostAndFoundId == LAFObj.Id).ToList();

                    foreach (var oldImage in oldImages)

                    {

                        if (System.IO.File.Exists(Path.Combine(webRootPath, oldImage.ImageURL.TrimStart('\\'))))

                        {

                            System.IO.File.Delete(Path.Combine(webRootPath, oldImage.ImageURL.TrimStart('\\')));

                        }

                        _unitOfWork.LostAndFoundImage.Remove(oldImage);

                    }

                    int i = 0;

                    foreach (var file in files)

                    {
                         fileName = Guid.NewGuid().ToString() + LAFObj.Id.ToString();
                        i++;

                        var extension = Path.GetExtension(file.FileName);

                        using (var filestream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))

                        {

                            file.CopyTo(filestream); // moves to server and renames

                        }

                        var image = new LostAndFoundImage()

                        {

                            LostAndFoundId = LAFObj.Id,

                            IsMainImage = (file == files.First()),

                            ImageURL = @"Images\LostAndFoundImages\" + fileName + extension

                        };
                        _unitOfWork.LostAndFoundImage.Add(image);
                    }

                    if (LAFObj.Claimed == false)
                    {
                        LAFObj.ClaimedBy = null;
                    }
                    else if (LAFObj.Claimed == true)
                    {
                        LAFObj.ClaimedDate = DateTime.Now;
                    }
                    

                }
                _unitOfWork.LostAndFound.Update(LAFObj);
            }

            _unitOfWork.Save();
            return RedirectToPage("./Index");
        }
    }
}