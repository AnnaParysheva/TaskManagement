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
                logger.Info("=== ������ ���������� ===");

                var services = new ServiceCollection();
                ConfigureServices(services);

                using var serviceProvider = services.BuildServiceProvider();

                using (var scope = serviceProvider.CreateScope())
                {
                    var db = scope.ServiceProvider.GetRequiredService<TaskDbContext>();
                    db.Database.EnsureCreated();
                    logger.Info("���� ������ ������ � ������");
                }

                ApplicationConfiguration.Initialize();
                var form = serviceProvider.GetRequiredService<TasksForm>();

                logger.Info("������ ������� �����");
                Application.Run(form);
            }
            catch (Exception ex)
            {
                logger.Fatal(ex, "����������� ������ ��� ������� ����������");
                MessageBox.Show("�� ������� ��������� ����������. ��������� ����.",
                    "��������� ������", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                logger.Info("=== ���������� ������ ���������� ===");
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

                logger.Info("������� ������� ����������������");
            }
            catch (Exception ex)
            {
                logger.Error(ex, "������ ��� ��������� ��������");
                throw;
            }
        }
    }
}