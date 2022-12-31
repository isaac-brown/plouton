// <copyright file="InvoicesController.cs" company="Isaac Brown">
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Plouton.Domain;
using Plouton.Domain.Entities;
using Plouton.Persistence.Abstractions;
using Plouton.Web.Api.Extensions;
using Plouton.Web.Api.Models;

namespace Plouton.Web.Api.Controllers;

/// <summary>
/// Provides methods to interact with invoices.
/// </summary>
[ApiController]
[Route("/api/v1/invoices")]
public class InvoicesController : ControllerBase
{
    private readonly InvoiceRepository invoiceRepository;
    private readonly ILogger<InvoicesController> logger;

    public InvoicesController(
        InvoiceRepository invoiceRepository,
        ILogger<InvoicesController> logger)
    {
        this.invoiceRepository = invoiceRepository;
        this.logger = logger;
    }

    [HttpGet]
    [Authorize(Policy = "IsInvoiceReader")]
    public async Task<ActionResult> Get([FromQuery] int? limit, [FromQuery] string? token, CancellationToken ct)
    {
        int ulimit = limit.GetValueOrDefault(1000);
        PagedCollection<Invoice> pagedInvoices = await this.invoiceRepository.ReadManyAsync(ulimit, token, ct);

        var response = new PagedResponseDto<GetInvoiceResponseDto>
        {
            Items = pagedInvoices.Items.Select(invoice => invoice.ToGetInvoiceResponseDto()).ToList(),
            Limit = pagedInvoices.Limit,
            Token = pagedInvoices.Token,
        };

        return this.Ok(response);
    }

    [HttpGet("{id:guid}")]
    [Authorize(Policy = "IsInvoiceReader")]
    public async Task<ActionResult> Get([FromRoute] Guid id, CancellationToken ct)
    {
        Invoice? invoice = await this.invoiceRepository.ReadAsync(id, ct);
        if (invoice is null)
        {
            return this.NotFound();
        }

        var response = invoice.ToGetInvoiceResponseDto();

        return this.Ok(response);
    }

    [HttpPost]
    [Authorize(Policy = "IsInvoiceWriter")]
    public async Task<ActionResult> Post(CreateInvoiceRequestDto request,
                                         CancellationToken ct)
    {
        var invoice = request.ToInvoice(user: this.User);

        invoice = await this.invoiceRepository.CreateAsync(invoice.Id, invoice, ct);

        return this.CreatedAtAction(nameof(this.Get), new { id = invoice.Id }, invoice.ToGetInvoiceResponseDto());
    }

    [HttpPut("{id:guid}")]
    [Authorize(Policy = "IsInvoiceWriter")]
    public async Task<ActionResult> Put([FromRoute] Guid id, UpdateInvoiceRequestDto request, CancellationToken ct)
    {
        Invoice? invoice = await this.invoiceRepository.ReadAsync(id, ct);
        if (invoice is null)
        {
            return this.NotFound();
        }

        invoice = request.ToInvoice(invoice, this.User);
        invoice = await this.invoiceRepository.UpdateAsync(invoice, ct);
        return this.Ok(invoice.ToGetInvoiceResponseDto());
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Policy = "IsInvoiceDeleter")]
    public async Task<ActionResult> Delete([FromRoute] Guid id, CancellationToken ct)
    {
        Invoice? invoice = await this.invoiceRepository.ReadAsync(id, ct);
        if (invoice is null)
        {
            return this.NotFound();
        }

        await this.invoiceRepository.DeleteAsync(id, ct);

        return this.NoContent();
    }
}