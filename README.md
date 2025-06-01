1. ITaskRepository и TaskRepository:
   Асинхронные методы:
   GetAllAsync()-получает список всех задач, это долгая операция чтения из бд
   GetTaskByIdAsync(Guid id)-получает задачу,	использует FindAsync, работает с бд
   AddAsync(Tasks task)-добавляет новую задачу и сохраняет изменения, использует AddAsync и SaveChangesAsync, работает с бд
   UpdateAsync(Tasks task)-обновляет задачу и сохраняет изменения, сохраняет через SaveChangesAsync, работет с бд
   DeleteAsync(Guid id)-удаляет задачу,	работает с бд
2. ITaskService и TaskService:
  Асинхронные методы:
   GetTasksAsync()-вызывает GetAllAsync() из репозитория
   GetTaskByIdAsync(Guid id)-вызывает GetTaskByIdAsync() из репозитория
   AddTaskAsync(Tasks task)- вызывает AddAsync() из репозитория
   UpdateTaskAsync(Tasks task)- вызывает UpdateAsync() из репозитория
   DeleteTaskAsync(Guid id)- вызывает DeleteAsync() из репозитория 
3. TasksForm:
  Асинхронность:
   OnLoad(EventArgs e)-использует LoadTasksAsync()
   ListView1_SelectedIndexChanged(object sender, EventArgs e)-использует GetTaskByIdAsync(id)
   LoadTasksAsync()-запрос к БД может занять время, использует GetTasksAsync()
   btnSave_Click(object sender, EventArgs e)-вызывает AddTaskAsync()
   btnEdit_Click(object sender, EventArgs e)- вызывает UpdateTaskAsync()
   btnDelete_Click(object sender, EventArgs e)- вызывает DeleteTaskAsync()
