// <copyright file="CosmosInvoiceRepository.cs" company="Isaac Brown">
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Text;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using Plouton.Domain;
using Plouton.Domain.Entities;
using Plouton.Persistence.Abstractions;
using Plouton.Persistence.CosmosDb.Extensions;
using Plouton.Persistence.CosmosDb.Records;

namespace Plouton.Persistence.CosmosDb;

/// <summary>
/// Cosmos DB implementation of <see cref="InvoiceRepository"/>.
/// </summary>
public class CosmosInvoiceRepository : InvoiceRepository
{
    private readonly CosmosClient client;
    private readonly IdGenerator idGenerator;

    /// <summary>
    /// Initializes a new instance of the <see cref="CosmosInvoiceRepository"/> class.
    /// </summary>
    /// <param name="client">The cosmos db client to use.</param>
    /// <param name="idGenerator">Used to generate sequential identifiers.</param>
    public CosmosInvoiceRepository(CosmosClient client, IdGenerator idGenerator)
    {
        this.client = client;
        this.idGenerator = idGenerator;
    }

    private Container Container => this.client.GetDatabase("Plouton").GetContainer("Invoices");

    /// <inheritdoc/>
    public override async Task<PagedCollection<Invoice>> ReadManyAsync(int limit, string? token, CancellationToken cancellationToken)
    {
        // Base64 decode the token, if it has a value.
        if (token is not null)
        {
            var base64EncodedBytes = Convert.FromBase64String(token);
            token = Encoding.UTF8.GetString(base64EncodedBytes);
        }

        // Ask for no more the 'limit' number of items per query execution.
        var requestOptions = new QueryRequestOptions
        {
            MaxItemCount = limit,
        };
        var queryable = this.Container.GetItemLinqQueryable<InvoiceRecord>(requestOptions: requestOptions, continuationToken: token);
        var matches = queryable.OrderByDescending(record => record.WhenCreated);

        using var feedIterator = matches.ToFeedIterator();
        var items = new List<InvoiceRecord>();
        string? continuationToken = null;
        while (feedIterator.HasMoreResults)
        {
            FeedResponse<InvoiceRecord> response = await feedIterator.ReadNextAsync(cancellationToken);
            items.AddRange(response.Resource);
            continuationToken = response.ContinuationToken;

            // Break once we have got some results.
            if (response.Count > 0)
            {
                break;
            }
        }

        // Base64 encode the continuation token to make life less hard for others.
        if (continuationToken is not null)
        {
            continuationToken = Convert.ToBase64String(Encoding.UTF8.GetBytes(continuationToken));
        }

        return new PagedCollection<Invoice>
        {
            Items = items.Select(item => item.ToInvoice()).ToList(),
            Token = continuationToken,
            Limit = limit,
        };
    }

    /// <inheritdoc/>
    public override async Task<Invoice> CreateAsync(Guid key, Invoice item, CancellationToken cancellationToken)
    {
        var record = item.ToInvoiceRecord();

        var nextId = await this.idGenerator.NextIdAsync();
        record.InvoiceNumber = $"INV{nextId:000000}";

        var partitionKey = new PartitionKey(key.ToString());

        record = await this.Container.CreateItemAsync(record, partitionKey, cancellationToken: cancellationToken);
        item = record.ToInvoice();
        return item;
    }

    /// <inheritdoc/>
    public override async Task<Invoice?> ReadAsync(Guid key, CancellationToken cancellationToken)
    {
        var partitionKey = new PartitionKey(key.ToString());
        InvoiceRecord record = await this.Container.ReadItemAsync<InvoiceRecord>(key.ToString(), partitionKey, cancellationToken: cancellationToken);

        return record.ToInvoice();
    }

    /// <inheritdoc/>
    public override async Task<Invoice> UpdateAsync(Invoice item, CancellationToken cancellationToken)
    {
        var id = item.Id.ToString();
        var partitionKey = new PartitionKey(id);
        var record = item.ToInvoiceRecord();
        record = await this.Container.UpsertItemAsync(record, partitionKey, cancellationToken: cancellationToken);
        item = record.ToInvoice();
        return item;
    }

    /// <inheritdoc/>
    public override async Task DeleteAsync(Guid key, CancellationToken cancellationToken)
    {
        var partitionKey = new PartitionKey(key.ToString());

        await this.Container.DeleteItemAsync<InvoiceRecord>(key.ToString(), partitionKey, cancellationToken: cancellationToken);
    }
}
