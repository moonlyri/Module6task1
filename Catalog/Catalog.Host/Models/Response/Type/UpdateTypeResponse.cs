namespace Catalog.Host.Models.Response.Type;

public class UpdateTypeResponse<T>
{
    public T Id { get; set; } = default(T) !;
}