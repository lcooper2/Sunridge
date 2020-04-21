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
    [BindProperties]
    public class NewsModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        public List<NewsItem> News { get; set; }
        public List<int> Years { get; set; }
        public string searchString { get; set; }

        public NewsModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> OnGet(string searchString)
        {
            this.searchString = searchString;
            Years = new List<int>();

            if (!String.IsNullOrEmpty(searchString))
            {
                News = _unitOfWork.NewsItem.GetAll().Where(c => c.Content.Contains(searchString, StringComparison.OrdinalIgnoreCase) || (c.Year.ToString()).Contains(searchString, StringComparison.OrdinalIgnoreCase) || c.Header.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();
            } else
            {
                News = _unitOfWork.NewsItem.GetAll().ToList();
            }
            
            if (News.Count > 0)
            {
                foreach (var item in News)
                {
                    if (!Years.Contains(item.Year))
                    {
                        Years.Add(item.Year);
                    }
                }
            }
            Years.Sort();
            return Page();
        }
    }
}