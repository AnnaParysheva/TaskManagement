using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.BLL.Interfaces;
using TaskManagement.DAL;

namespace TaskManagement.BLL.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _repo;
        private readonly ILogger<TaskService> _logger;

        public TaskService(ITaskRepository repo, ILogger<TaskService> logger)
        {
            _repo = repo;
            _logger = logger;
            _logger.LogInformation("Сервис задач инициализирован");
        }

        public async Task<List<Tasks>> GetTasksAsync()
        {
            _logger.LogDebug("Получение списка всех задач");
            return await _repo.GetAllAsync();
        }

        public async Task<Tasks> GetTaskByIdAsync(Guid id)
        {
            _logger.LogDebug("Получение задачи по ID: {TaskId}", id);
            return await _repo.GetTaskByIdAsync(id);
        }

        public async Task AddTaskAsync(Tasks task)
        {
            _logger.LogInformation("Добавление новой задачи: '{TaskTitle}'", task.Title);
            await _repo.AddAsync(task);
            _logger.LogInformation("Задача успешно добавлена");
        }

        public async Task UpdateTaskAsync(Tasks task)
        {
            _logger.LogInformation("Обновление задачи ID {TaskId}: '{TaskTitle}'",
                task.Id, task.Title);
            await _repo.UpdateAsync(task);
            _logger.LogInformation("Задача успешно обновлена");
        }

        public async Task DeleteTaskAsync(Guid id)
        {
            _logger.LogWarning("Удаление задачи с ID: {TaskId}", id);
            await _repo.DeleteAsync(id);
            _logger.LogInformation("Задача успешно удалена");
        }
    }
}
