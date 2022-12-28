// <copyright file="CreateOrUpdateLineAnnotation.cs" company="Isaac Brown">
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using Plouton.Domain.Entities;

namespace Plouton.Web.Api.Models;

public class CreateOrUpdateLineAnnotation
{
    public string Description { get; set; }

    internal LineAnnotation ToLineAnnotation()
    {
        return new LineAnnotation(Description: this.Description);
    }
}