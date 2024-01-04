namespace BuildingBlock.Core.Domain.Exceptions;

public class EntityConflictException : Exception
{
    protected EntityConflictException(string entity, string column, object value) : base(
        $"{entity} with {column}: '{value}' is already existed")
    {
    }

    protected EntityConflictException(string message) : base(message)
    {
    }

    protected EntityConflictException(string entity, Guid id) : base(
        $"{entity} with id: '{id}' is not found")
    {
    }

    protected EntityConflictException(Guid id) : base($"Entity with id: '{id}' is already existed")
    {
    }
}