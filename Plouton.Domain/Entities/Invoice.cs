// <copyright file="Invoice.cs" company="Isaac Brown">
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using NodaTime;

namespace Plouton.Domain.Entities;

/// <summary>
/// Represents an invoice for goods/services.
/// </summary>
/// <param name="Id">Uniquely identifies an invoice.</param>
/// <param name="InvoiceNumber">Identifies an invoice.</param>
/// <param name="WhenCreated">The <see cref="Instant"/> at which the invoice was created.</param>
/// <param name="WhoCreated">The user which created the invoice.</param>
/// <param name="Status">The status of the invoice.</param>
/// <param name="WhenDue">The date that the invoice is due for payment.</param>
/// <param name="WhenIssued">The date that the invoice was issued.</param>
/// <param name="Reference">
/// A context-specific reference.
/// This usually represents the reference with which a customer should make payment.
/// </param>
/// <param name="WhenModified">The <see cref="Instant"/> at which the invoice was last modified.</param>
/// <param name="WhoModified">The user which last modified the invoice.</param>
/// <param name="LineItems">The line items for this invoice.</param>
/// <param name="Contact">The contact for this invoice.</param>
public record Invoice(Guid Id,
                      string InvoiceNumber,
                      Instant WhenCreated,
                      string WhoCreated,
                      InvoiceStatus Status,
                      LocalDate WhenDue,
                      LocalDate WhenIssued,
                      string? Reference,
                      Instant WhenModified,
                      string WhoModified,
                      IReadOnlyList<LineItem> LineItems,
                      Contact Contact);