// <copyright file="InvoiceExtensions.cs" company="Isaac Brown">
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using Plouton.Domain.Entities;
using Plouton.Web.Api.Models;

namespace Plouton.Web.Api.Extensions;

public static class InvoiceExtensions
{
    public static GetInvoiceResponseDto ToGetInvoiceResponseDto(this Invoice invoice)
    {
        return new GetInvoiceResponseDto
        {
            Id = invoice.Id,
            InvoiceNumber = invoice.InvoiceNumber,
            LineItems = invoice.LineItems.Select(lineItem => lineItem.ToGetLineItemResponseDto()).ToList(),
            Reference = invoice.Reference,
            Status = invoice.Status.ToString(),
            WhenCreated = invoice.WhenCreated.ToDateTimeUtc().ToLocalTime(),
            WhoCreated = invoice.WhoCreated,
            WhenDue = invoice.WhenDue.ToDateTimeUtc().ToLocalTime(),
            WhenIssued = invoice.WhenIssued.ToDateTimeUtc().ToLocalTime(),
            WhenModified = invoice.WhenModified.ToDateTimeUtc().ToLocalTime(),
            WhoModified = invoice.WhoModified,
        };
    }
}