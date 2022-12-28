// <copyright file="InvoiceRecord.cs" company="Isaac Brown">
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using Newtonsoft.Json;
using NodaTime;
using Plouton.Domain.Entities;

namespace Plouton.Persistence.CosmosDb.Records;

public class InvoiceRecord
{
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
    public long WhenDue { get; set; }

    [JsonProperty(propertyName: "whenIssued")]
    public long WhenIssued { get; set; }

    [JsonProperty(propertyName: " reference")]
    public string? Reference { get; set; }

    [JsonProperty(propertyName: "whenModified")]
    public long WhenModified { get; set; }

    [JsonProperty(propertyName: "whoModified")]
    public string WhoModified { get; set; }

    [JsonProperty(propertyName: "lneItems")]
    public IReadOnlyList<LineItemRecord> LineItems { get; set; }

    public Invoice ToInvoice()
    {
        return new Invoice(
            Id: this.Id,
            InvoiceNumber: this.InvoiceNumber,
            WhenCreated: Instant.FromUnixTimeMilliseconds(this.WhenCreated),
            WhoCreated: this.WhoCreated,
            Status: Enum.Parse<InvoiceStatus>(this.Status),
            WhenDue: Instant.FromUnixTimeMilliseconds(this.WhenDue),
            WhenIssued: Instant.FromUnixTimeMilliseconds(this.WhenIssued),
            Reference: this.Reference,
            WhenModified: Instant.FromUnixTimeMilliseconds(this.WhenModified),
            WhoModified: this.WhoModified,
            LineItems: this.LineItems.Select(lineItem => lineItem.ToLineItem()).ToList());
    }
}