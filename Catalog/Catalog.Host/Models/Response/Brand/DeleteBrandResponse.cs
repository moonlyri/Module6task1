namespace Catalog.Host.Models.Response.Brand;

public class DeleteBrandResponse<T>
{
    public T Id { get; set; } = default(T) !;
}