namespace TinyCRM.Domain.Entities;

public class PermissionEntity
{
    public PermissionEntity(string type, string value)
    {
        Type = type;
        Value = value;
    }

    public string Type { get; set; }
    public string Value { get; set; }
}