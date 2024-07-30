using CopyRight.Dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Task = CopyRight.Dal.Models.Task;


namespace CopyRight.Dal.Interfaces
{
    public interface ITasks : IDalCrud<Task>
    {
        Task<Task> GetById(int id);
        Task<List<PriorityCode>> ReadAllPriorityAsync();
        Task<List<StatusCodeProject>> ReadAllStatusAsync();
        Task<bool> UpdateGoogleCalendarAsync(int taskId, string googleId);

    }
}
