using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;

namespace Sunridge.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public IndexModel(
            IUnitOfWork unitOfWork,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [BindProperty]
        public ApplicationUser appUser { get; set; }

        [BindProperty]
        public bool receiveEmail { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        private async Task LoadAsync(IdentityUser user)
        {
            var applicationUser = _unitOfWork.ApplicationUser.GetFirstOrDefault(x => x.Id == user.Id);

            appUser = applicationUser;

                receiveEmail = false;
            if (appUser.ReceiveEmails == true)
                receiveEmail = true;

        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }
      
        public  IActionResult OnPost()
        {


            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (appUser.Id == null)
            {
                return Page();
            }
            else
            {
                if (receiveEmail == true)
                {
                    appUser.ReceiveEmails = true;
                }
                else
                {
                    appUser.ReceiveEmails = false;
                }
                _unitOfWork.ApplicationUser.Update(appUser);
            }

            StatusMessage = "Profile has been Updated!";
            _unitOfWork.Save(); 
            return RedirectToPage();
        }
    }
}
