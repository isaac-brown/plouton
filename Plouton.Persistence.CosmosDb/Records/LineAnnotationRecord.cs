// <copyright file="LineAnnotationRecord.cs" company="Isaac Brown">
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using Newtonsoft.Json;
using Plouton.Domain.Entities;

namespace Plouton.Persistence.CosmosDb.Records
{
    public class LineAnnotationRecord
    {

        [JsonProperty(propertyName: "description")]
        public string Description { get; set; }

        internal LineAnnotation ToLineAnnotation()
        {
            return new LineAnnotation(Description: this.Description);
        }
    }
}