// <copyright file="InvoiceRecord.cs" company="Isaac Brown">
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using Newtonsoft.Json;
using NodaTime;
using NodaTime.Text;
using Plouton.Domain.Entities;

namespace Plouton.Persistence.CosmosDb.Records;

/// <summary>
/// Represents the Cosmos DB document which is used to store <see cref="Invoice"/> instances.
/// </summary>
public class InvoiceRecord
{
    // Disable some warnings as this class is a POCO used to map data to/from Cosmos DB.
#pragma warning disable SA1600 //Elements should be documented
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CS8618 // Non-nullable property must contain a non-null value when exiting constructor.

    [JsonProperty(propertyName: "id")]
    public Guid Id { get; set; }

    [JsonProperty(propertyName: "whenCreated")]
    public long WhenCreated { get; set; }

    [JsonProperty(propertyName: "invoiceNumber")]
    public string InvoiceNumber { get; set; }

    [JsonProperty(propertyName: "whoCreated")]
    public string WhoCreated { get; set; }

    [JsonProperty(propertyName: "status")]
    public string Status { get; set; }

    [JsonProperty(propertyName: "whenDue")]
    public string WhenDue { get; set; }

    [JsonProperty(propertyName: "whenIssued")]
    public string WhenIssued { get; set; }

    [JsonProperty(propertyName: " reference")]
    public string? Reference { get; set; }

    [JsonProperty(propertyName: "whenModified")]
    public long WhenModified { get; set; }

    [JsonProperty(propertyName: "whoModified")]
    public string WhoModified { get; set; }

    [JsonProperty(propertyName: "lineItems")]
    public IReadOnlyList<LineItemRecord> LineItems { get; set; }

    [JsonProperty(propertyName: "contact")]
    public ContactRecord Contact { get; set; }

#pragma warning restore SA1600 //Elements should be documented
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
#pragma warning restore CS8618 // Non-nullable property must contain a non-null value when exiting constructor.

    /// <summary>
    /// Maps this instance to a new instance of <see cref="Invoice"/>.
    /// </summary>
    /// <returns>A new instance of <see cref="Invoice"/>.</returns>
    public Invoice ToInvoice()
    {
        var localDatePattern = LocalDatePattern.Iso;
        return new Invoice(
            Id: this.Id,
            InvoiceNumber: this.InvoiceNumber,
            WhenCreated: Instant.FromUnixTimeMilliseconds(this.WhenCreated),
            WhoCreated: this.WhoCreated,
            Status: Enum.Parse<InvoiceStatus>(this.Status),
            WhenDue: localDatePattern.Parse(this.WhenDue).GetValueOrThrow(),
            WhenIssued: localDatePattern.Parse(this.WhenIssued).GetValueOrThrow(),
            Reference: this.Reference,
            WhenModified: Instant.FromUnixTimeMilliseconds(this.WhenModified),
            WhoModified: this.WhoModified,
            LineItems: this.LineItems.Select(lineItem => lineItem.ToLineItem()).ToList(),
            Contact: this.Contact.ToContact());
    }
}