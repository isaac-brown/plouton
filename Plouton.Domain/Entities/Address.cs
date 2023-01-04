// <copyright file="Address.cs" company="Isaac Brown">
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Plouton.Domain.Entities;

/// <summary>
/// Represents an address.
/// </summary>
/// <param name="AddressLine1">The first line of the address.</param>
/// <param name="AddressLine2">The second line of the address.</param>
/// <param name="AddressLine3">The third line of the address.</param>
/// <param name="AddressLine4">The fourth line of the address.</param>
public record Address(
    string AddressLine1,
    string? AddressLine2,
    string? AddressLine3,
    string? AddressLine4);