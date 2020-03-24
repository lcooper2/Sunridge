using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models.ViewModels;
using Sunridge.Utility;

namespace Sunridge.Pages.Dashboard.AdminDash.Board
{
    public class UpsertModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly UserManager<IdentityUser> _userManager;


        public UpsertModel(IUnitOfWork unitOfWork, IWebHostEnvironment hostingEnvironment, UserManager<IdentityUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _hostingEnvironment = hostingEnvironment;
            _userManager = userManager;
        }

        [BindProperty]
        public BoardVM BoardObj { get; set; }



        public IActionResult OnGet(int? id)
        {
            BoardObj = new BoardVM
            {
                ApplicationUserList = _unitOfWork.ApplicationUser.GetUserListForDropDown(),

                Board = new Models.Board()
            };

            if (id != null)// allows edit to be used
            {
                BoardObj.Board = _unitOfWork.Board.GetFirstOrDefault(s => s.Id == id);
                if (BoardObj == null) // catches the exception if it can not find the Board object
                {
                    return NotFound();
                }
            }
            return Page();
        }

        //public async Task<IActionResult> OnPost()
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

            if (BoardObj.Board.Id == 0) // checking to see if it is a new Board
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

                BoardObj.Board.Image = @"Images\BoardImages\" + fileName + extension;
                _unitOfWork.Board.Add(BoardObj.Board);

               // await _userManager.AddToRoleAsync(BoardObj.Board.ApplicationUser, SD.AdminRole);



            }
            else // it is an existing object that is being edit and updated
            {
                var objFromDb = _unitOfWork.Board.Get(BoardObj.Board.Id);

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

                    BoardObj.Board.Image = @"Images\BoardImages\" + fileName + extension;

                }
                else
                {
                    BoardObj.Board.Image = objFromDb.Image;
                }

                _unitOfWork.Board.Update(BoardObj.Board);
            }

            _unitOfWork.Save();
            return RedirectToPage("./Index");
        }

    }
}