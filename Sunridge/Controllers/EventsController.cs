﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Sunridge.Utility;
using Microsoft.AspNetCore.Identity;
using Sunridge.Models;

namespace Sunridge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : Controller
    {
        public readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public EventsController(IUnitOfWork unitOfWork, IWebHostEnvironment hostingEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Get()
        {
            return Json(new { data = _unitOfWork.ScheduledEvent.GetAll() });
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var ObjFromDb = _unitOfWork.ScheduledEvent.GetFirstOrDefault(u => u.Id == id);
                if (ObjFromDb == null)
                {
                    return Json(new { success = false, message = "Error while deleting" });
                }

                _unitOfWork.ScheduledEvent.Remove(ObjFromDb);
                _unitOfWork.Save();
            }
            catch (Exception)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            return Json(new { success = true, message = "Deletion was Successful" });
        }


    }
}