// <copyright file="LineAnnotationExtensions.cs" company="Isaac Brown">
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using Plouton.Domain.Entities;
using Plouton.Persistence.CosmosDb.Records;
using Plouton.Web.Api.Models;

namespace Plouton.Web.Api.Extensions;

public static class LineAnnotationExtensions
{
    public static GetLineAnnotationResponseDto ToGetLineAnnotationResponseDto(this LineAnnotation annotation)
    {
        return new GetLineAnnotationResponseDto
        {
            Description = annotation.Description,
        };
    }
}