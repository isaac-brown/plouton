// <copyright file="ContactExtensions.cs" company="Isaac Brown">
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using Plouton.Domain.Entities;
using Plouton.Web.Api.Models;

namespace Plouton.Web.Api.Extensions;

/// <summary>
/// Provides extension methods for <see cref="Contact"/> instances.
/// These methods are specific to the Web API implementation of Plouton.
/// </summary>
public static class ContactExtensions
{
    /// <summary>
    /// Maps the given <paramref name="contact"/> to a new instance of <see cref="GetContactResponseDto"/>.
    /// </summary>
    /// <param name="contact">The <see cref="Contact"/> instance which properties will be mapped from.</param>
    /// <returns>A new instance of <see cref="GetContactResponseDto"/>.</returns>
    public static GetContactResponseDto ToGetContactResponseDto(this Contact contact)
    {
        return new GetContactResponseDto
        {
            Name = contact.Name,
            ExternalReference = contact.ExternalReference,
            Address = contact.Address.ToGetAddressResponseDto(),
        };
    }
}