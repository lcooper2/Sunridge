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
       

        public ProfileModel(
            UserManager<IdentityUser> userManager,
            
            IUnitOfWork unitOfWork
            )
        {
            _userManager = userManager;
          
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
            public bool makeAdmin { get; set; }
            public bool ReceiveEmail { get; set; }
        }


        private async Task LoadAsync()
        {

            var UserRole = await _userManager.IsInRoleAsync(applicationUser, SD.AdminRole);

            if (UserRole == true)
            {
                Input = new InputModel
                {
                    Role = SD.AdminRole,
                    makeAdmin = true,
                    ReceiveEmail = false
                    
                };
                
            }
            else
            {
                Input = new InputModel
                {
                    Role = SD.Owner,
                    makeAdmin = false,
                    ReceiveEmail = false
                };
            }

           
                if (applicationUser.ReceiveEmails == true)
                    Input.ReceiveEmail = true;
          

            
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

            var user = await _userManager.FindByIdAsync(applicationUser.Id);
            if (Input.makeAdmin == true)
            {
                Input.Role = SD.AdminRole;
            }
            else
            {
                Input.Role = SD.Owner;
            }

            if (await _userManager.IsInRoleAsync(applicationUser, SD.AdminRole))
            {
                OrginalRole = SD.AdminRole;

                if (OrginalRole != Input.Role)
                {
                    await _userManager.RemoveFromRoleAsync(user, SD.AdminRole);

                }
            }
            else
            {
                OrginalRole = SD.Owner;
                if (OrginalRole != Input.Role)
                {
                    await _userManager.AddToRoleAsync(user, SD.AdminRole);


                }
            }
            if(Input.ReceiveEmail == true)
            {
                applicationUser.ReceiveEmails = true;
            }
            else
            {
                applicationUser.ReceiveEmails = false;
            }
           

            _unitOfWork.ApplicationUser.Update(applicationUser);
            _unitOfWork.Save();

            return RedirectToPage("./Index");
            }
        }
    }
