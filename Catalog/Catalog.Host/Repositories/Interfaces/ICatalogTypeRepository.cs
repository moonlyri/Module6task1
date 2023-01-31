using Catalog.Host.Data;
using Catalog.Host.Data.Entities;

namespace Catalog.Host.Repositories.Interfaces;

public interface ICatalogTypeRepository
{
    Task<int?> Add(int id, string? type);
    Task<CatalogType> UpdateTypeAsync(CatalogType type);
    Task<bool> DeleteTypeAsync(int id);
    Task<CatalogType?> GetById(int id);
}