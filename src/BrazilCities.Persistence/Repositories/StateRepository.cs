using BrazilCities.Application.Repositories;
using BrazilCities.Domain.Entities;
using BrazilCities.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace BrazilCities.Persistence.Repositories;

public sealed class StateRepository(AppDbContext appDbContext) : RepositoryBase<StateEntity>(appDbContext), IStateRepository
{
    public async Task<StateEntity?> FindByAcronymAsync(string acronym, CancellationToken cancellationToken)
    {
        return await DbSet.FirstOrDefaultAsync(state => state.StateAcronym  == acronym.ToUpper(), cancellationToken);
    }
}