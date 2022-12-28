// <copyright file="LineAnnotationExtensions.cs" company="Isaac Brown">
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using Plouton.Domain.Entities;
using Plouton.Persistence.CosmosDb.Records;

namespace Plouton.Persistence.CosmosDb.Extensions;

public static class LineAnnotationExtensions
{
    public static LineAnnotationRecord ToLineAnnotationRecord(this LineAnnotation annotation)
    {
        return new LineAnnotationRecord
        {
            Description = annotation.Description,
        };
    }
}