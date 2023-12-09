using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Npgsql;
using static TarefasApi.Data.TarefasContext;

namespace TarefasApi.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static WebApplicationBuilder AddPersistence(this WebApplicationBuilder builder)
        {
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddScoped<GetConnection>(sp => 
                async () => {
                    var connection = new NpgsqlConnection(connectionString);
                    await connection.OpenAsync();
                    return connection;
                }
            );
            return builder;
        }
    }
}