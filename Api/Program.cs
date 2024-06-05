using Kanban.Application.Tasks;
using Kanban.Domain.Tasks;
using KTask = Kanban.Domain.Tasks.Task;
using Kanban.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ITaskRepository, InmemoryTasksRepository>();
builder.Services.AddScoped<TasksService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/tasks", () =>
{
    using var scope = app.Services.CreateScope();
    var tasksService = scope.ServiceProvider.GetRequiredService<TasksService>();
    return tasksService.GetTasks();
})
.WithOpenApi();

app.MapGet("/tasks/{id:guid}", (TasksService tasksService, Guid id) =>
{
    TaskId taskId = new TaskId(id);
    KTask task = tasksService.GetTask(taskId);
    if (task == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(task);
})
.WithOpenApi();

app.MapPost("/tasks", (TasksService tasksService, KTask task) =>
{
    tasksService.AddTask(task);
    return Results.Created($"/tasks/{task.Id}", task);
})
.WithOpenApi();

app.MapDelete("/tasks/{id:guid}", (TasksService tasksService, Guid id) =>
{
    TaskId taskId = new TaskId(id);
    KTask task = tasksService.GetTask(taskId);
    if (task == null)
    {
        return Results.NotFound();
    }

    tasksService.DeleteTask(task.Id);
    return Results.NoContent();
});

app.Run();
