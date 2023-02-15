using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Requests;
using Catalog.Host.Models.Requests.Brand;
using Catalog.Host.Models.Requests.Type;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;

namespace Catalog.Host.Services;

public class CatalogTypeService : BaseDataService<ApplicationDbContext>, ICatalogTypeService
{
    private readonly ICatalogTypeRepository _catalogTypeRepository;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;

    public CatalogTypeService(
        Interfaces.IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
        ILogger<BaseDataService<ApplicationDbContext>> logger,
        IMapper mapper,
        ICatalogTypeRepository catalogTypeRepository)
        : base(dbContextWrapper, logger)
    {
        _catalogTypeRepository = catalogTypeRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public Task<int?> Add(string? type, string? description)
    {
        return ExecuteSafeAsync(() => _catalogTypeRepository.Add(type, description));
    }

    public async Task<CatalogTypeDto> GetTypeById(int id)
    {
        return await ExecuteSafeAsync(async () =>
        {
            var result = await _catalogTypeRepository.GetById(id);
            if (result == null)
            {
                return new CatalogTypeDto();
            }

            return _mapper.Map<CatalogTypeDto>(result);
        });
    }

    public async Task<int> UpdateTypeAsync(UpdateTypeRequest type)
    {
        var result = new CatalogType()
        {
            Type = type.Type,
            Description = type.Description
        };
        await _catalogTypeRepository.UpdateTypeAsync(result);
        return result.Id;
    }

    public async Task<bool> DeleteTypeAsync(int id)
    {
        return await ExecuteSafeAsync<bool>(async () =>
        {
            var result = await _catalogTypeRepository.DeleteTypeAsync(id);
            if (!result)
            {
                _logger.LogError("Deleting failed");
                return result;
            }

            _logger.LogInformation("Deleting succeed");
            return result;
        });
    }
}