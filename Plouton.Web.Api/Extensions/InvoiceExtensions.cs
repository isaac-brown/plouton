// <copyright file="InvoiceExtensions.cs" company="Isaac Brown">
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using NodaTime.Text;
using Plouton.Domain.Entities;
using Plouton.Web.Api.Models;

namespace Plouton.Web.Api.Extensions;

/// <summary>
/// Provides extension methods for <see cref="Invoice"/> instances.
/// These methods are specific to the Web API implementation of Plouton.
/// </summary>
public static class InvoiceExtensions
{
    /// <summary>
    /// Maps the given <paramref name="invoice"/> to a new instance of <see cref="GetInvoiceResponseDto"/>.
    /// </summary>
    /// <param name="invoice">The invoice which properties will be mapped from.</param>
    /// <returns>A new instance of <see cref="GetInvoiceResponseDto"/>.</returns>
    public static GetInvoiceResponseDto ToGetInvoiceResponseDto(this Invoice invoice)
    {
        var localDateTimePattern = LocalDatePattern.Iso;

        return new GetInvoiceResponseDto
        {
            Id = invoice.Id,
            InvoiceNumber = invoice.InvoiceNumber,
            LineItems = invoice.LineItems.Select(lineItem => lineItem.ToGetLineItemResponseDto()).ToList(),
            Reference = invoice.Reference,
            Status = invoice.Status.ToString(),
            WhenCreated = invoice.WhenCreated.ToDateTimeUtc().ToLocalTime(),
            WhoCreated = invoice.WhoCreated,
            WhenDue = localDateTimePattern.Format(invoice.WhenDue),
            WhenIssued = localDateTimePattern.Format(invoice.WhenIssued),
            WhenModified = invoice.WhenModified.ToDateTimeUtc().ToLocalTime(),
            WhoModified = invoice.WhoModified,
            Contact = invoice.Contact.ToGetContactResponseDto(),
        };
    }
}