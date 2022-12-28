

using Plouton.Domain.Entities;
using Plouton.Persistence.CosmosDb.Records;

namespace Plouton.Persistence.CosmosDb.Extensions;

public static class LineItemExtensions
{
    public static LineItemRecord ToLineItemRecord(this LineItem lineItem)
    {
        return new LineItemRecord
        {
            Description = lineItem.Description,
            AmountNet = lineItem.AmountNet,
            AmountTax = lineItem.AmountNet,
            Quantity = lineItem.Quantity,
            Annotations = lineItem.Annotations.Select(annotation => new LineAnnotationRecord{ Description = annotation.Description }).ToList(),
        };
    }
}