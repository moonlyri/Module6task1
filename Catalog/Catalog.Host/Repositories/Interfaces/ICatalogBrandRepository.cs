using Catalog.Host.Data;
using Catalog.Host.Data.Entities;

namespace Catalog.Host.Repositories.Interfaces;

public interface ICatalogBrandRepository
{
    Task<PaginatedItems<CatalogBrand>> GetByPageAsync(int pageIndex, int pageSize);
    Task<int?> Add(int id, string? brand);
    Task<CatalogBrand> UpdateBrandAsync(CatalogBrand brand);
    Task<bool> DeleteBrandAsync(int id);
    Task<CatalogBrand?> GetById(int id);
}