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

namespace Sunridge.Pages.Dashboard.AdminDash.Events
{
    [BindProperties]
    public class UpsertModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpsertModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ScheduledEvent Event { get; set; }
        public IActionResult OnGet(int? id)
        {
            if (id != null)
            {
                Event = _unitOfWork.ScheduledEvent.GetFirstOrDefault(s => s.Id == id);
                if (Event == null)
                {
                    return NotFound();
                }
            } else
            {
                Event = new ScheduledEvent
                {
                    Subject = "",
                    Start = DateTime.Now,
                    End = DateTime.Now.AddHours(1),
                    IsFullDay = false
                };
            }
            return Page();
        }

        public IActionResult OnPost()
        {

            if (Event.Id == 0)
            {
                _unitOfWork.ScheduledEvent.Add(Event);
                _unitOfWork.Save();
            }
            else
            {
                _unitOfWork.ScheduledEvent.Update(Event);
            }

            _unitOfWork.Save();
            return RedirectToPage("./Index");
        }

    }
}