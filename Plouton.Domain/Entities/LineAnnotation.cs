// <copyright file="LineAnnotation.cs" company="Isaac Brown">
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Plouton.Domain.Entities;

/// <summary>
/// Represents an annotation to a <see cref="LineItem"/>. This can be used as a note.
/// </summary>
/// <param name="Description">The text of the annotation.</param>
public record LineAnnotation(string Description);