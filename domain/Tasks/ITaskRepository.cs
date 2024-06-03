namespace Kanban.Domain.Tasks
{
    public interface ITaskRepository
    {
        void Add(Task task);
        List<Task> Get();
        Task Get(int id);
    }
}
