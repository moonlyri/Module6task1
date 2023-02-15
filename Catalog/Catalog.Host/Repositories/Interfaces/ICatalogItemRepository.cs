using Catalog.Host.Data;
using Catalog.Host.Data.Entities;

namespace Catalog.Host.Repositories.Interfaces;

public interface ICatalogItemRepository
{
    Task<PaginatedItems<CatalogItem>> GetByPageAsync(int pageIndex, int pageSize, int? brandFilter, int? typeFilter);
    Task<int?> Add(string name, string description, decimal price, int availableStock, int catalogBrandId, int catalogTypeId, string pictureFileName);
    Task<CatalogItem> UpdateItemAsync(CatalogItem item);
    Task<bool> DeleteItemAsync(int id);
    Task<CatalogItem?> GetById(int id);
    Task<PaginatedItems<CatalogItem>> GetBrand(string brand, int pageIndex, int pageSize);
    Task<PaginatedItems<CatalogItem>> GetType(string type, int pageIndex, int pageSize);
}