// <copyright file="MigratorHostedService.cs" company="Isaac Brown">
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using Microsoft.Azure.Cosmos;
using Plouton.Persistence.CosmosDb;

namespace Plouton.Web.Api.HostedServices;

/// <summary>
/// Hosted service responsible for ensuring required Cosmos databases and collections exist.
/// </summary>
public class MigratorHostedService : IHostedService
{
    private readonly CosmosClient client;
    private readonly ILogger<MigratorHostedService> logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="MigratorHostedService"/> class.
    /// </summary>
    /// <param name="client">The Cosmos client to use.</param>
    /// <param name="logger">The logger to write messages to.</param>
    public MigratorHostedService(CosmosClient client, ILogger<MigratorHostedService> logger)
    {
        this.client = client;
        this.logger = logger;
    }

    /// <inheritdoc/>
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        // Ensure everything exists.
        // First, the database.
        this.logger.LogInformation("Ensuring the database {databaseName} exists", DatabasesMetadata.Plouton.Name);
        await this.client.CreateDatabaseIfNotExistsAsync(DatabasesMetadata.Plouton.Name, cancellationToken: cancellationToken);

        Database ploutonDatabase = this.client.GetDatabase(DatabasesMetadata.Plouton.Name);

        // Next the invoices collection.
        this.logger.LogInformation(
            "Ensuring the collection {collectionName} in database {databaseName} exists",
            DatabasesMetadata.Plouton.Collections.Invoices.Name,
            DatabasesMetadata.Plouton.Name);
        await ploutonDatabase
                    .CreateContainerIfNotExistsAsync(DatabasesMetadata.Plouton.Collections.Invoices.ContainerProperties, cancellationToken: cancellationToken);

        // Finally, the counters collection.
        this.logger.LogInformation(
            "Ensuring the collection {collectionName} in database {databaseName} exists",
            DatabasesMetadata.Plouton.Collections.Counters.Name,
            DatabasesMetadata.Plouton.Name);
        await ploutonDatabase
                    .CreateContainerIfNotExistsAsync(DatabasesMetadata.Plouton.Collections.Counters.ContainerProperties, cancellationToken: cancellationToken);

        this.logger.LogInformation(
            "Ensuring the collection {collectionName} in database {databaseName} has a seed document",
            DatabasesMetadata.Plouton.Collections.Counters.Name,
            DatabasesMetadata.Plouton.Name);

        // TODO: Figure out what to do with this bit. Not happy with it sitting here.
        // Thinking that it could be farmed off to the `CosmosIdGenerator` instance.
        // Perhaps as a one off action when first calling `NextIdAsync`?
        // It feels wrong for the migrator to know internal implementation details of the IdGenerator.
        try
        {
            await ploutonDatabase.GetContainer(DatabasesMetadata.Plouton.Collections.Counters.Name)
                                 .CreateItemAsync(
                                    item: new { id = "invoiceCounter", value = 1 },
                                    partitionKey: new PartitionKey("invoiceCounter"),
                                    cancellationToken: cancellationToken);
        }
        catch (CosmosException e)
        {
            if (e.StatusCode != System.Net.HttpStatusCode.Conflict)
            {
                throw;
            }

            this.logger.LogInformation(
                "No seed document added, the collection {collectionName} in database {databaseName} has a seed document already",
                DatabasesMetadata.Plouton.Collections.Counters.Name,
                DatabasesMetadata.Plouton.Name);
        }
    }

    /// <inheritdoc/>
    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}