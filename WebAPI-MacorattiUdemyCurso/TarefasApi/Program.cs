using TarefasApi.Extensions;
using TarefasApi.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.AddPersistence();
var app = builder.Build();

app.MapTarefasEndpoints();

app.Run();
