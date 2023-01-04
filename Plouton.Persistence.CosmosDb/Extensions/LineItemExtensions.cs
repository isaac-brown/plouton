// <copyright file="LineItemExtensions.cs" company="Isaac Brown">
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using Plouton.Domain.Entities;
using Plouton.Persistence.CosmosDb.Records;

namespace Plouton.Persistence.CosmosDb.Extensions;

/// <summary>
/// Provides extension methods for <see cref="LineAnnotation"/> instances.
/// These methods are specific to the Cosmos DB implementation of Plouton.
/// </summary>
public static class LineItemExtensions
{
    /// <summary>
    /// Maps the given <paramref name="lineItem"/> to a new instance of <see cref="LineItemRecord"/>.
    /// </summary>
    /// <param name="lineItem">The <see cref="LineItem"/> instance which properties will be mapped from.</param>
    /// <returns>A new instance of <see cref="LineItemRecord"/>.</returns>
    public static LineItemRecord ToLineItemRecord(this LineItem lineItem)
    {
        return new LineItemRecord
        {
            Description = lineItem.Description,
            AmountNet = lineItem.AmountNet,
            AmountTax = lineItem.AmountNet,
            Quantity = lineItem.Quantity,
            Annotations = lineItem.Annotations.Select(annotation => new LineAnnotationRecord { Description = annotation.Description }).ToList(),
        };
    }
}