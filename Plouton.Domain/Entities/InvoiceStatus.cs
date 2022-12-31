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
        /// <summary>An invoice which has been.</summary>
        Draft,

        /// <summary>An invoice which has been marked as deleted.</summary>
        Deleted,

        /// <summary>An invoice which has been submitted for payment.</summary>
        Submitted,

        /// <summary>An invoice which has been paid.</summary>
        Paid,

        /// <summary>An invoice which is no longer required.</summary>
        Voided,
    }
}