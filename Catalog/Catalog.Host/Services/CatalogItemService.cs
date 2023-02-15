using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Requests;
using Catalog.Host.Models.Response;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;

namespace Catalog.Host.Services;

public class CatalogItemService : BaseDataService<ApplicationDbContext>, ICatalogItemService
{
    private readonly ICatalogItemRepository _catalogItemRepository;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;

    public CatalogItemService(
        Interfaces.IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
        ILogger<BaseDataService<ApplicationDbContext>> logger,
        IMapper mapper,
        ICatalogItemRepository catalogItemRepository)
        : base(dbContextWrapper, logger)
    {
        _catalogItemRepository = catalogItemRepository;
        _mapper = mapper;
        _logger = logger;
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

    public async Task<bool> DeleteItemAsync(int id)
    {
        return await ExecuteSafeAsync<bool>(async () =>
        {
            var result = await _catalogItemRepository.DeleteItemAsync(id);
            if (!result)
            {
                _logger.LogError("Deleting failed");
                return result;
            }

            _logger.LogInformation("Deleting succeed");
            return result;
        });
    }

    public async Task<PaginatedItems<CatalogItem>> GetBrand(string brand, int pageIndex, int pageSize)
    {
        return await ExecuteSafeAsync(async () =>
        {
            return await _catalogItemRepository.GetBrand(brand, pageIndex, pageSize);
        });
    }

    public async Task<PaginatedItems<CatalogItem>> GetType(string type, int pageIndex, int pageSize)
    {
        return await ExecuteSafeAsync(async () =>
        {
            return await _catalogItemRepository.GetType(type, pageIndex, pageSize);
        });
    }
}