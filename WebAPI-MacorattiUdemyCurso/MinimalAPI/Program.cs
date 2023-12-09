using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("TarefasDb"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/", () => "Olá Mundo!");

app.MapGet("frases", async () => 
    await new HttpClient().GetStringAsync("https://ron-swanson-quotes.herokuapp.com/v2/quotes")
);

app.MapGet("tarefas", async (AppDbContext db) => { return await db.Tarefas.ToListAsync(); });

app.MapGet("tarefa/{id}", async (int id, AppDbContext db) => 
    { 
        return await db.Tarefas.FindAsync(id) is Tarefa tarefa ? Results.Ok(tarefa) : Results.NotFound(); 
    });

app.MapGet("tarefas/concluidas", async (AppDbContext db) => { return await db.Tarefas.Where(t => t.IsConcluida).ToListAsync(); });

app.MapPost("postartarefa", async (Tarefa tarefa, AppDbContext db) => 
    { 
        db.Tarefas.Add(tarefa);
        await db.SaveChangesAsync();
        return Results.Created($"tarefas/{tarefa.Id}", tarefa); 
    });

app.MapPut("tarefa/{id}", async (Tarefa atualizaTarefa, int id, AppDbContext db) => 
    { 
        var tarefa = await db.Tarefas.FindAsync(id); 
        if (tarefa is null)
        {
            return Results.NotFound();
        }
        tarefa.NomeTarefa = atualizaTarefa.NomeTarefa;
        tarefa.IsConcluida = atualizaTarefa.IsConcluida;
        await db.SaveChangesAsync();
        return Results.NoContent();
    });

app.MapDelete("tarefa/{id}", async (int id, AppDbContext db) => 
    { 
        if (await db.Tarefas.FindAsync(id) is Tarefa tarefa)
        {
            db.Tarefas.Remove(tarefa);
            await db.SaveChangesAsync();
            return Results.Ok();
        }
        return Results.NotFound();
    });

app.Run();

class Tarefa
{
    public int Id { get; set; }
    public string? NomeTarefa { get; set; }
    public bool IsConcluida { get; set; }
}

class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {}

    public DbSet<Tarefa> Tarefas => Set<Tarefa>();
}