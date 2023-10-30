namespace BuildingBlock.Domain.Exceptions;

public class EntityConflictException : Exception
{
    protected EntityConflictException(string entity, string column, object value) : base(
        $"{entity} with {column}: '{value}' is already existed")
    {
    }

    protected EntityConflictException(string message) : base(message)
    {
    }
}