using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Requests;
using Catalog.Host.Models.Requests.Brand;
using Catalog.Host.Models.Requests.Type;

namespace Catalog.Host.Services.Interfaces;

public interface ICatalogBrandService
{
    Task<int?> Add(int id, string? brand);
    Task<CatalogBrand?> GetBrandById(int id);
    Task<int> UpdateBrandAsync(UpdateBrandRequest brand);
    Task<bool> DeleteBrandAsync(int id);
}