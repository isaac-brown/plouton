// <copyright file="InvoiceExtensions.cs" company="Isaac Brown">
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using Plouton.Domain.Entities;
using Plouton.Persistence.CosmosDb.Records;

namespace Plouton.Persistence.CosmosDb.Extensions;

public static class InvoiceExtensions
{
    public static InvoiceRecord ToInvoiceRecord(this Invoice invoice)
    {
        return new InvoiceRecord
        {
            Id = invoice.Id,
            InvoiceNumber = invoice.InvoiceNumber,
            LineItems = invoice.LineItems.Select(lineItem => lineItem.ToLineItemRecord()).ToList(),
            Reference = invoice.Reference,
            Status = invoice.Status.ToString(),
            WhenCreated = invoice.WhenCreated.ToUnixTimeMilliseconds(),
            WhoCreated = invoice.WhoCreated,
            WhenDue = invoice.WhenDue.ToUnixTimeMilliseconds(),
            WhenIssued = invoice.WhenIssued.ToUnixTimeMilliseconds(),
            WhenModified = invoice.WhenModified.ToUnixTimeMilliseconds(),
            WhoModified = invoice.WhoModified,
        };
    }
}