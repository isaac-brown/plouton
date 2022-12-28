// <copyright file="ICrudRepository.cs" company="Isaac Brown">
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Plouton.Persistence.Abstractions;

/// <summary>
/// Represents a repository which can be used to create, read, update or delete instances of <see cref="TValue"/>.
/// </summary>
/// <typeparam name="TKey">The key used to uniquely identify records.</typeparam>
/// <typeparam name="TValue">The items which are stored in this repository.</typeparam>
public interface ICrudRepository<TKey, TValue>
where TKey : struct
where TValue : class
{
    /// <summary>
    /// Asynchronously creates the given <paramref name="item"/> in the backing store.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <param name="key">The <see cref="TKey"/> to store the item with.
    /// <param name="item">The instance of <see cref="TValue"/> to store.</param>
    /// <param name="cancellationToken">Used to cancel the asynchronous operation.</param>
    Task<TValue> CreateAsync(TKey key, TValue item, CancellationToken cancellationToken);

    /// <summary>
    /// Asynchronously retrieves the given <paramref name="item"/> from the backing store.
    /// If no <<see cref="TValue"/> exists with the given <paramref name="key"/>, then null is returned.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <param name="key">The <see cref="TKey"/> of the item to retrieve.
    /// <param name="cancellationToken">Used to cancel the asynchronous operation.</param>
    Task<TValue?> ReadAsync(TKey key, CancellationToken cancellationToken);

    /// <summary>
    /// Asynchronously updates the given <paramref name="item"/> in the backing store.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <param name="key">The <see cref="TKey"/> of the item to update.
    /// <param name="item">The instance of <see cref="TValue"/> to update.</param>
    /// <param name="cancellationToken">Used to cancel the asynchronous operation.</param>
    Task<TValue> UpdateAsync(TKey key, TValue item, CancellationToken cancellationToken);

    /// <summary>
    /// Asynchronously deletes the given <paramref name="item"/> in the backing store.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <param name="key">The <see cref="TKey"/> of the item to delete.
    /// <param name="cancellationToken">Used to cancel the asynchronous operation.</param>
    Task DeleteAsync(TKey key, CancellationToken cancellationToken);
}