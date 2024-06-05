﻿namespace Kanban.Domain.Tasks
{
    public class Task
    {
        public TaskId Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Status Status { get; set; }
    }
}
