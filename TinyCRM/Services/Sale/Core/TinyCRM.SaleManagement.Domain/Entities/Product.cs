using TinyCRM.Core;

namespace TinyCRM.SaleManagement.Domain.Entities;

public class Product : GuidBaseEntity
{
    public string Code { get; private set; } = null!;
}