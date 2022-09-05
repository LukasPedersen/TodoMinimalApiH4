using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<TodoDb>(opt => opt.UseInMemoryDatabase("TodoList"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

#region Debug
app.MapGet("/", () => "Hello World!");
#endregion

#region API GET
app.MapGet("/GetTodoItems/Uncomplete", async (TodoDb db) =>
    await db.Todos.ToListAsync());



app.MapGet("/GetTodoItems/Complete", async (TodoDb db) =>
    await db.Todos.Where(t => t.Completed).ToListAsync());

app.MapGet("/GetTodoItem/{id}", async (int id, TodoDb db) =>
    await db.Todos.FindAsync(id)
        is Todo todo
            ? Results.Ok(todo)
            : Results.NotFound());
#endregion

#region API POST
app.MapPost("/CreateTodoItem", async (Todo todo, TodoDb db) =>
{
    db.Todos.Add(todo);
    await db.SaveChangesAsync();

    return Results.Created($"/todoitems/{todo.Id}", todo);
});
#endregion

#region API PUT
app.MapPut("/UpdateTodoitem/{id}", async (int id, Todo inputTodo, TodoDb db) =>
{
    var todo = await db.Todos.FindAsync(id);

    if (todo is null) return Results.NotFound();

    todo.Description = inputTodo.Description;
    todo.Completed = inputTodo.Completed;

    await db.SaveChangesAsync();

    return Results.NoContent();
});
#endregion

#region API DELETE
app.MapDelete("/DeleteTodoitem/{id}", async (int id, TodoDb db) =>
{
    if (await db.Todos.FindAsync(id) is Todo todo)
    {
        db.Todos.Remove(todo);
        await db.SaveChangesAsync();
        return Results.Ok(todo);
    }

    return Results.NotFound();
});
#endregion

app.Run();

class Todo
{
    public enum Prioritys { Low, Normal, High }

    public UInt16 Id { get; set; }
    public string Description { get; set; }
    public DateTime CreatedTime { get; } = DateTime.Now;
    public bool Completed { get; set; } = false;
    public Prioritys Priority { get; set; }
}

class TodoDb : DbContext
{
    public TodoDb(DbContextOptions<TodoDb> options)
        : base(options) { }

    public DbSet<Todo> Todos => Set<Todo>();
}