// <copyright file="AddressExtensions.cs" company="Isaac Brown">
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using Plouton.Domain.Entities;
using Plouton.Persistence.CosmosDb.Records;

namespace Plouton.Persistence.CosmosDb.Extensions;

/// <summary>
/// Provides extension methods for <see cref="Address"/> instances.
/// These methods are specific to the Cosmos DB implementation of Plouton.
/// </summary>
internal static class AddressExtensions
{
    /// <summary>
    /// Maps the given <paramref name="address"/> to a new instance of <see cref="AddressRecord"/>.
    /// </summary>
    /// <param name="address">The <see cref="Contact"/> instance which properties will be mapped from.</param>
    /// <returns>A new instance of <see cref="AddressRecord"/>.</returns>
    public static AddressRecord ToAddressRecord(this Address address)
    {
        return new AddressRecord
        {
            AddressLine1 = address.AddressLine1,
            AddressLine2 = address.AddressLine2,
            AddressLine3 = address.AddressLine3,
            AddressLine4 = address.AddressLine4,
        };
    }
}