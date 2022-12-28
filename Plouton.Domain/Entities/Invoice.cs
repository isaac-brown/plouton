// <copyright file="Invoice.cs" company="Isaac Brown">
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using NodaTime;

namespace Plouton.Domain.Entities;

public record Invoice(Guid Id,
                      string InvoiceNumber,
                      Instant WhenCreated,
                      string WhoCreated,
                      InvoiceStatus Status,
                      Instant WhenDue,
                      Instant WhenIssued,
                      string? Reference,
                      Instant WhenModified,
                      string WhoModified,
                      IReadOnlyList<LineItem> LineItems
                      );