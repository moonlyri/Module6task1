using Microsoft.AspNetCore.Mvc;

namespace Catalog.Host.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderForecastController : ControllerBase
{
    private static readonly string[] Orders = new[]
    {
        "Hoodie", "T-Shirt", "Jeans", "Dress", "Blouse", "Jumper", "Trousers", "Top", "Blazer", 
        "Cardigan", "Jumpsuit", "Sweater", "Jacket", "Shirt", "Sweatshirt", "Blouse", "Skirt", "Shorts", "Underwear"
    };
    
    private static readonly string[] Size = new[]
    {
        "XS", "S", "M", "L", "XL", "XXL"
    };

    private readonly ILogger<OrderForecastController> _logger;

    public OrderForecastController(ILogger<OrderForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetOrderForecast")]
    public IEnumerable<OrderForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new OrderForecast
            {
                Date = DateTime.Now.AddDays(index),
                Size = Size[Random.Shared.Next(Size.Length)],
                Order = Orders[Random.Shared.Next(Orders.Length)]
            })
            .ToArray();
    }
}