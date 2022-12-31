// <copyright file="LineAnnotationExtensions.cs" company="Isaac Brown">
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using Plouton.Domain.Entities;
using Plouton.Persistence.CosmosDb.Records;

namespace Plouton.Persistence.CosmosDb.Extensions;

/// <summary>
/// Provides extension methods for <see cref="LineAnnotation"/> instances.
/// These methods are specific to the Cosmos DB implementation of Plouton.
/// </summary>
public static class LineAnnotationExtensions
{
    /// <summary>
    /// Maps the given <paramref name="annotation"/> to a new instance of <see cref="LineAnnotationRecord"/>.
    /// </summary>
    /// <param name="annotation">The line annotation which properties will be mapped from.</param>
    /// <returns>A new instance of <see cref="LineAnnotationRecord"/>.</returns>
    public static LineAnnotationRecord ToLineAnnotationRecord(this LineAnnotation annotation)
    {
        return new LineAnnotationRecord
        {
            Description = annotation.Description,
        };
    }
}