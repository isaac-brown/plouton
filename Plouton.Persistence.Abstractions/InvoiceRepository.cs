// <copyright file="InvoiceRepository.cs" company="Isaac Brown">
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using Plouton.Domain.Entities;

namespace Plouton.Persistence.Abstractions;

/// <summary>
/// Base implementation of a repository which stores <see cref="Invoice"/>s.
/// </summary>
public abstract class InvoiceRepository : ICrudRepository<Guid, Invoice>
{
    /// <summary>
    /// Gets multiple items which match a given query.
    /// </summary>
    /// <param name="limit">The maximum number of invoices to take.</param>
    /// <param name="token">A token used to continue querying if more records exists than match the query.</param>
    /// <param name="cancellationToken">Used to cancel the asynchronous operation.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
    public abstract Task<PagedCollection<Invoice>> ReadManyAsync(int limit, string? token, CancellationToken cancellationToken);

    /// <inheritdoc/>
    public abstract Task<Invoice> CreateAsync(Guid key, Invoice item, CancellationToken cancellationToken);

    /// <inheritdoc/>
    public abstract Task DeleteAsync(Guid key, CancellationToken cancellationToken);

    /// <inheritdoc/>
    public abstract Task<Invoice?> ReadAsync(Guid key, CancellationToken cancellationToken);

    /// <inheritdoc/>
    public abstract Task<Invoice> UpdateAsync(Guid key, Invoice item, CancellationToken cancellationToken);
}
