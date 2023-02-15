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

    public async Task<int?> Add(string? type, string? description)
    {
        var item = await _dbContext.AddAsync(new CatalogType()
        {
            Type = type,
            Description = description
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
        var item = await GetById(id);
        if (item == null)
        {
            return false;
        }

        _dbContext.Entry(item).State = EntityState.Deleted;
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<CatalogType?> GetById(int id)
    {
        return await _dbContext.CatalogTypes
            .Include(i => i.Id)
            .FirstOrDefaultAsync(f => f.Id == id);
    }
}