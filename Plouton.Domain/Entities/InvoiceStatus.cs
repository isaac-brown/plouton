// <copyright file="InvoiceStatus.cs" company="Isaac Brown">
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Plouton.Domain.Entities
{
    /// <summary>
    /// Represents the current status of an <see cref="Invoice"/>.
    /// </summary>
    public enum InvoiceStatus
    {
        Draft,
        Deleted,
        Submitted,
        Paid,
        Voided,
    }
}