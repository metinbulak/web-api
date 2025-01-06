


using Marten;
using Marten.Services;
using Microsoft.AspNetCore.Http.Json;
using web_api.config;
using web_api.dto;

var builder = WebApplication.CreateBuilder(args);


builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.WriteIndented = true;
});

builder.Services.AddMarten(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("MartenDatabase");
    options.Connection(connectionString);
    options.AutoCreateSchemaObjects = Weasel.Core.AutoCreate.All;
    // options.Schema.For<KeyValue>(); // Map KeyValue model
    options.Schema.For<KeyValue>()
            .Identity(x => x.Key);
    options.Serializer(new SystemTextJsonSerializer());
});



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
