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
            _logger.Info("����� ����� ����������������");
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            try
            {
                _logger.Debug("�������� ����� ��� �������� �����");
                await LoadTasksAsync();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "������ ��� �������� �����");
                MessageBox.Show("�� ������� ��������� ������", "������",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigureListView()
        {
            listView1.SelectedIndexChanged += ListView1_SelectedIndexChanged;
            listView1.Columns.Add("��������", 200);
            listView1.Columns.Add("��������", 300);
            listView1.Columns.Add("���� ����������", 150);
            _logger.Debug("ListView ���������������");
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
                    _logger.Info($"������� ������: {task.Title} (ID: {task.Id})");
                }
                else
                {
                    _currentEditingId = null;
                    _logger.Debug("����� ������ �������");
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "������ ��� ������ ������");
            }

            btnEdit.Enabled = _currentEditingId.HasValue;
            btnDelete.Enabled = _currentEditingId.HasValue;
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            _logger.Info("���������� � ���������� ����� ������");
            ClearInputs();
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (!_currentEditingId.HasValue) return;

            try
            {
                var result = MessageBox.Show("������� ��������� ������?", "�������������",
                                           MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    _logger.Info($"�������� ������ ID: {_currentEditingId}");
                    await _taskService.DeleteTaskAsync(_currentEditingId.Value);
                    await LoadTasksAsync();
                    ClearInputs();
                    _logger.Info("������ ������� �������");
                }
                else
                {
                    _logger.Debug("�������� ������ �������� �������������");
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"������ ��� �������� ������ ID: {_currentEditingId}");
                MessageBox.Show("������ ��� �������� ������", "������",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnEdit_Click(object sender, EventArgs e)
        {
            if (!_currentEditingId.HasValue || string.IsNullOrWhiteSpace(txtTitle.Text)) return;

            try
            {
                _logger.Info($"�������������� ������ ID: {_currentEditingId}");

                var existingTask = await _taskService.GetTaskByIdAsync(_currentEditingId.Value);
                existingTask.Title = txtTitle.Text;
                existingTask.Description = txtDescription.Text;
                existingTask.Deadline = dateTimePickerDeadline.Value;

                await _taskService.UpdateTaskAsync(existingTask);
                await LoadTasksAsync();

                _logger.Info("������ ������� ���������");
                MessageBox.Show("������ ������� ���������!", "�����",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "������ ��� ���������� ������");
                MessageBox.Show("������ ��� ���������� ������", "������",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task LoadTasksAsync()
        {
            try
            {
                _logger.Debug("�������� ������ �����");
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

                _logger.Info($"��������� {tasks.Count} �����");
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "������ ��� �������� �����");
                throw;
            }
        }

        private void ClearInputs()
        {
            _logger.Debug("������� ����� �����");
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
                _logger.Warn("������� ���������� ������ ��� ��������");
                MessageBox.Show("������� �������� ������!", "������",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (_currentEditingId.HasValue)
                {
                    _logger.Info($"���������� ��������� ������ ID: {_currentEditingId}");

                    var task = await _taskService.GetTaskByIdAsync(_currentEditingId.Value);
                    task.Title = txtTitle.Text;
                    task.Description = txtDescription.Text;
                    task.Deadline = dateTimePickerDeadline.Value;

                    await _taskService.UpdateTaskAsync(task);
                    _logger.Info("��������� ������ ���������");
                    MessageBox.Show("������ ���������!", "�����",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    _logger.Info($"�������� ����� ������: '{txtTitle.Text}'");

                    var newTask = new Tasks
                    {
                        Title = txtTitle.Text,
                        Description = txtDescription.Text,
                        Deadline = dateTimePickerDeadline.Value
                    };

                    await _taskService.AddTaskAsync(newTask);
                    _logger.Info("����� ������ ������� �������");
                    MessageBox.Show("������ ���������!", "�����",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                await LoadTasksAsync();
                ClearInputs();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "������ ��� ���������� ������");
                MessageBox.Show("������ ��� ���������� ������", "������",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}