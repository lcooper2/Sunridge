using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;

namespace Sunridge
{
    public class PhotosSelectorModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public IEnumerable<UserPhotoCategory> CategoryList { get; set; }

        public PhotosSelectorModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void OnGet()
        {
            CategoryList = _unitOfWork.UserPhotoCategory.GetAll();
        }
    }
}