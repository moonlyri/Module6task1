namespace Catalog.Host.Models.Response.Brand;

public class AddBrandResponse<T>
{
    public T Id { get; set; } = default(T) !;
}