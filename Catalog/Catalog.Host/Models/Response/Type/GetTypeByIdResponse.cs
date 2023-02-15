using Catalog.Host.Data.Entities;

namespace Catalog.Host.Models.Response.Type;

public class GetTypeByIdResponse<T>
{
    public T Id { get; set; } = default(T) !;
}