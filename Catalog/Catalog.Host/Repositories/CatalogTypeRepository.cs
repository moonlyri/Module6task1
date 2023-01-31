using System.Linq;
using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Host.Repositories;

public class CatalogTypeRepository : ICatalogTypeRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<CatalogTypeRepository> _logger;

    public CatalogTypeRepository(
        Services.Interfaces.IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
        ILogger<CatalogTypeRepository> logger)
    {
        _dbContext = dbContextWrapper.DbContext;
        _logger = logger;
    }

    public async Task<int?> Add(int id, string? type)
    {
        var item = await _dbContext.AddAsync(new CatalogType()
        {
            Id = id,
            Type = type
        });

        await _dbContext.SaveChangesAsync();

        return item.Entity.Id;
    }

    public async Task<CatalogType> UpdateTypeAsync(CatalogType type)
    {
        if (type.Id != default)
        {
            _dbContext.Entry(type).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        return type;
    }

    public async Task<bool> DeleteTypeAsync(int id)
    {
        CatalogType type = await _dbContext.CatalogTypes.FirstAsync(c => c.Id == id);
        _dbContext.CatalogTypes.Remove(type);
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<CatalogType?> GetById(int id)
    {
        return await _dbContext.CatalogTypes.FirstOrDefaultAsync(f => f.Id == id);
    }
}