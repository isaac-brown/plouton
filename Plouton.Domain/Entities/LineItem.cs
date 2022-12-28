// <copyright file="LineItem.cs" company="Isaac Brown">
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Plouton.Domain.Entities;

public record LineItem(string Description,
                       int Quantity,
                       decimal AmountNet,
                       decimal AmountTax,
                       IReadOnlyList<LineAnnotation> Annotations)
{
    public decimal AmountGross => this.AmountNet + this.AmountTax;
    public decimal LineAmountNet => this.AmountNet * this.Quantity;
    public decimal LineAmountTax => this.AmountTax * this.Quantity;
    public decimal LineAmountGross => this.AmountGross * this.Quantity;
};
