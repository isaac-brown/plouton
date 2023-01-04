// <copyright file="InvoicesController.cs" company="Isaac Brown">
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Plouton.Domain.Entities;
using Plouton.Persistence.Abstractions;
using Plouton.Web.Api.Extensions;
using Plouton.Web.Api.Models;

namespace Plouton.Web.Api.Controllers;

/// <summary>
/// Provides methods to interact with invoices.
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Route("/api/v1/invoices")]
[Consumes("application/json")]
[Produces("application/json")]
public class InvoicesController : ControllerBase
{
    private readonly InvoiceRepository invoiceRepository;
    private readonly ILogger<InvoicesController> logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="InvoicesController"/> class.
    /// </summary>
    /// <param name="invoiceRepository">The repository used to store invoices.</param>
    /// <param name="logger">Used to write messages to.</param>
    public InvoicesController(
        InvoiceRepository invoiceRepository,
        ILogger<InvoicesController> logger)
    {
        this.invoiceRepository = invoiceRepository;
        this.logger = logger;
    }

    /// <summary>
    /// Gets a page of invoices. Callers must have the claim `invoice:read`.
    /// </summary>
    /// <param name="limit">The maximum number of invoices to be returned on a page. Default is 1000.</param>
    /// <param name="token">The token to continue a previous query.</param>
    /// <param name="ct">Used to cancel the asynchronous operation.</param>
    /// <returns>
    /// A <see cref="Task"/> representing the asynchronous operation.
    /// The result will be an <see cref="ActionResult"/> which describes the result of the HTTP operation.
    /// </returns>
    [HttpGet]
    [Authorize(Policy = "IsInvoiceReader")]
    [ProducesResponseType(typeof(PagedResponseDto<GetInvoiceResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> Get([FromQuery] int? limit, [FromQuery] string? token, CancellationToken ct)
    {
        PagedCollection<Invoice> pagedInvoices = await this.invoiceRepository.ReadManyAsync(limit.GetValueOrDefault(1000), token, ct);

        var response = pagedInvoices.ToPagedResponseDto(invoice => invoice.ToGetInvoiceResponseDto());

        return this.Ok(response);
    }

    /// <summary>
    /// Gets a single invoice by id. Callers must have the claim `invoice:read`.
    /// </summary>
    /// <param name="id">The id of the invoice to retrieve.</param>
    /// <param name="ct">Used to cancel the asynchronous operation.</param>
    /// <returns>
    /// A <see cref="Task"/> representing the asynchronous operation.
    /// The result will be an <see cref="ActionResult"/> which describes the result of the HTTP operation.
    /// </returns>
    [HttpGet("{id:guid}")]
    [Authorize(Policy = "IsInvoiceReader")]
    [ProducesResponseType(typeof(GetInvoiceResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
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

    /// <summary>
    /// Creates a new invoice. Callers must have the claim `invoice:write`.
    /// </summary>
    /// <param name="request">The new invoice.</param>
    /// <param name="ct">Used to cancel the asynchronous operation.</param>
    /// <returns>
    /// A <see cref="Task"/> representing the asynchronous operation.
    /// The result will be an <see cref="ActionResult"/> which describes the result of the HTTP operation.
    /// </returns>
    [HttpPost]
    [Authorize(Policy = "IsInvoiceWriter")]
    [ProducesResponseType(typeof(GetInvoiceResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> Post(
        CreateInvoiceRequestDto request,
        CancellationToken ct)
    {
        var invoice = request.ToInvoice(user: this.User.Identity!);

        invoice = await this.invoiceRepository.CreateAsync(invoice.Id, invoice, ct);

        return this.CreatedAtAction(nameof(this.Get), new { id = invoice.Id }, invoice.ToGetInvoiceResponseDto());
    }

    /// <summary>
    /// Updates an existing invoice. Callers must have the claim `invoice:write`.
    /// </summary>
    /// <param name="id">The new invoice.</param>
    /// <param name="request">The modified invoice.</param>
    /// <param name="ct">Used to cancel the asynchronous operation.</param>
    /// <returns>
    /// A <see cref="Task"/> representing the asynchronous operation.
    /// The result will be an <see cref="ActionResult"/> which describes the result of the HTTP operation.
    /// </returns>
    [HttpPut("{id:guid}")]
    [Authorize(Policy = "IsInvoiceWriter")]
    [ProducesResponseType(typeof(GetInvoiceResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Put([FromRoute] Guid id, UpdateInvoiceRequestDto request, CancellationToken ct)
    {
        Invoice? invoice = await this.invoiceRepository.ReadAsync(id, ct);
        if (invoice is null)
        {
            return this.NotFound();
        }

        invoice = request.ToInvoice(invoice, this.User.Identity!);
        invoice = await this.invoiceRepository.UpdateAsync(invoice, ct);
        return this.Ok(invoice.ToGetInvoiceResponseDto());
    }

    /// <summary>
    /// Deletes an existing invoice. Callers must have the claim `invoice:delete`.
    /// </summary>
    /// <param name="id">The new invoice.</param>
    /// <param name="ct">Used to cancel the asynchronous operation.</param>
    /// <returns>
    /// A <see cref="Task"/> representing the asynchronous operation.
    /// The result will be an <see cref="ActionResult"/> which describes the result of the HTTP operation.
    /// </returns>
    [HttpDelete("{id:guid}")]
    [Authorize(Policy = "IsInvoiceDeleter")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
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