using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Requests;

namespace Catalog.Host.Services.Interfaces;

public interface ICatalogItemService
{
    Task<int?> Add(string name, string description, decimal price, int availableStock, int catalogBrandId, int catalogTypeId, string pictureFileName);
    Task<CatalogItem?> GetItemById(int id);
    Task<int> UpdateItemAsync(UpdateItemRequest item);
    Task<bool> DeleteItemAsync(int id);
    Task<CatalogItem?> GetBrand(int id);
    Task<CatalogItem?> GetType(int id);
}