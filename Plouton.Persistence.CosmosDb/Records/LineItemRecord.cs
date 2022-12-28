// <copyright file="LineItemRecord.cs" company="Isaac Brown">
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using Newtonsoft.Json;
using Plouton.Domain.Entities;

namespace Plouton.Persistence.CosmosDb.Records;

public class LineItemRecord
{

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