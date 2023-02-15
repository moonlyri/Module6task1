using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Requests;
using Catalog.Host.Models.Requests.Type;

namespace Catalog.Host.Services.Interfaces;

public interface ICatalogTypeService
{
    Task<int?> Add(string? type, string? description);
    Task<CatalogTypeDto> GetTypeById(int id);
    Task<int> UpdateTypeAsync(UpdateTypeRequest type);
    Task<bool> DeleteTypeAsync(int id);
}