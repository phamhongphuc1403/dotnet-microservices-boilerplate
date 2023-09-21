namespace BuildingBlock.Domain.Exceptions;

public class EntityNotFoundException : Exception
{
    public EntityNotFoundException(string entity, string column, object value) : base(
        $"{entity} with {column}: '{value}' is not found")
    {
    }

    public EntityNotFoundException(string entity, Guid id) : base(
        $"{entity} with id: '{id}' is not found")
    {
    }
}