using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.BLL.Interfaces;

namespace TaskManagement.DAL
{
    public class TaskRepository : ITaskRepository
    {
        private readonly TaskDbContext _context;
        private readonly ILogger<TaskRepository> _logger;

        public TaskRepository(TaskDbContext context, ILogger<TaskRepository> logger)
        {
            _context = context;
            _logger = logger;
            _logger.LogDebug("Репозиторий задач инициализирован");
        }

        public async Task<List<Tasks>> GetAllAsync()
        {
            _logger.LogTrace("Запрос всех задач из базы данных");
            var tasks = await _context.Tasks.ToListAsync();
            _logger.LogDebug("Получено {TaskCount} задач из базы", tasks.Count);
            return tasks;
        }

        public async Task<Tasks> GetTaskByIdAsync(Guid id)
        {
            _logger.LogDebug("Поиск задачи по ID: {TaskId}", id);
            var task = await _context.Tasks.FindAsync(id);

            if (task == null)
                _logger.LogWarning("Задача с ID {TaskId} не найдена", id);
            else
                _logger.LogDebug("Найдена задача: '{TaskTitle}'", task.Title);

            return task;
        }

        public async Task AddAsync(Tasks task)
        {
            _logger.LogInformation("Добавление задачи в БД: '{TaskTitle}' (ID: {TaskId})",
                task.Title, task.Id);
            await _context.Tasks.AddAsync(task);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Задача успешно сохранена в БД");
        }

        public async Task UpdateAsync(Tasks task)
        {
            _logger.LogInformation("Обновление задачи в БД: '{TaskTitle}' (ID: {TaskId})",
                task.Title, task.Id);
            _context.Tasks.Update(task);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Изменения задачи сохранены в БД");
        }

        public async Task DeleteAsync(Guid id)
        {
            _logger.LogWarning("Попытка удаления задачи ID: {TaskId}", id);
            var task = await _context.Tasks.FindAsync(id);

            if (task != null)
            {
                _logger.LogInformation("Удаление задачи: '{TaskTitle}' (ID: {TaskId})",
                    task.Title, task.Id);
                _context.Tasks.Remove(task);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Задача успешно удалена из БД");
            }
            else
            {
                _logger.LogWarning("Задача для удаления не найдена: ID {TaskId}", id);
            }
        }
    }
}
