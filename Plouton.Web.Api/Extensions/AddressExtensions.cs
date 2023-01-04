// <copyright file="AddressExtensions.cs" company="Isaac Brown">
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using Plouton.Domain.Entities;
using Plouton.Web.Api.Models;

namespace Plouton.Web.Api.Extensions;

/// <summary>
/// Provides extension methods for <see cref="Address"/> instances.
/// These methods are specific to the Web API implementation of Plouton.
/// </summary>
public static class AddressExtensions
{
    /// <summary>
    /// Maps the given <paramref name="address"/> to a new instance of <see cref="GetAddressResponseDto"/>.
    /// </summary>
    /// <param name="address">The <see cref="Address"/> instance which properties will be mapped from.</param>
    /// <returns>A new instance of <see cref="GetAddressResponseDto"/>.</returns>
    public static GetAddressResponseDto ToGetAddressResponseDto(this Address address)
    {
        return new GetAddressResponseDto
        {
            AddressLine1 = address.AddressLine1,
            AddressLine2 = address.AddressLine2,
            AddressLine3 = address.AddressLine3,
            AddressLine4 = address.AddressLine4,
        };
    }
}
