using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog;
using System.Diagnostics;
using TaskManagement.BLL.Interfaces;
using TaskManagement.BLL.Services;
using TaskManagement.DAL;

namespace TaskManagement
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            LogManager.LoadConfiguration("nlog.config");
            var logger = LogManager.GetCurrentClassLogger();

            try
            {
                logger.Info("=== Запуск приложения ===");

                var services = new ServiceCollection();
                ConfigureServices(services);

                using var serviceProvider = services.BuildServiceProvider();

                using (var scope = serviceProvider.CreateScope())
                {
                    var db = scope.ServiceProvider.GetRequiredService<TaskDbContext>();
                    db.Database.EnsureCreated();
                    logger.Info("База данных готова к работе");
                }

                ApplicationConfiguration.Initialize();
                var form = serviceProvider.GetRequiredService<TasksForm>();

                logger.Info("Запуск главной формы");
                Application.Run(form);
            }
            catch (Exception ex)
            {
                logger.Fatal(ex, "Критическая ошибка при запуске приложения");
                MessageBox.Show("Не удалось запустить приложение. Проверьте логи.",
                    "Фатальная ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                logger.Info("=== Завершение работы приложения ===");
                LogManager.Shutdown();
            }
        }
        static void ConfigureServices(IServiceCollection services)
        {
            var logger = LogManager.GetCurrentClassLogger();
            try
            {
                services.AddLogging(loggingBuilder =>
                {
                    loggingBuilder.ClearProviders();
                    loggingBuilder.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                    loggingBuilder.AddProvider(new NLog.Extensions.Logging.NLogLoggerProvider());
                });

                services.AddDbContext<TaskDbContext>(options =>
                    options.UseSqlite("Data Source=Tasks.db"));
                services.AddScoped<ITaskRepository, TaskRepository>();
                services.AddScoped<ITaskService, TaskService>();
                services.AddScoped<TasksForm>();

                logger.Info("Сервисы успешно сконфигурированы");
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Ошибка при настройке сервисов");
                throw;
            }
        }
    }
}