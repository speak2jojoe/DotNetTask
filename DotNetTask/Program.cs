using DotNetTask.Controllers;
using DotNetTask.Interfaces;
using DotNetTask.Services;
using Microsoft.Azure.Cosmos;

using CosmosClient client = new(
    accountEndpoint: "https://localhost:8081/",
    authKeyOrResourceToken: "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw=="
);

Database database = await client.CreateDatabaseIfNotExistsAsync(
    id: "dotnettask",
    throughput: 1000
);

Container container = await database.CreateContainerIfNotExistsAsync(
    id: "QuestionType",
    partitionKeyPath: "/id"
);

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

namespace DotNetTask
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    // Register services
                    services.AddTransient<IDotNetTaskService, DotNetTaskService>();

                    // Register your entry point class if needed
                    services.AddTransient<DotNetTaskController>();
                });
    }
}




