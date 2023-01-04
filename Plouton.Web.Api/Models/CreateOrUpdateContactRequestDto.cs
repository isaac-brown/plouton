// <copyright file="CreateOrUpdateContactRequestDto.cs" company="Isaac Brown">
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using Plouton.Domain.Entities;

namespace Plouton.Web.Api.Models;

/// <summary>
/// Represents the JSON document which is used to create or update <see cref="Contact"/> instances.
/// </summary>
public class CreateOrUpdateContactRequestDto
{
    // Disable some warnings as this class is a POCO used to map data to/from HTTP request/response bodies.
#pragma warning disable SA1600 //Elements should be documented
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CS8618 // Non-nullable property must contain a non-null value when exiting constructor.

    public string Name { get; set; }

    public string? ExternalReference { get; set; }

    public CreateOrUpdateAddressRequestDto Address { get; set; }

#pragma warning restore SA1600 //Elements should be documented
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
#pragma warning restore CS8618 // Non-nullable property must contain a non-null value when exiting constructor.

    /// <summary>
    /// Maps this instance to a new instance of <see cref="Contact"/>.
    /// </summary>
    /// <returns>A new instance of <see cref="Contact"/>.</returns>
    public Contact ToContact()
    {
        return new Contact(
            Name: this.Name,
            ExternalReference: this.ExternalReference,
            Address: this.Address.ToAddress());
    }
}