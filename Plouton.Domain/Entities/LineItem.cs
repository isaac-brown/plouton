// <copyright file="LineItem.cs" company="Isaac Brown">
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Plouton.Domain.Entities;

#pragma warning disable SA1313 // Parameter should begin with lower-case letter. StyleCop doesn't seem to properly support record types.

/// <summary>
/// Represents a single line on an <see cref="Invoice"/>.
/// </summary>
/// <param name="Description">A description of the goods/services being purchased.</param>
/// <param name="Quantity">The number of the good/service being purchased. i.e. 12 carrots.</param>
/// <param name="AmountNet">The net (before tax) amount for a single line item.</param>
/// <param name="AmountTax">The tax charged for a single line item.</param>
/// <param name="Annotations">The annotations attached to this line item.</param>
public record LineItem(string Description,
                       int Quantity,
                       decimal AmountNet,
                       decimal AmountTax,
                       IReadOnlyList<LineAnnotation> Annotations)
{
    /// <summary>
    /// Gets the gross (after tax) amount for a single line item. i.e. AmountNet + AmountTax.
    /// </summary>
    public decimal AmountGross => this.AmountNet + this.AmountTax;

    /// <summary>
    /// Gets the net amount (before tax) for the line item(s). i.e. AmountNet * Quantity.
    /// </summary>
    public decimal LineAmountNet => this.AmountNet * this.Quantity;

    /// <summary>
    /// Gets the tax amount for the line item(s). i.e. AmountTax * Quantity.
    /// </summary>
    public decimal LineAmountTax => this.AmountTax * this.Quantity;

    /// <summary>
    /// Gets the gross (after tax) amount for the line item(s). i.e. AmountGross * Quantity.
    /// </summary>
    public decimal LineAmountGross => this.AmountGross * this.Quantity;
}
