namespace Plouton.Domain.Entities;

public struct ItemKey
{
    public ItemKey(string value)
    {
        var parts = value.Split(":");
        if (parts.Length != 2)
        {
            throw new ArgumentException("Must contain a key (of type Guid) and partition value separated by a colon ':'. For example 'b22e714d-089b-4493-9931-a47208758565:goats'.");
        }

        this.Id = Guid.Parse(parts.First());
        this.Partition = parts.Last();
    }

    public Guid Id { get; }
    public string Partition { get; }
}