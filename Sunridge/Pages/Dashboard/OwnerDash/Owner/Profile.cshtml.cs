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

namespace Sunridge.Pages.Dashboard.OwnerDash.Owner
{
    public partial class ProfileModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public ProfileModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IUnitOfWork unitOfWork
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _unitOfWork = unitOfWork;
        }
        [BindProperty]
        public ApplicationUser ApplicationUser { get; set; }
        public string Username { get; set; }




        private async Task LoadAsync(IdentityUser user)
        {
            var applicationUser = _unitOfWork.ApplicationUser.GetFirstOrDefault(x => x.Id == user.Id);

            ApplicationUser = applicationUser;

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

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (ApplicationUser.Id == null)
            {
                return Page();
            }
            else
            {
                _unitOfWork.ApplicationUser.Update(ApplicationUser);
            }


            _unitOfWork.Save();
            return RedirectToPage("./Profile");
        }


    }

}

