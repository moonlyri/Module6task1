using Catalog.Host.Data.Entities;

namespace Catalog.Host.Models.Requests;

public class GetTypeRequest
{
    public CatalogType? CatalogType { get; set; }
    public int CatalogTypeId { get; set; }
}