using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Requests.Brand;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;

namespace Catalog.Host.Services;

public class CatalogBrandService : BaseDataService<ApplicationDbContext>, ICatalogBrandService
{
    private readonly ICatalogBrandRepository _catalogBrandRepository;

    public CatalogBrandService(
        IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
        ILogger<BaseDataService<ApplicationDbContext>> logger,
        ICatalogBrandRepository catalogBrandRepository)
        : base(dbContextWrapper, logger)
    {
        _catalogBrandRepository = catalogBrandRepository;
    }

    public Task<int?> Add(int id, string? brand)
    {
        return ExecuteSafeAsync(() => _catalogBrandRepository.Add(id, brand));
    }

    public Task<CatalogBrand?> GetBrandById(int id)
    {
        return ExecuteSafeAsync(() => _catalogBrandRepository.GetById(id));
    }

    public async Task<int> UpdateBrandAsync(UpdateBrandRequest brand)
    {
        var result = new CatalogBrand()
        {
            Brand = brand.Brand
        };
        await _catalogBrandRepository.UpdateBrandAsync(result);
        return result.Id;
    }

    public Task<bool> DeleteBrandAsync(int id)
    {
        return ExecuteSafeAsync(() => _catalogBrandRepository.DeleteBrandAsync(id));
    }
}