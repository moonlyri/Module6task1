namespace Catalog.Host.Models.Response.Type;

public class DeleteTypeResponse<T>
{
    public T Id { get; set; } = default(T) !;
}