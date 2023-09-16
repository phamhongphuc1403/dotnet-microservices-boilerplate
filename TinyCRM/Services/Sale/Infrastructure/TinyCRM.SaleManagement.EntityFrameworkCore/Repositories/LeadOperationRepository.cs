using BuildingBlock.EntityFrameworkCore;
using TinyCRM.SaleManagement.Domain.Entities;
using TinyCRM.SaleManagement.Domain.Repositories;

namespace TinyCRM.SaleManagement.EntityFrameworkCore.Repositories;

public class LeadOperationRepository : OperationRepository<Lead>, ILeadOperationRepository
{
    public LeadOperationRepository(DbFactory dbFactory) : base(dbFactory)
    {
    }
}