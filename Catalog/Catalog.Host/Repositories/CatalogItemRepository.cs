using System.Linq;
using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Host.Repositories;

public class CatalogItemRepository : ICatalogItemRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<CatalogItemRepository> _logger;

    public CatalogItemRepository(
        Services.Interfaces.IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
        ILogger<CatalogItemRepository> logger)
    {
        _dbContext = dbContextWrapper.DbContext;
        _logger = logger;
    }

    public async Task<PaginatedItems<CatalogItem>> GetByPageAsync(int pageIndex, int pageSize, int? brandFilter, int? typeFilter)
    {
        IQueryable<CatalogItem> query = _dbContext.CatalogItems;

        if (brandFilter.HasValue)
        {
            query = query.Where(w => w.CatalogBrandId == brandFilter.Value);
        }

        if (typeFilter.HasValue)
        {
            query = query.Where(w => w.CatalogTypeId == typeFilter.Value);
        }

        var totalItems = await query.LongCountAsync();

        var itemsOnPage = await query.OrderBy(c => c.Name)
            .Include(i => i.CatalogBrand)
            .Include(i => i.CatalogType)
            .Skip(pageSize * pageIndex)
            .Take(pageSize)
            .ToListAsync();

        return new PaginatedItems<CatalogItem>() { TotalCount = totalItems, Data = itemsOnPage };
    }

    public async Task<int?> Add(string name, string description, decimal price, int availableStock, int catalogBrandId, int catalogTypeId, string pictureFileName)
    {
        var item = await _dbContext.AddAsync(new CatalogItem
        {
            CatalogBrandId = catalogBrandId,
            CatalogTypeId = catalogTypeId,
            Description = description,
            Name = name,
            PictureFileName = pictureFileName,
            Price = price
        });

        await _dbContext.SaveChangesAsync();

        return item.Entity.Id;
    }

    public async Task<CatalogItem> UpdateItemAsync(CatalogItem item)
    {
        if (item.Id != default)
        {
            _dbContext.Entry(item).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        return item;
    }

    public async Task<bool> DeleteItemAsync(int id)
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

    public async Task<CatalogItem?> GetById(int id)
    {
        return await _dbContext.CatalogItems
            .Include(i => i.Id)
            .Include(i => i.CatalogBrand)
            .Include(i => i.CatalogType)
            .FirstOrDefaultAsync(f => f.Id == id);
    }

    public async Task<PaginatedItems<CatalogItem>> GetBrand(string brand, int pageIndex, int pageSize)
    {
        var itemsOnPage = await _dbContext.CatalogItems
            .Include(i => i.CatalogBrand)
            .Include(i => i.CatalogType)
            .Where(s => s.CatalogBrand.Brand == brand)
            .Skip(pageIndex * pageSize)
            .Take(pageSize)
            .ToListAsync();
        return new PaginatedItems<CatalogItem>() { TotalCount = itemsOnPage.Count, Data = itemsOnPage };
    }

    public async Task<PaginatedItems<CatalogItem>> GetType(string type, int pageIndex, int pageSize)
    {
        var itemsOnPage = await _dbContext.CatalogItems
            .Include(i => i.CatalogBrand)
            .Include(i => i.CatalogType)
            .Where(s => s.CatalogType.Type == type)
            .Skip(pageIndex * pageSize)
            .Take(pageSize)
            .ToListAsync();
        return new PaginatedItems<CatalogItem>() { TotalCount = itemsOnPage.Count, Data = itemsOnPage };
    }
}