namespace Kanban.Infrastructure
{
    using Kanban.Domain.Tasks;

    public class InmemoryTasksRepository : ITaskRepository
    {
        private static List<Task> tasks = new List<Task> {
            new Task
            {
                Id = 1,
                Title = "Task 1",
                Description = "Task 1 is not started",
                Status = Status.Todo
            },
            new Task
            {
                Id = 2,
                Title = "Task 2",
                Description = "Task 2 is in progress",
                Status = Status.InProgress
            },
            new Task
            {
                Id = 3,
                Title = "Task 3",
                Description = "Task 3 is done",
                Status = Status.Done
            }
        };

        public void Add(Task task)
        {
            tasks.Add(task);
        }

        public List<Task> Get()
        {
            return tasks;
        }

        public Task Get(int id)
        {
            return tasks.FirstOrDefault(t => t.Id == id);
        }

        public void Remove(int id)
        {
            tasks.RemoveAll(t => t.Id == id);
        }
    }
}
