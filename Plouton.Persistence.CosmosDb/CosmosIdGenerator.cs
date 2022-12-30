// <copyright file="CosmosIdGenerator.cs" company="Isaac Brown">
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using Microsoft.Azure.Cosmos;
using Newtonsoft.Json;
using Plouton.Domain;

namespace Plouton.Persistence.CosmosDb;

public class CosmosIdGenerator : IdGenerator
{
    private readonly CosmosClient client;

    public CosmosIdGenerator(CosmosClient client)
    {
        this.client = client;
    }

    public override async Task<int> NextIdAsync()
    {
        var container = this.client.GetDatabase("Plouton").GetContainer("Counters");
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
        public string Id { get; set; }

        [JsonProperty(propertyName: "value")]
        public int Value { get; set; }
    }
}