// <copyright file="Program.cs" company="Isaac Brown">
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using Keycloak.AuthServices.Authentication;
using Keycloak.AuthServices.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Azure.Cosmos;
using Plouton.Domain;
using Plouton.Persistence.Abstractions;
using Plouton.Persistence.CosmosDb;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Authentication and authorization.
builder.Services.AddKeycloakAuthentication(builder.Configuration);
builder.Services.AddKeycloakAuthorization(builder.Configuration);

builder.Services.AddAuthorization(o =>
{
    o.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();

    o.AddPolicy("IsInvoiceReader", policy =>
    {
        // realm_access.roles
        policy.RequireRealmRoles("invoice:read", "invoice:write", "invoice:delete");

        // resource_access.<resource-name>.roles
        // policy.RequireResourceRoles("invoice:read", "invoice:write");
    });

    o.AddPolicy("IsInvoiceWriter", policy =>
    {
        policy.RequireRealmRoles("invoice:write", "invoice:delete");
    });

    o.AddPolicy("IsInvoiceDeleter", policy =>
    {
        policy.RequireRealmRoles("invoice:delete");
    });
});

// Custom services.
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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
