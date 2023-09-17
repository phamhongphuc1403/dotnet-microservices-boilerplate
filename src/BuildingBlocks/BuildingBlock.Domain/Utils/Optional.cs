namespace BuildingBlock.Domain.Utils;

public class Optional<T>
{
    private readonly T? _instance;

    private Optional(T? instance)
    {
        _instance = instance;
    }

    public static Optional<T> Of(T? instance)
    {
        return new Optional<T>(instance);
    }

    public Optional<T> ThrowIfPresent(Exception exception)
    {
        if ((_instance is bool && _instance.Equals(true)) || (_instance is not bool && _instance != null))
            throw exception;

        return this;
    }

    public Optional<T> ThrowIfNotPresent(Exception exception)
    {
        if ((_instance is bool && _instance.Equals(false)) || _instance == null) throw exception;

        return this;
    }

    public T Get()
    {
        return _instance ?? throw new InvalidOperationException("No value present");
    }
}