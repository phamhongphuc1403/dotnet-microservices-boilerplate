namespace BuildingBlock.Core.Domain.Shared.Constants;

public static class CacheKeyRegistry
{
    public static RecordKey GetEmailConfirmationByUserIdKey(string userId)
    {
        return new RecordKey($"email-{userId}");
    }

    public static RecordKey GetRolesByUserIdKey(string userId)
    {
        return new RecordKey($"role-{userId}");
    }

    public static RecordKey GetPermissionsByRoleNameKey(string roleName)
    {
        return new RecordKey($"permission-{roleName}");
    }
}

public class RecordKey
{
    public RecordKey(string value)
    {
        Value = value.ToUpper();
    }

    public string Value { get; }
}