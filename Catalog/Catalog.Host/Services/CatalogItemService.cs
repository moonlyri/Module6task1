using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Requests;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;

namespace Catalog.Host.Services;

public class CatalogItemService : BaseDataService<ApplicationDbContext>, ICatalogItemService
{
    private readonly ICatalogItemRepository _catalogItemRepository;

    public CatalogItemService(
        Interfaces.IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
        ILogger<BaseDataService<ApplicationDbContext>> logger,
        ICatalogItemRepository catalogItemRepository)
        : base(dbContextWrapper, logger)
    {
        _catalogItemRepository = catalogItemRepository;
    }

    public Task<int?> Add(string name, string description, decimal price, int availableStock, int catalogBrandId, int catalogTypeId, string pictureFileName)
    {
        return ExecuteSafeAsync(() => _catalogItemRepository.Add(name, description, price, availableStock, catalogBrandId, catalogTypeId, pictureFileName));
    }

    public Task<CatalogItem?> GetItemById(int id)
    {
        return ExecuteSafeAsync(() => _catalogItemRepository.GetById(id));
    }

    public async Task<int> UpdateItemAsync(UpdateItemRequest item)
    {
        var result = new CatalogItem()
        {
            Name = item.Name,
            Description = item.Description,
            Price = item.Price,
            PictureFileName = item.PictureFileName
        };
        await _catalogItemRepository.UpdateItemAsync(result);
        return result.Id;
    }

    public Task<bool> DeleteItemAsync(int id)
    {
        return ExecuteSafeAsync(() => _catalogItemRepository.DeleteItemAsync(id));
    }

    public Task<CatalogItem?> GetBrand(int id)
    {
        return ExecuteSafeAsync(() => _catalogItemRepository.GetBrand(id));
    }

    public Task<CatalogItem?> GetType(int id)
    {
        return ExecuteSafeAsync(() => _catalogItemRepository.GetType(id));
    }
}