// <copyright file="GetLineItemResponseDto.cs" company="Isaac Brown">
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Plouton.Web.Api.Models;

public class GetLineItemResponseDto
{
    public string Description { get; set; }
    public decimal AmountNet { get; set; }
    public decimal AmountTax { get; set; }
    public int Quantity { get; set; }
    public List<GetLineAnnotationResponseDto> Annotations { get; set; }
}