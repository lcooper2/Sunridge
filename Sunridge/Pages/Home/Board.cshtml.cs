using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sunridge.Models;
using Sunridge.DataAccess.Data.Repository.IRepository;

namespace Sunridge.Pages.Home
{
    public class BoardModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public IEnumerable<Board> BoardMembers { get; set; }

        public BoardModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void OnGet()
        {
            BoardMembers = _unitOfWork.Board.GetAll(null, q => q.OrderBy(c => c.DisplayOrder), null);

        }
    }
}