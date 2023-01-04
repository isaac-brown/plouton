// <copyright file="ContactRecord.cs" company="Isaac Brown">
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using Newtonsoft.Json;
using Plouton.Domain.Entities;

namespace Plouton.Persistence.CosmosDb.Records;

/// <summary>
/// Represents the Cosmos DB document which is used to store <see cref="Contact"/> instances.
/// </summary>
public class ContactRecord
{
    // Disable some warnings as this class is a POCO used to map data to/from Cosmos DB.
#pragma warning disable SA1600 //Elements should be documented
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CS8618 // Non-nullable property must contain a non-null value when exiting constructor.

    [JsonProperty(propertyName: "name")]
    public string Name { get; set; }

    [JsonProperty(propertyName: "externalReference")]
    public string? ExternalReference { get; set; }

    [JsonProperty(propertyName: "address")]
    public AddressRecord Address { get; init; }

#pragma warning restore SA1600 //Elements should be documented
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
#pragma warning restore CS8618 // Non-nullable property must contain a non-null value when exiting constructor.

    /// <summary>
    /// Maps this instance to a new instance of <see cref="Contact"/>.
    /// </summary>
    /// <returns>A new instance of <see cref="Contact"/>.</returns>
    internal Contact ToContact()
    {
        return new Contact(
            this.Name,
            this.ExternalReference,
            this.Address.ToAddress());
    }
}