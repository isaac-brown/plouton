// <copyright file="UpdateInvoiceRequestDto.cs" company="Isaac Brown">
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Security.Claims;
using NodaTime;
using NodaTime.Text;
using Plouton.Domain.Entities;

namespace Plouton.Web.Api.Models;

public class UpdateInvoiceRequestDto
{
    public string Status { get; set; }
    public string WhenDue { get; set; }
    public string WhenIssued { get; set; }
    public string? Reference { get; set; }
    public IEnumerable<CreateOrUpdateLineItemRequestDto> LineItems { get; set; }

    public Invoice ToInvoice(Invoice existingInvoice, ClaimsPrincipal user)
    {
        var instantPattern = InstantPattern.CreateWithCurrentCulture("yyyy-MM-dd");
        return existingInvoice with {
            WhenDue = instantPattern.Parse(this.WhenDue).GetValueOrThrow(),
            WhenIssued = instantPattern.Parse(this.WhenIssued).GetValueOrThrow(),
            Status = Enum.Parse<InvoiceStatus>(this.Status),
            LineItems = this.LineItems.Select(lineItem => lineItem.ToLineItem()).ToList(),
            Reference = this.Reference,
            WhenModified = Instant.FromDateTimeUtc(DateTime.UtcNow),
            WhoModified = user.Identity.Name,
        };
    }
}