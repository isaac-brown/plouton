// <copyright file="LineItemExtensions.cs" company="Isaac Brown">
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using Plouton.Domain.Entities;
using Plouton.Web.Api.Models;

namespace Plouton.Web.Api.Extensions;

public static class LineItemExtensions
{
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