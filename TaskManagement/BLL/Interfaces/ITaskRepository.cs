using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.DAL;

namespace TaskManagement.BLL.Interfaces
{
    public interface ITaskRepository
    {
        Task<List<Tasks>> GetAllAsync();
        Task<Tasks> GetTaskByIdAsync(Guid id);
        Task AddAsync(Tasks task);
        Task UpdateAsync(Tasks task);
        Task DeleteAsync(Guid id);
    }
}
