using System.Net;
using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Requests;
using Catalog.Host.Models.Requests.Type;
using Catalog.Host.Models.Response;
using Catalog.Host.Models.Response.Type;
using Catalog.Host.Services.Interfaces;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Host.Controllers;

[ApiController]
[Route(ComponentDefaults.DefaultRoute)]
public class CatalogTypeController : ControllerBase
{
    private readonly ILogger<CatalogTypeController> _logger;
    private readonly ICatalogTypeService _catalogTypeService;

    public CatalogTypeController(ILogger<CatalogTypeController> logger, ICatalogTypeService catalogTypeService)
    {
        _logger = logger;
        _catalogTypeService = catalogTypeService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(AddTypeResponse<int?>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Add(AddTypeRequest request)
    {
        var result = await _catalogTypeService.Add(request.Id, request.Type);
        return Ok(new AddItemResponse<int?>() { Id = result });
    }

    [HttpPost]
    [ProducesResponseType(typeof(UpdateTypeResponse<int?>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Update(UpdateTypeRequest request)
    {
        var result = await _catalogTypeService.UpdateTypeAsync(request);
        return Ok(new UpdateTypeResponse<int?>() { Id = result });
    }

    [HttpPost]
    [ProducesResponseType(typeof(DeleteTypeResponse<int?>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Delete(DeleteTypeRequest request)
    {
        var result = await _catalogTypeService.DeleteTypeAsync(request.Id);
        return Ok(new DeleteTypeResponse<bool>() { Id = result });
    }

    [HttpPost]
    [ProducesResponseType(typeof(GetTypeByIdResponse<int?>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetTypeById(GetTypeByIdRequest request)
    {
        var result = await _catalogTypeService.GetTypeById(request.Id);
        return Ok(new GetTypeByIdResponse<CatalogType?>() { Id = result });
    }
}