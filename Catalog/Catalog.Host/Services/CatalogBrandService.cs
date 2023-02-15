using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Requests.Brand;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;

namespace Catalog.Host.Services;

public class CatalogBrandService : BaseDataService<ApplicationDbContext>, ICatalogBrandService
{
    private readonly ICatalogBrandRepository _catalogBrandRepository;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;

    public CatalogBrandService(
        Interfaces.IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
        ILogger<BaseDataService<ApplicationDbContext>> logger,
        IMapper mapper,
        ICatalogBrandRepository catalogBrandRepository)
        : base(dbContextWrapper, logger)
    {
        _catalogBrandRepository = catalogBrandRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public Task<int?> Add(string? brand, string? description)
    {
        return ExecuteSafeAsync(() => _catalogBrandRepository.Add(brand, description));
    }

    public async Task<CatalogBrandDto> GetBrandById(int id)
    {
        return await ExecuteSafeAsync(async () =>
        {
            var result = await _catalogBrandRepository.GetById(id);
            if (result == null)
            {
                return new CatalogBrandDto();
            }

            return _mapper.Map<CatalogBrandDto>(result);
        });
    }

    public async Task<int> UpdateBrandAsync(UpdateBrandRequest brand)
    {
        var result = new CatalogBrand()
        {
            Brand = brand.Brand,
            Description = brand.Description
        };
        await _catalogBrandRepository.UpdateBrandAsync(result);
        return result.Id;
    }

    public async Task<bool> DeleteBrandAsync(int id)
    {
        return await ExecuteSafeAsync<bool>(async () =>
        {
            var result = await _catalogBrandRepository.DeleteBrandAsync(id);
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