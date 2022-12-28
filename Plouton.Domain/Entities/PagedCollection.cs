// <copyright file="PagedCollection.cs" company="Isaac Brown">
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Plouton.Domain.Entities;

public class PagedCollection<T>
{
    public IReadOnlyList<T> Items { get; init; }

    public string? Token { get; init; }

    public int Limit { get; init; }
}