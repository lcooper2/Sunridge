using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;

namespace Sunridge.Pages.Home
{
    public class LostFoundModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public List<LostAndFound> itemList { get; set; }
        public IEnumerable<LostAndFoundImage> ItemImages { get; set; }

        public LostFoundModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void OnGet()
        {
            itemList = new List<LostAndFound>();

            IEnumerable<LostAndFound> List = _unitOfWork.LostAndFound.GetAll();
     
            foreach(var item in List)
            {
                ItemImages = _unitOfWork.LostAndFoundImage.GetAll(x => x.LostAndFoundId == item.Id);
                item.Images = ItemImages.ToList();
                itemList.Add(item);
            }

        }
    }
}