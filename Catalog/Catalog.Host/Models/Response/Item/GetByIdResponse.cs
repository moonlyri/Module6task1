using Catalog.Host.Data.Entities;

namespace Catalog.Host.Models.Response;

public class GetByIdResponse<T>
{
    public T Id { get; set; } = default(T) !;
    public CatalogItem? Item { get; set; }
}