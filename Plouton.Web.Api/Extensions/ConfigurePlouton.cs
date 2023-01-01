// <copyright file="ConfigurePlouton.cs" company="Isaac Brown">
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using Microsoft.Azure.Cosmos;
using Plouton.Domain;
using Plouton.Persistence.Abstractions;
using Plouton.Persistence.CosmosDb;

namespace Plouton.Web.Api.Extensions;

/// <summary>
/// Extension methods to configure Plouton-specific services.
/// </summary>
public static class ConfigurePlouton
{
    /// <summary>
    /// Adds implementations of Plouton-specific interfaces and abstract classes to the given <paramref name="services"/>.
    /// </summary>
    /// <param name="services">The service collection to configure.</param>
    /// <param name="configuration">Used to source configuration values from.</param>
    /// <returns>An <see cref="IServiceCollection"/> for chaining.</returns>
    public static IServiceCollection AddPlouton(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<InvoiceRepository, CosmosInvoiceRepository>();
        services.AddSingleton(cfg =>
        {
            string connectionString = configuration.GetConnectionString("Plouton");
            return new CosmosClient(connectionString);
        });
        services.AddSingleton<IdGenerator, CosmosIdGenerator>();

        return services;
    }
}