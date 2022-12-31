// <copyright file="GetLineItemResponseDto.cs" company="Isaac Brown">
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using Plouton.Domain.Entities;

namespace Plouton.Web.Api.Models;

/// <summary>
/// Represents the JSON document which is used to get/read <see cref="LineItem"/> instances.
/// </summary>
public class GetLineItemResponseDto
{
    // Disable some warnings as this class is a POCO used to map data to/from HTTP request/response bodies.
#pragma warning disable SA1600 //Elements should be documented
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CS8618 // Non-nullable property must contain a non-null value when exiting constructor.

    public string Description { get; set; }

    public decimal AmountNet { get; set; }

    public decimal AmountTax { get; set; }

    public int Quantity { get; set; }

    public List<GetLineAnnotationResponseDto> Annotations { get; set; }

#pragma warning restore SA1600 //Elements should be documented
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
#pragma warning restore CS8618 // Non-nullable property must contain a non-null value when exiting constructor.
}