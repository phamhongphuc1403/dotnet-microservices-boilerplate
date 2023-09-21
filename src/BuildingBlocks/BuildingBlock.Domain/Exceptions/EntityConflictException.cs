namespace BuildingBlock.Domain.Exceptions;

public class EntityConflictException : Exception
{
    public EntityConflictException(string entity, string column, object value) : base(
        $"{entity} with {column}: '{value}' is conflicted")
    {
    }
}