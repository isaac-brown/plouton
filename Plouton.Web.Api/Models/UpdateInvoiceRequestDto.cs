// <copyright file="UpdateInvoiceRequestDto.cs" company="Isaac Brown">
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Security.Claims;
using System.Security.Principal;
using NodaTime;
using NodaTime.Text;
using Plouton.Domain.Entities;

namespace Plouton.Web.Api.Models;

/// <summary>
/// Represents the JSON document which is used to update <see cref="Invoice"/> instances.
/// </summary>
public class UpdateInvoiceRequestDto
{
    // Disable some warnings as this class is a POCO used to map data to/from HTTP request/response bodies.
#pragma warning disable SA1600 //Elements should be documented
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CS8618 // Non-nullable property must contain a non-null value when exiting constructor.

    public string Status { get; set; }

    public string WhenDue { get; set; }

    public string WhenIssued { get; set; }

    public string? Reference { get; set; }

    public IEnumerable<CreateOrUpdateLineItemRequestDto> LineItems { get; set; }

#pragma warning restore SA1600 //Elements should be documented
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
#pragma warning restore CS8618 // Non-nullable property must contain a non-null value when exiting constructor.

    /// <summary>
    /// Maps this instance to a new instance of <see cref="Invoice"/>.
    /// </summary>
    /// <param name="existingInvoice">An existing invoice instance to map updated values into.</param>
    /// <param name="user">The principal which holds the current user's information.</param>
    /// <returns>A new instance of <see cref="Invoice"/>.</returns>
    public Invoice ToInvoice(Invoice existingInvoice, IIdentity user)
    {
        var localDateTimePattern = LocalDatePattern.Iso;

        return existingInvoice with
        {
            WhenDue = localDateTimePattern.Parse(this.WhenDue).GetValueOrThrow(),
            WhenIssued = localDateTimePattern.Parse(this.WhenIssued).GetValueOrThrow(),
            Status = Enum.Parse<InvoiceStatus>(this.Status),
            LineItems = this.LineItems.Select(lineItem => lineItem.ToLineItem()).ToList(),
            Reference = this.Reference,
            WhenModified = Instant.FromDateTimeUtc(DateTime.UtcNow),
            WhoModified = user.Name ?? "<username not found>",
        };
    }
}