using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sunridge.DataAccess.Data.Repository.IRepository;


namespace Sunridge.Pages.Dashboard.AdminDash.Board
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
        public Models.Board BoardObj { get; set; }



        public IActionResult OnGet(int? id)
        {
            BoardObj = new Models.Board();

            if (id != null)// allows edit to be used
            {
                BoardObj = _unitOfWork.Board.GetFirstOrDefault(s => s.Id == id);
                if (BoardObj == null) // catches the exception if it can not find the Board object
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
            if (BoardObj.Id == 0) // checking to see if it is a new Board member
            {
                // rename the file user submited
                string fileName = Guid.NewGuid().ToString();

                // upload to path
                var uploads = Path.Combine(webRootPath, @"Images\BoardImages");

                // preserve our extentions
                var extension = Path.GetExtension(files[0].FileName);

                using (var fileStream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))

                {
                    files[0].CopyTo(fileStream);
                }

                BoardObj.Image = @"Images\BoardImages\" + fileName + extension;
                _unitOfWork.Board.Add(BoardObj);
            }
            else // if is not then it is an existing object that is being edit and updated
            {
                var objFromDb = _unitOfWork.Board.Get(BoardObj.Id);

                if (files.Count > 0)
                {
                    string fileName = Guid.NewGuid().ToString();

                    var uploads = Path.Combine(webRootPath, @"Images\BoardImages");

                    var extension = Path.GetExtension(files[0].FileName);

                    var imagePath = Path.Combine(webRootPath, objFromDb.Image.TrimStart('\\'));

                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }

                    using (var fileStream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }

                    BoardObj.Image = @"Images\BoardImages\" + fileName + extension;

                }
                else
                {
                    BoardObj.Image = objFromDb.Image;
                }

                _unitOfWork.Board.Update(BoardObj);
            }

            _unitOfWork.Save();
            return RedirectToPage("./Index");
        }

    }
}