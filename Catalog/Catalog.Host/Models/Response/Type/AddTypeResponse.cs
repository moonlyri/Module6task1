namespace Catalog.Host.Models.Response.Type;

public class AddTypeResponse<T>
{
    public T Id { get; set; } = default(T) !;
}