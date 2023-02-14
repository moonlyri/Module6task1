using Catalog.Host.Data.Entities;

namespace Catalog.Host.Models.Requests;

public class GetBrandRequest
{
    public CatalogBrand? CatalogBrand { get; set; }
    public int CatalogBrandId { get; set; }
}