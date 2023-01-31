namespace Catalog.Host.Models.Response;

public class GetBrandResponse<T>
{
    public T Id { get; set; } = default(T) !;
}