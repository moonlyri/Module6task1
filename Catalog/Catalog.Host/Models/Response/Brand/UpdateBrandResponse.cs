namespace Catalog.Host.Models.Response.Brand;

public class UpdateBrandResponse<T>
{
    public T Id { get; set; } = default(T) !;
}