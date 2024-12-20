﻿using CopyRight.Bl.Interfaces;
using CopyRight.Dto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopyRight.Bl.Interfaces
{
    public interface IProject :IBlcrud<Projects>
    {
       
        Task<bool> IsOnTheDB(int? id);
        bool IsCorrectDates(DateTime? start, DateTime? end);
        Task<Projects> GetByIdAsync(int id);
        Task<bool> ReadTaskAuthAsync(int id);
        Task<List<Projects>> ReadProjectAsync();
       
    }
}
