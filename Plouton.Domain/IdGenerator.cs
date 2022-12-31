// <copyright file="IdGenerator.cs" company="Isaac Brown">
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Plouton.Domain;

/// <summary>
/// Represents a source of sequential identifiers.
/// </summary>
public abstract class IdGenerator
{
    /// <summary>
    /// Asynchronously get the next sequential identifier.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the next sequential identifier.</returns>
    public abstract Task<int> NextIdAsync();
}