// <copyright file="LineAnnotationExtensions.cs" company="Isaac Brown">
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using Plouton.Domain.Entities;
using Plouton.Web.Api.Models;

namespace Plouton.Web.Api.Extensions;

/// <summary>
/// Provides extension methods for <see cref="LineAnnotation"/> instances.
/// These methods are specific to the Web API implementation of Plouton.
/// </summary>
public static class LineAnnotationExtensions
{
    /// <summary>
    /// Maps the given <paramref name="annotation"/> to a new instance of <see cref="GetLineAnnotationResponseDto"/>.
    /// </summary>
    /// <param name="annotation">The annotation which properties will be mapped from.</param>
    /// <returns>A new instance of <see cref="GetLineAnnotationResponseDto"/>.</returns>
    public static GetLineAnnotationResponseDto ToGetLineAnnotationResponseDto(this LineAnnotation annotation)
    {
        return new GetLineAnnotationResponseDto
        {
            Description = annotation.Description,
        };
    }
}