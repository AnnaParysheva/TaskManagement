using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.DAL;

namespace TaskManagement.BLL.Interfaces
{
    public interface ITaskService
    {
        Task<List<Tasks>> GetTasksAsync();
        Task<Tasks> GetTaskByIdAsync(Guid id);
        Task AddTaskAsync(Tasks task);
        Task UpdateTaskAsync(Tasks task);
        Task DeleteTaskAsync(Guid id);
    }
}
