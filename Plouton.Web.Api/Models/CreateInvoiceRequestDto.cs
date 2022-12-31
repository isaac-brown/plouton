// <copyright file="CreateInvoiceRequestDto.cs" company="Isaac Brown">
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using NodaTime;
using NodaTime.Text;
using Plouton.Domain.Entities;
using System.Security.Claims;

namespace Plouton.Web.Api.Models;

public class CreateInvoiceRequestDto
{
    public string Status { get; set; }
    public string WhenDue { get; set; }
    public string WhenIssued { get; set; }
    public string? Reference { get; set; }
    public IEnumerable<CreateOrUpdateLineItemRequestDto> LineItems { get; set; }

    public Invoice ToInvoice(ClaimsPrincipal user)
    {
        var instantPattern = InstantPattern.CreateWithCurrentCulture("yyyy-MM-dd");
        return new Invoice(
            Id: Guid.NewGuid(),
            InvoiceNumber: "TODO",
            WhenCreated: Instant.FromDateTimeUtc(DateTime.UtcNow),
            WhoCreated: user.Identity.Name,
            Status: Enum.Parse<InvoiceStatus>(this.Status),
            WhenDue: instantPattern.Parse(this.WhenDue).GetValueOrThrow(),
            WhenIssued: instantPattern.Parse(this.WhenIssued).GetValueOrThrow(),
            Reference: this.Reference,
            WhenModified: Instant.FromDateTimeUtc(DateTime.UtcNow),
            WhoModified: user.Identity.Name,
            LineItems: this.LineItems.Select(lineItem => lineItem.ToLineItem()).ToList());
    }
}