// <copyright file="CreateOrUpdateLineItemDto.cs" company="Isaac Brown">
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using Plouton.Domain.Entities;

namespace Plouton.Web.Api.Models
{
    public class CreateOrUpdateLineItemRequestDto
    {
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal AmountNet { get; set; }
        public decimal AmountTax { get; set; }
        public IReadOnlyList<CreateOrUpdateLineAnnotationRequestDto> Annotations { get; set; }

        internal LineItem ToLineItem()
        {
            return new LineItem(
                Description: this.Description,
                Quantity: this.Quantity,
                AmountNet: this.AmountNet,
                AmountTax: this.AmountTax,
                Annotations: this.Annotations.Select(annotation => annotation.ToLineAnnotation()).ToList());
        }
    }
}