// <copyright file="GetInvoiceResponseDto.cs" company="Isaac Brown">
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using Plouton.Domain.Entities;

namespace Plouton.Web.Api.Models;

/// <summary>
/// Represents the JSON document which is used to get/read <see cref="Invoice"/> instances.
/// </summary>
public class GetInvoiceResponseDto
{
    // Disable some warnings as this class is a POCO used to map data to/from HTTP request/response bodies.
#pragma warning disable SA1600 //Elements should be documented
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CS8618 // Non-nullable property must contain a non-null value when exiting constructor.

    public Guid Id { get; set; }

    public DateTime WhenCreated { get; set; }

    public string InvoiceNumber { get; set; }

    public IReadOnlyList<GetLineItemResponseDto> LineItems { get; set; }

    public string? Reference { get; set; }

    public string Status { get; set; }

    public string WhoCreated { get; set; }

    public DateTime WhenDue { get; set; }

    public DateTime WhenIssued { get; set; }

    public DateTime WhenModified { get; set; }

    public string WhoModified { get; set; }

#pragma warning restore SA1600 //Elements should be documented
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
#pragma warning restore CS8618 // Non-nullable property must contain a non-null value when exiting constructor.
}