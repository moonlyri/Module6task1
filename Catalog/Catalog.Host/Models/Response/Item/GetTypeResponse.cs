namespace Catalog.Host.Models.Response;

public class GetTypeResponse<T>
{
    public T Id { get; set; } = default(T) !;
}