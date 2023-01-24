using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Requests;
using Catalog.Host.Models.Requests.Type;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;

namespace Catalog.Host.Services;

public class CatalogTypeService : BaseDataService<ApplicationDbContext>, ICatalogTypeService
{
    private readonly ICatalogTypeRepository _catalogTypeRepository;

    public CatalogTypeService(
        IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
        ILogger<BaseDataService<ApplicationDbContext>> logger,
        ICatalogTypeRepository catalogTypeRepository)
        : base(dbContextWrapper, logger)
    {
        _catalogTypeRepository = catalogTypeRepository;
    }

    public Task<int?> Add(int id, string? type)
    {
        return ExecuteSafeAsync(() => _catalogTypeRepository.Add(id, type));
    }

    public Task<CatalogType?> GetTypeById(int id)
    {
        return ExecuteSafeAsync(() => _catalogTypeRepository.GetById(id));
    }

    public async Task<int> UpdateTypeAsync(UpdateTypeRequest type)
    {
        var result = new CatalogType()
        {
            Type = type.Type
        };
        await _catalogTypeRepository.UpdateTypeAsync(result);
        return result.Id;
    }

    public Task<bool> DeleteTypeAsync(int id)
    {
        return ExecuteSafeAsync(() => _catalogTypeRepository.DeleteTypeAsync(id));
    }
}