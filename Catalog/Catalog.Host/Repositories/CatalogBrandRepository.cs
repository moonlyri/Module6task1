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
        IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
        ILogger<CatalogBrandRepository> logger)
    {
        _dbContext = dbContextWrapper.DbContext;
        _logger = logger;
    }

    public async Task<PaginatedItems<CatalogBrand>> GetByPageAsync(int pageIndex, int pageSize)
    {
        var totalItems = await _dbContext.CatalogBrands
            .LongCountAsync();

        var itemsOnPage = await _dbContext.CatalogBrands
            .Include(i => i.Brand)
            .OrderBy(c => c.Id)
            .Skip(pageSize * pageIndex)
            .Take(pageSize)
            .ToListAsync();

        return new PaginatedItems<CatalogBrand>() { TotalCount = totalItems, Data = itemsOnPage };
    }

    public async Task<int?> Add(int id, string? brand)
    {
        var item = await _dbContext.AddAsync(new CatalogBrand()
        {
            Id = id,
            Brand = brand
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
        CatalogBrand brand = await _dbContext.CatalogBrands.FirstAsync(c => c.Id == id);
        _dbContext.CatalogBrands.Remove(brand);
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<CatalogBrand?> GetById(int id)
    {
        return await _dbContext.CatalogBrands.FirstOrDefaultAsync(f => f.Id == id);
    }
}