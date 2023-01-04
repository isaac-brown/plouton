// <copyright file="InvoiceExtensions.cs" company="Isaac Brown">
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using NodaTime.Text;
using Plouton.Domain.Entities;
using Plouton.Persistence.CosmosDb.Records;

namespace Plouton.Persistence.CosmosDb.Extensions;

/// <summary>
/// Provides extension methods for <see cref="Invoice"/> instances.
/// These methods are specific to the Cosmos DB implementation of Plouton.
/// </summary>
public static class InvoiceExtensions
{
    /// <summary>
    /// Maps the given <paramref name="invoice"/> to a new instance of <see cref="InvoiceRecord"/>.
    /// </summary>
    /// <param name="invoice">The <see cref="Invoice"/> instance which properties will be mapped from.</param>
    /// <returns>A new instance of <see cref="InvoiceRecord"/>.</returns>
    public static InvoiceRecord ToInvoiceRecord(this Invoice invoice)
    {
        LocalDatePattern localDatePattern = LocalDatePattern.Iso;
        return new InvoiceRecord
        {
            Id = invoice.Id,
            InvoiceNumber = invoice.InvoiceNumber,
            LineItems = invoice.LineItems.Select(lineItem => lineItem.ToLineItemRecord()).ToList(),
            Reference = invoice.Reference,
            Status = invoice.Status.ToString(),
            WhenCreated = invoice.WhenCreated.ToUnixTimeMilliseconds(),
            WhoCreated = invoice.WhoCreated,
            WhenDue = localDatePattern.Format(invoice.WhenDue),
            WhenIssued = localDatePattern.Format(invoice.WhenIssued),
            WhenModified = invoice.WhenModified.ToUnixTimeMilliseconds(),
            WhoModified = invoice.WhoModified,
            Contact = invoice.Contact.ToContactRecord(),
        };
    }
}