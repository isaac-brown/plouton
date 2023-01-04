// <copyright file="CosmosIdGenerator.cs" company="Isaac Brown">
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using Microsoft.Azure.Cosmos;
using Newtonsoft.Json;
using Plouton.Domain;

namespace Plouton.Persistence.CosmosDb;

/// <summary>
/// Implementation of <see cref="IdGenerator"/> using Cosmos DB as the backing store.
/// </summary>
public class CosmosIdGenerator : IdGenerator
{
    private readonly CosmosClient client;

    /// <summary>
    /// Initializes a new instance of the <see cref="CosmosIdGenerator"/> class.
    /// </summary>
    /// <param name="client">The Cosmos DB client to use to connect to the backing store.</param>
    public CosmosIdGenerator(CosmosClient client)
    {
        this.client = client;
    }

    /// <inheritdoc/>
    public override async Task<int> NextIdAsync()
    {
        // TODO: Logging could be improved: it wasn't clear that the patch operation was failing during creation of a new invoice.
        var container = this.client.GetDatabase(DatabasesMetadata.Plouton.Name)
                                   .GetContainer(DatabasesMetadata.Plouton.Collections.Counters.Name);
        IReadOnlyList<PatchOperation> patchOperations = new List<PatchOperation>()
        {
            PatchOperation.Increment("/value", 1L),
        };

        GenericCounter counter = await container.PatchItemAsync<GenericCounter>("invoiceCounter", new PartitionKey("invoiceCounter"), patchOperations);

        return counter.Value;
    }

    private class GenericCounter
    {
        [JsonProperty(propertyName: "id")]
        public string Id { get; set; } = string.Empty;

        [JsonProperty(propertyName: "value")]
        public int Value { get; set; }
    }
}