using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Requests;
using Catalog.Host.Models.Response;

namespace Catalog.Host.Services.Interfaces;

public interface ICatalogItemService
{
    Task<int?> Add(string name, string description, decimal price, int availableStock, int catalogBrandId, int catalogTypeId, string pictureFileName);
    Task<CatalogItem?> GetItemById(int id);
    Task<int> UpdateItemAsync(UpdateItemRequest item);
    Task<bool> DeleteItemAsync(int id);
    Task<PaginatedItems<CatalogItem>> GetBrand(string brand, int pageIndex, int pageSize);
    Task<PaginatedItems<CatalogItem>> GetType(string type, int pageIndex, int pageSize);
}