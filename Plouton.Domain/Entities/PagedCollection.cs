// <copyright file="PagedCollection.cs" company="Isaac Brown">
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Plouton.Domain.Entities;

/// <summary>
/// Represents a paged collection of elements.
/// </summary>
/// <typeparam name="T">The type of items contained in the collection.</typeparam>
public class PagedCollection<T>
{
    /// <summary>
    /// Gets the items in the page.
    /// </summary>
    public IReadOnlyList<T> Items { get; init; } = new List<T>();

    /// <summary>
    /// Gets the token which can be used to fetch the next page.
    /// </summary>
    /// <remarks>A null value indicates that there are no more pages available.</remarks>
    public string? Token { get; init; }

    /// <summary>
    /// Gets the maximum number of items which could be contained in the page.
    /// i.e. this is the number which a client requested to be in a page.
    /// </summary>
    public int Limit { get; init; }
}