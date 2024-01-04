namespace BuildingBlock.Core.Domain.ValueObject;

public abstract class ValueObject : IEquatable<ValueObject>
{
    public bool Equals(ValueObject? other)
    {
        return other is not null && EqualsTo(other);
    }

    public abstract IEnumerable<object?> GetValues();

    protected abstract void ValidateValues();

    public override bool Equals(object? obj)
    {
        return obj is ValueObject other && EqualsTo(other);
    }

    public override int GetHashCode()
    {
        return GetValues().Aggregate(default(int), HashCode.Combine);
    }

    private bool EqualsTo(ValueObject other)
    {
        return GetValues().SequenceEqual(other.GetValues());
    }
}