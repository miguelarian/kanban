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

app.MapGet("/tasks/{id}", (TasksService tasksService, int id) =>
{
    KTask result = tasksService.GetTask(id);
    if (result == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(result);
})
.WithOpenApi();

app.MapPost("/tasks", (TasksService tasksService, KTask task) =>
{
    tasksService.AddTask(task);
    return Results.Created($"/tasks/{task.Id}", task);
})
.WithOpenApi();

app.MapDelete("/tasks/{id}", (TasksService tasksService, int id) =>
{
    KTask result = tasksService.GetTask(id);
    if (result == null)
    {
        return Results.NotFound();
    }

    tasksService.DeleteTask(id);
    return Results.NoContent();
});

app.Run();
