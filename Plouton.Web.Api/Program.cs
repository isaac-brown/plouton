// <copyright file="Program.cs" company="Isaac Brown">
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using Microsoft.Azure.Cosmos;
using Plouton.Domain;
using Plouton.Persistence.Abstractions;
using Plouton.Persistence.CosmosDb;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<InvoiceRepository, CosmosInvoiceRepository>();
builder.Services.AddSingleton(cfg =>
{
    string connectionString = builder.Configuration.GetConnectionString("Plouton");
    return new CosmosClient(connectionString);
});
builder.Services.AddSingleton<IdGenerator, CosmosIdGenerator>();

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
