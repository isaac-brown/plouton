// <copyright file="LineItemRecord.cs" company="Isaac Brown">
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using Newtonsoft.Json;
using Plouton.Domain.Entities;

namespace Plouton.Persistence.CosmosDb.Records;

/// <summary>
/// Represents the Cosmos DB document which is used to store <see cref="LineItem"/> instances.
/// </summary>
public class LineItemRecord
{
    // Disable some warnings as this class is a POCO used to map data to/from Cosmos DB.
#pragma warning disable SA1600 //Elements should be documented
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CS8618 // Non-nullable property must contain a non-null value when exiting constructor.

    [JsonProperty(propertyName: "description")]
    public string Description { get; set; }

    [JsonProperty(propertyName: "quantity")]
    public int Quantity { get; set; }

    [JsonProperty(propertyName: "amountNet")]
    public decimal AmountNet { get; set; }

    [JsonProperty(propertyName: "amountTax")]
    public decimal AmountTax { get; set; }

    [JsonProperty(propertyName: "annotations")]
    public IReadOnlyList<LineAnnotationRecord> Annotations { get; set; }

#pragma warning restore SA1600 //Elements should be documented
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
#pragma warning restore CS8618 // Non-nullable property must contain a non-null value when exiting constructor.

    /// <summary>
    /// Maps this instance to a new instance of <see cref="LineItem"/>.
    /// </summary>
    /// <returns>A new instance of <see cref="LineItem"/>.</returns>
    internal LineItem ToLineItem()
    {
        return new LineItem(
            Description: this.Description,
            Quantity: this.Quantity,
            AmountNet: this.AmountNet,
            AmountTax: this.AmountTax,
            Annotations: this.Annotations.Select(annotation => annotation.ToLineAnnotation()).ToList());
    }
}