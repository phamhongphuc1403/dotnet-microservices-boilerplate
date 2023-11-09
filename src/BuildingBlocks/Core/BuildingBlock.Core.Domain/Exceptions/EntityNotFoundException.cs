namespace BuildingBlock.Core.Domain.Exceptions;

public class EntityNotFoundException : Exception
{
    protected EntityNotFoundException(string entity, string column, object value) : base(
        $"{entity} with {column}: '{value}' is not found")
    {
    }

    protected EntityNotFoundException(string entity, Guid id) : base(
        $"{entity} with id: '{id}' is not found")
    {
    }

    protected EntityNotFoundException(string entity, string id) : base(
        $"{entity} with id: '{id}' is not found")
    {
    }

    public EntityNotFoundException(string message) : base(message)
    {
    }
}