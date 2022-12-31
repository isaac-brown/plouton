// <copyright file="LineItemExtensions.cs" company="Isaac Brown">
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using Plouton.Domain.Entities;
using Plouton.Web.Api.Models;

namespace Plouton.Web.Api.Extensions;

/// <summary>
/// Provides extension methods for <see cref="LineItem"/> instances.
/// These methods are specific to the Web API implementation of Plouton.
/// </summary>
public static class LineItemExtensions
{
    /// <summary>
    /// Maps the given <paramref name="lineItem"/> to a new instance of <see cref="GetLineItemResponseDto"/>.
    /// </summary>
    /// <param name="lineItem">The lineItem which properties will be mapped from.</param>
    /// <returns>A new instance of <see cref="GetLineItemResponseDto"/>.</returns>
    public static GetLineItemResponseDto ToGetLineItemResponseDto(this LineItem lineItem)
    {
        return new GetLineItemResponseDto
        {
            Description = lineItem.Description,
            AmountNet = lineItem.AmountNet,
            AmountTax = lineItem.AmountNet,
            Quantity = lineItem.Quantity,
            Annotations = lineItem.Annotations.Select(annotation => annotation.ToGetLineAnnotationResponseDto()).ToList(),
        };
    }
}