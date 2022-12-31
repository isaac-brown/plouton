// <copyright file="ICrudRepository.cs" company="Isaac Brown">
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Plouton.Persistence.Abstractions;

/// <summary>
/// Represents a repository which can be used to create, read, update or delete instances of <typeparamref name="TValue"/>.
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
    /// <param name="key">The <typeparamref name="TKey"/> to store the item with.</param>
    /// <param name="item">The instance of <typeparamref name="TValue"/> to store.</param>
    /// <param name="cancellationToken">Used to cancel the asynchronous operation.</param>
    /// <returns>
    /// A <see cref="Task"/> representing the asynchronous operation.
    /// The value will be a new instance of <typeparamref name="TValue"/>, with any updates made during storage.
    /// </returns>
    Task<TValue> CreateAsync(TKey key, TValue item, CancellationToken cancellationToken);

    /// <summary>
    /// Asynchronously retrieves the item from the backing store which matches the given <paramref name="key"/>.
    /// If no <typeparamref name="TKey"/> exists with the given <paramref name="key"/>, then null is returned.
    /// </summary>
    /// <param name="key">The <typeparamref name="TKey"/> of the item to retrieve.</param>
    /// <param name="cancellationToken">Used to cancel the asynchronous operation.</param>
    /// <returns>
    /// A <see cref="Task"/> representing the asynchronous operation.
    /// The value will be a new instance of <typeparamref name="TValue"/>.
    /// </returns>
    Task<TValue?> ReadAsync(TKey key, CancellationToken cancellationToken);

    /// <summary>
    /// Asynchronously updates the given <paramref name="item"/> in the backing store.
    /// </summary>
    /// <param name="item">The instance of <typeparamref name="TValue"/> to update.</param>
    /// <param name="cancellationToken">Used to cancel the asynchronous operation.</param>
    /// <returns>
    /// A <see cref="Task"/> representing the asynchronous operation.
    /// The value will be a new instance of <typeparamref name="TValue"/>, with any updates made during storage.
    /// </returns>
    Task<TValue> UpdateAsync(TValue item, CancellationToken cancellationToken);

    /// <summary>
    /// Asynchronously deletes the item in the backing store with the given <paramref name="key"/>.
    /// </summary>
    /// <param name="key">The <typeparamref name="TKey"/> of the item to delete.</param>
    /// <param name="cancellationToken">Used to cancel the asynchronous operation.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <remarks>
    /// The item may be logically or physically deleted.
    /// Implementations need only ensure that the item is not shown when trying to get it from the backing store after deletion.
    /// </remarks>
    Task DeleteAsync(TKey key, CancellationToken cancellationToken);
}