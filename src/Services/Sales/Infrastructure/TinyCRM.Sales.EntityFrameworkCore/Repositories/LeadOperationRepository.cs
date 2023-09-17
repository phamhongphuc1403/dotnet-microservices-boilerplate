using BuildingBlock.EntityFrameworkCore;
using TinyCRM.Sales.Domain.Entities;
using TinyCRM.Sales.Domain.Repositories;

namespace TinyCRM.Sales.EntityFrameworkCore.Repositories;

public class LeadOperationRepository : OperationRepository<SaleDbContext, Lead>, ILeadOperationRepository
{
    public LeadOperationRepository(SaleDbContext dbContext) : base(dbContext)
    {
    }
}