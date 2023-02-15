using Catalog.Host.Data;
using Catalog.Host.Data.Entities;

namespace Catalog.Host.Repositories.Interfaces;

public interface ICatalogTypeRepository
{
    Task<int?> Add(string? type, string? description);
    Task<CatalogType> UpdateTypeAsync(CatalogType type);
    Task<bool> DeleteTypeAsync(int id);
    Task<CatalogType?> GetById(int id);
}