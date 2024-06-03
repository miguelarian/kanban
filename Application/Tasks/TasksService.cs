namespace Kanban.Application.Tasks
{
    using Kanban.Domain.Tasks;

    public class TasksService
    {
        private ITaskRepository tasksRepository;

        public TasksService(ITaskRepository tasksRepository)
        {
            this.tasksRepository = tasksRepository;
        }

        public void AddTask(Task task)
        {
            tasksRepository.Add(task);
        }

        public List<Task> GetTasks()
        {
            return tasksRepository.Get();
        }

        public Task GetTask(int id)
        {
            return tasksRepository.Get(id);
        }
    }
}
