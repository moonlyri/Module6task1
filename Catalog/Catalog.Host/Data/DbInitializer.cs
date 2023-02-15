using System.Collections.Generic;
using System.Linq;
using Catalog.Host.Data.Entities;

namespace Catalog.Host.Data;

public static class DbInitializer
{
    public static async Task Initialize(ApplicationDbContext context)
    {
        await context.Database.EnsureCreatedAsync();

        if (!context.CatalogBrands.Any())
        {
            await context.CatalogBrands.AddRangeAsync(GetPreconfiguredCatalogBrands());

            await context.SaveChangesAsync();
        }

        if (!context.CatalogTypes.Any())
        {
            await context.CatalogTypes.AddRangeAsync(GetPreconfiguredCatalogTypes());

            await context.SaveChangesAsync();
        }

        if (!context.CatalogItems.Any())
        {
            await context.CatalogItems.AddRangeAsync(GetPreconfiguredItems());

            await context.SaveChangesAsync();
        }
    }

    private static IEnumerable<CatalogBrand> GetPreconfiguredCatalogBrands()
    {
        return new List<CatalogBrand>()
        {
            new CatalogBrand() { Brand = "Lavazza" },
            new CatalogBrand() { Brand = "Chorna Karta" },
            new CatalogBrand() { Brand = "Ambassador" },
            new CatalogBrand() { Brand = "Fort" },
            new CatalogBrand() { Brand = "Other" }
        };
    }

    private static IEnumerable<CatalogType> GetPreconfiguredCatalogTypes()
    {
        return new List<CatalogType>()
        {
            new CatalogType() { Type = "Cappucino" },
            new CatalogType() { Type = "Espresso" },
            new CatalogType() { Type = "Latte" },
            new CatalogType() { Type = "Flat White" }
        };
    }

    private static IEnumerable<CatalogItem> GetPreconfiguredItems()
    {
        return new List<CatalogItem>()
        {
            new CatalogItem { CatalogTypeId = 2, CatalogBrandId = 2, AvailableStock = 100, Description = "shot of espresso topped with hot water", Name = "Lavazza Americano", Price = 2, PictureFileName = "1.png" },
            new CatalogItem { CatalogTypeId = 1, CatalogBrandId = 2, AvailableStock = 100, Description = "half brewed coffee and half steamed milk", Name = "Fort Caffe Misto", Price = 3, PictureFileName = "2.png" },
            new CatalogItem { CatalogTypeId = 2, CatalogBrandId = 5, AvailableStock = 100, Description = "Espresso-based hot coffee beverage, served in a 5 fl. oz. (150 ml) to 7 fl. oz. (200 ml) cup.\n\nIt’s made of a shot of espresso and steamed and frothed milk.\n\nA classic cappuccino generally contains equal amounts of milk foam and steamed milk.", Name = "Cappuccino", Price = 4, PictureFileName = "3.png" },
            new CatalogItem { CatalogTypeId = 2, CatalogBrandId = 2, AvailableStock = 100, Description = "Dark Roast coffees are dark brown, even close to a blackened color.", Name = "Dark Roast Coffee", Price = 2, PictureFileName = "4.png" },
            new CatalogItem { CatalogTypeId = 3, CatalogBrandId = 5, AvailableStock = 100, Description = "mostly espresso, marked with a small amount of steamed milk and foam for those who love a rich, bold taste", Name = "Espresso Macchiato", Price = 2.5M, PictureFileName = "5.png" },
            new CatalogItem { CatalogTypeId = 2, CatalogBrandId = 2, AvailableStock = 100, Description = "blend of micro-foamed milk poured over a single or double shot of espresso.", Name = "Flat White", Price = 4, PictureFileName = "6.png" },
            new CatalogItem { CatalogTypeId = 2, CatalogBrandId = 5, AvailableStock = 100, Description = "concentrated form of coffee, served in shots. It's made of two ingredients - finely ground, 100% coffee, and hot water.", Name = "Espresso", Price = 1, PictureFileName = "7.png" }
        };
    }
}