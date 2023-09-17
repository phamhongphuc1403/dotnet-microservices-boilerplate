using BuildingBlock.EntityFrameworkCore;
using TinyCRM.SaleManagement.Domain.Entities;
using TinyCRM.SaleManagement.Domain.Repositories;

namespace TinyCRM.SaleManagement.EntityFrameworkCore.Repositories;

public class LeadOperationRepository : OperationRepository<SaleDbContext, Lead>, ILeadOperationRepository
{
    public LeadOperationRepository(SaleDbContext dbContext) : base(dbContext)
    {
    }
}