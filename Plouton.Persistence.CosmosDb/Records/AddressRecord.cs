// <copyright file="AddressRecord.cs" company="Isaac Brown">
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using Newtonsoft.Json;
using Plouton.Domain.Entities;

namespace Plouton.Persistence.CosmosDb.Records;

/// <summary>
/// Represents the Cosmos DB document which is used to store <see cref="Address"/> instances.
/// </summary>
public class AddressRecord
{
    // Disable some warnings as this class is a POCO used to map data to/from Cosmos DB.
#pragma warning disable SA1600 //Elements should be documented
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CS8618 // Non-nullable property must contain a non-null value when exiting constructor.

    [JsonProperty(propertyName: "addressLine1")]
    public string AddressLine1 { get; set; }

    [JsonProperty(propertyName: "addressLine2")]
    public string? AddressLine2 { get; set; }

    [JsonProperty(propertyName: "addressLine3")]
    public string? AddressLine3 { get; set; }

    [JsonProperty(propertyName: "addressLine4")]
    public string? AddressLine4 { get; set; }

#pragma warning restore SA1600 //Elements should be documented
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
#pragma warning restore CS8618 // Non-nullable property must contain a non-null value when exiting constructor.

    /// <summary>
    /// Maps this instance to a new instance of <see cref="Address"/>.
    /// </summary>
    /// <returns>A new instance of <see cref="Address"/>.</returns>
    internal Address ToAddress()
    {
        return new Address(
            AddressLine1: this.AddressLine1,
            AddressLine2: this.AddressLine2,
            AddressLine3: this.AddressLine3,
            AddressLine4: this.AddressLine4);
    }
}