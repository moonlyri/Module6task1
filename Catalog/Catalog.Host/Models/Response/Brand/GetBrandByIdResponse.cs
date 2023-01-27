namespace Catalog.Host.Models.Response.Brand;

public class GetBrandByIdResponse<T>
{
    public T Id { get; set; } = default(T) !;
}