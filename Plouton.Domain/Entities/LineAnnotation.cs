// <copyright file="LineAnnotation.cs" company="Isaac Brown">
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Plouton.Domain.Entities;

#pragma warning disable SA1313 // Parameter should begin with lower-case letter. StyleCop doesn't seem to properly support record types.

/// <summary>
/// Represents an annotation to a <see cref="LineItem"/>. This can be used as a note.
/// </summary>
/// <param name="Description">The text of the annotation.</param>
public record LineAnnotation(string Description);