using Catalog.Host.Data;
using Catalog.Host.Data.Entities;

namespace Catalog.Host.Repositories.Interfaces;

public interface ICatalogBrandRepository
{
    Task<int?> Add(string? brand, string? description);
    Task<CatalogBrand> UpdateBrandAsync(CatalogBrand brand);
    Task<bool> DeleteBrandAsync(int id);
    Task<CatalogBrand?> GetById(int id);
}