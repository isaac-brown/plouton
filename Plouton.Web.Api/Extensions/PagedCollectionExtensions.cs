// <copyright file="PagedCollectionExtensions.cs" company="Isaac Brown">
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using Plouton.Domain.Entities;
using Plouton.Web.Api.Models;

namespace Plouton.Web.Api.Extensions;

/// <summary>
/// Provides extension methods for <see cref="LineItem"/> instances.
/// These methods are specific to the Web API implementation of Plouton.
/// </summary>
public static class PagedCollectionExtensions
{
    /// <summary>
    /// Maps the given <paramref name="pagedCollection"/> to a new instance of <see cref="PagedResponseDto{TOutput}"/>.
    /// </summary>
    /// <param name="pagedCollection">The paged collection to be mapped.</param>
    /// <param name="mappingFunc">A function which maps items from <typeparamref name="TInput"/> to <typeparamref name="TOutput"/>.</param>
    /// <typeparam name="TInput">The type of objects contained in the input collection.</typeparam>
    /// <typeparam name="TOutput">The type of objects contained in the output collection.</typeparam>
    /// <returns>A new instance of <see cref="PagedResponseDto{TOutput}"/>.</returns>
    public static PagedResponseDto<TOutput> ToPagedResponseDto<TInput, TOutput>(
        this PagedCollection<TInput> pagedCollection,
        Func<TInput, TOutput> mappingFunc)
    {
        return new PagedResponseDto<TOutput>
        {
            Items = pagedCollection.Items.Select(mappingFunc).ToList(),
            Limit = pagedCollection.Limit,
            Token = pagedCollection.Token,
        };
    }
}