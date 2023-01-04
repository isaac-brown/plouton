// <copyright file="ContactExtensions.cs" company="Isaac Brown">
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using Plouton.Domain.Entities;
using Plouton.Persistence.CosmosDb.Records;

namespace Plouton.Persistence.CosmosDb.Extensions;

/// <summary>
/// Provides extension methods for <see cref="Contact"/> instances.
/// These methods are specific to the Cosmos DB implementation of Plouton.
/// </summary>
internal static class ContactExtensions
{
    /// <summary>
    /// Maps the given <paramref name="contact"/> to a new instance of <see cref="ContactRecord"/>.
    /// </summary>
    /// <param name="contact">The <see cref="Contact"/> instance which properties will be mapped from.</param>
    /// <returns>A new instance of <see cref="ContactRecord"/>.</returns>
    public static ContactRecord ToContactRecord(this Contact contact)
    {
        return new ContactRecord
        {
            Name = contact.Name,
            ExternalReference = contact.ExternalReference,
            Address = contact.Address.ToAddressRecord(),
        };
    }
}