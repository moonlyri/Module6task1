using System.Linq;
using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Host.Repositories;

public class CatalogBrandRepository : ICatalogBrandRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<CatalogBrandRepository> _logger;

    public CatalogBrandRepository(
        Services.Interfaces.IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
        ILogger<CatalogBrandRepository> logger)
    {
        _dbContext = dbContextWrapper.DbContext;
        _logger = logger;
    }

    public async Task<int?> Add(string? brand, string? description)
    {
        var item = await _dbContext.AddAsync(new CatalogBrand()
        {
            Brand = brand,
            Description = description
        });

        await _dbContext.SaveChangesAsync();

        return item.Entity.Id;
    }

    public async Task<CatalogBrand> UpdateBrandAsync(CatalogBrand brand)
    {
        if (brand.Id != default)
        {
            _dbContext.Entry(brand).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        return brand;
    }

    public async Task<bool> DeleteBrandAsync(int id)
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

    public async Task<CatalogBrand?> GetById(int id)
    {
        return await _dbContext.CatalogBrands
            .Include(i => i.Id)
            .FirstOrDefaultAsync(f => f.Id == id);
    }
}