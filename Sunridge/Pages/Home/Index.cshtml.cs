using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;
using Sunridge.Utility;

namespace Sunridge.Pages.Home
{
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public IndexModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [BindProperty]
        public List<Banner> BannerList { get; set; }
        public void OnGet()
        {

            BannerList = _unitOfWork.Banner.GetAll(c => c.Status == SD.StatusActive).ToList();
            //BannerList = new List<Banner>();
            //List<Banner> FullList = _unitOfWork.Banner.GetAll().ToList();

            //foreach(Banner banner in FullList)
            //{
            //    if(banner.Status == SD.StatusActive)
            //    {
            //        BannerList.Add(banner);
            //    }
            //}

            
        }
    }
}