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
using Sunridge.Utility;

namespace Sunridge.Pages.Dashboard.AdminDash.User
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

        public string OrginalRole { get; set; }

        [BindProperty]
        public ApplicationUser applicationUser { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {

            public string Role { get; set; }
        }

        private async Task LoadAsync()
        {

            var UserRole = await _userManager.IsInRoleAsync(applicationUser, SD.AdminRole);

            if (UserRole == true)
            {
                Input = new InputModel
                {
                    Role = SD.AdminRole

                };
            }
            else
            {
                Input = new InputModel
                {
                    Role = SD.Owner
                };
            }


            //var userName = await _userManager.GetUserNameAsync(user);
            //var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            //Username = userName;

            //Input = new InputModel
            //{
            //    PhoneNumber = phoneNumber
            //};
        }

        public async Task<IActionResult> OnGet(string Id)
        {
            applicationUser = new ApplicationUser();

            applicationUser = _unitOfWork.ApplicationUser.GetFirstOrDefault(x => x.Id == Id);

            if (applicationUser == null) // catches the exception if it can not find the Board object
            {
                return NotFound();
            }
            await LoadAsync();
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var UserRole = await _userManager.IsInRoleAsync(applicationUser, SD.AdminRole);
            var st = applicationUser.Id;
            if (UserRole == true)
            {
                OrginalRole = SD.AdminRole;
                //_unitOfWork.ApplicationUser.Update(applicationUser);
                if (OrginalRole != Input.Role)
                {
                    await _userManager.RemoveFromRoleAsync(applicationUser, SD.AdminRole);
                    await _userManager.AddToRoleAsync(applicationUser, SD.Owner);
                }
            }
            else
            {
                OrginalRole = SD.Owner;
                if (OrginalRole != Input.Role)
                {
                    await _userManager.RemoveFromRoleAsync(applicationUser, SD.Owner);
                    await _userManager.AddToRoleAsync(applicationUser, SD.AdminRole);
                }
            }

                
                return RedirectToPage("./Index");
            }
        }
    }
