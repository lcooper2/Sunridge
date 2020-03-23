﻿using Sunridge.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sunridge.DataAccess.Data.Repository.IRepository
{
    public interface IBoardRepository : IRepository<Board>
    {
        public void Update(Board board);

    }
}
