// <copyright file="GetInvoiceResponseDto.cs" company="Isaac Brown">
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Plouton.Web.Api.Models;

public class GetInvoiceResponseDto
{
    public Guid Id { get; set; }
    public DateTime WhenCreated { get; set; }
    public string InvoiceNumber { get; set; }
    public IReadOnlyList<GetLineItemResponseDto> LineItems { get; set; }
    public string? Reference { get; set; }
    public string Status { get; set; }
    public string WhoCreated { get; set; }
    public DateTime WhenDue { get; set; }
    public DateTime WhenIssued { get; set; }
    public DateTime WhenModified { get; set; }
    public string WhoModified { get; set; }
}