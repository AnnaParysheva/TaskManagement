using Microsoft.Extensions.Logging;
using NLog;
using TaskManagement.BLL.Interfaces;
using TaskManagement.DAL;

namespace TaskManagement
{
    public partial class TasksForm : Form
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly ITaskService _taskService;
        private Guid? _currentEditingId = null;

        public TasksForm(ITaskService taskService)
        {
            _taskService = taskService;
            InitializeComponent();
            ConfigureListView();
            _logger.Info("Форма задач инициализирована");
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            try
            {
                _logger.Debug("Загрузка задач при открытии формы");
                await LoadTasksAsync();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Ошибка при загрузке задач");
                MessageBox.Show("Не удалось загрузить задачи", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigureListView()
        {
            listView1.SelectedIndexChanged += ListView1_SelectedIndexChanged;
            listView1.Columns.Add("Название", 200);
            listView1.Columns.Add("Описание", 300);
            listView1.Columns.Add("Срок выполнения", 150);
            _logger.Debug("ListView сконфигурирован");
        }

        private async void ListView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (listView1.SelectedItems.Count > 0 && listView1.SelectedItems[0].Tag is Guid id)
                {
                    _currentEditingId = id;
                    var task = await _taskService.GetTaskByIdAsync(id);
                    txtTitle.Text = task.Title;
                    txtDescription.Text = task.Description;
                    dateTimePickerDeadline.Value = task.Deadline;
                    _logger.Info($"Выбрана задача: {task.Title} (ID: {task.Id})");
                }
                else
                {
                    _currentEditingId = null;
                    _logger.Debug("Выбор задачи сброшен");
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Ошибка при выборе задачи");
            }

            btnEdit.Enabled = _currentEditingId.HasValue;
            btnDelete.Enabled = _currentEditingId.HasValue;
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            _logger.Info("Подготовка к добавлению новой задачи");
            ClearInputs();
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (!_currentEditingId.HasValue) return;

            try
            {
                var result = MessageBox.Show("Удалить выбранную задачу?", "Подтверждение",
                                           MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    _logger.Info($"Удаление задачи ID: {_currentEditingId}");
                    await _taskService.DeleteTaskAsync(_currentEditingId.Value);
                    await LoadTasksAsync();
                    ClearInputs();
                    _logger.Info("Задача успешно удалена");
                }
                else
                {
                    _logger.Debug("Удаление задачи отменено пользователем");
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Ошибка при удалении задачи ID: {_currentEditingId}");
                MessageBox.Show("Ошибка при удалении задачи", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnEdit_Click(object sender, EventArgs e)
        {
            if (!_currentEditingId.HasValue || string.IsNullOrWhiteSpace(txtTitle.Text)) return;

            try
            {
                _logger.Info($"Редактирование задачи ID: {_currentEditingId}");

                var existingTask = await _taskService.GetTaskByIdAsync(_currentEditingId.Value);
                existingTask.Title = txtTitle.Text;
                existingTask.Description = txtDescription.Text;
                existingTask.Deadline = dateTimePickerDeadline.Value;

                await _taskService.UpdateTaskAsync(existingTask);
                await LoadTasksAsync();

                _logger.Info("Задача успешно обновлена");
                MessageBox.Show("Задача успешно обновлена!", "Успех",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Ошибка при обновлении задачи");
                MessageBox.Show("Ошибка при обновлении задачи", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task LoadTasksAsync()
        {
            try
            {
                _logger.Debug("Загрузка списка задач");
                listView1.Items.Clear();
                var tasks = await _taskService.GetTasksAsync();

                foreach (var task in tasks)
                {
                    var item = new ListViewItem(task.Title)
                    {
                        Tag = task.Id,
                        BackColor = task.Deadline < DateTime.Now ? Color.LightPink : Color.White
                    };
                    item.SubItems.Add(task.Description);
                    item.SubItems.Add(task.Deadline.ToString("dd.MM.yyyy"));
                    listView1.Items.Add(item);
                }

                _logger.Info($"Загружено {tasks.Count} задач");
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Ошибка при загрузке задач");
                throw;
            }
        }

        private void ClearInputs()
        {
            _logger.Debug("Очистка полей ввода");
            _currentEditingId = null;
            txtTitle.Text = "";
            txtDescription.Text = "";
            dateTimePickerDeadline.Value = DateTime.Now;
            listView1.SelectedItems.Clear();
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                _logger.Warn("Попытка сохранения задачи без названия");
                MessageBox.Show("Введите название задачи!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (_currentEditingId.HasValue)
                {
                    _logger.Info($"Сохранение изменений задачи ID: {_currentEditingId}");

                    var task = await _taskService.GetTaskByIdAsync(_currentEditingId.Value);
                    task.Title = txtTitle.Text;
                    task.Description = txtDescription.Text;
                    task.Deadline = dateTimePickerDeadline.Value;

                    await _taskService.UpdateTaskAsync(task);
                    _logger.Info("Изменения задачи сохранены");
                    MessageBox.Show("Задача обновлена!", "Успех",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    _logger.Info($"Создание новой задачи: '{txtTitle.Text}'");

                    var newTask = new Tasks
                    {
                        Title = txtTitle.Text,
                        Description = txtDescription.Text,
                        Deadline = dateTimePickerDeadline.Value
                    };

                    await _taskService.AddTaskAsync(newTask);
                    _logger.Info("Новая задача успешно создана");
                    MessageBox.Show("Задача добавлена!", "Успех",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                await LoadTasksAsync();
                ClearInputs();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Ошибка при сохранении задачи");
                MessageBox.Show("Ошибка при сохранении задачи", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}