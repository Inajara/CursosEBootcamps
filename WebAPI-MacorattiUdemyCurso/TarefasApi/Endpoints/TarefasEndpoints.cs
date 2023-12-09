using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;
using TarefasApi.Data;
using static TarefasApi.Data.TarefasContext;

namespace TarefasApi.Endpoints
{
    public static class TarefasEndpoints
    {
        public static void MapTarefasEndpoints(this WebApplication app){
            app.MapGet("/", () => $"Welcome to the Tasks API {DateTime.Now}");

            app.MapGet("/tarefas", async(GetConnection connectionGetter) => {
                using var con = await connectionGetter();
                var tarefas = con.GetAll<Tarefas>().ToList();

                if(tarefas is null)
                    return Results.NotFound();
                
                return Results.Ok(tarefas);
            });

            app.MapGet("/tarefas/{id}", async(GetConnection connectionGetter, int id) => {
                using var con = await connectionGetter();
                var tarefa = con.Get<Tarefas>(id);

                return tarefa is Tarefas ? Results.Ok(tarefa): Results.NotFound();
            });

            app.MapPost("/tarefas", async(GetConnection connectionGetter, Tarefas tarefa) => {
                using var con = await connectionGetter();
                var id = con.Insert(tarefa);

                return Results.Created($"/tarefas/{id}", tarefa);
            });

            app.MapPut("/tarefas", async(GetConnection connectionGetter, Tarefas tarefa) => {
                using var con = await connectionGetter();
                var id = con.Update(tarefa);

                return Results.Ok();
            });

            app.MapDelete("/tarefas/{id}", async(GetConnection connectionGetter, int id) => {
                using var con = await connectionGetter();
                var deleted = con.Get<Tarefas>(id);

                if(deleted is null)
                    return Results.NotFound();
                
                con.Delete(deleted);
                return Results.Ok(deleted);
            });
        }
    }
}