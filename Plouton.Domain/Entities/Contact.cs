// <copyright file="Contact.cs" company="Isaac Brown">
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Plouton.Domain.Entities;

/// <summary>
/// Represents a contact.
/// </summary>
/// <param name="Name">The name of the contact.</param>
/// <param name="ExternalReference">The reference of this contact in an external system.</param>
/// <param name="Address">The address details of the contact.</param>
public record Contact(
    string Name,
    string? ExternalReference,
    Address Address);