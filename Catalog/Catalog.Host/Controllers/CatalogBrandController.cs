using System.Net;
using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Requests.Brand;
using Catalog.Host.Models.Requests.Type;
using Catalog.Host.Models.Response;
using Catalog.Host.Models.Response.Brand;
using Catalog.Host.Models.Response.Type;
using Catalog.Host.Services.Interfaces;
using Infrastructure;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Host.Controllers;

[ApiController]
[Authorize(Policy = AuthPolicy.AllowClientPolicy)]
[Scope("catalog.catalogbrand")]
[Route(ComponentDefaults.DefaultRoute)]
public class CatalogBrandController : ControllerBase
{
    private readonly ILogger<CatalogBrandController> _logger;
    private readonly ICatalogBrandService _catalogBrandService;

    public CatalogBrandController(ILogger<CatalogBrandController> logger, ICatalogBrandService catalogBrandService)
    {
        _logger = logger;
        _catalogBrandService = catalogBrandService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(AddBrandResponse<int?>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Add(AddBrandRequest request)
    {
        var result = await _catalogBrandService.Add(request.Brand, request.Description);
        return Ok(new AddBrandResponse<int?>() { Id = result });
    }

    [HttpPost]
    [ProducesResponseType(typeof(UpdateBrandResponse<int?>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Update(UpdateBrandRequest request)
    {
        var result = await _catalogBrandService.UpdateBrandAsync(request);
        return Ok(new UpdateBrandResponse<int?>() { Id = result });
    }

    [HttpPost]
    [ProducesResponseType(typeof(DeleteBrandResponse<int?>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Delete(DeleteBrandRequest request)
    {
        var result = await _catalogBrandService.DeleteBrandAsync(request.Id);
        return Ok(new DeleteBrandResponse<bool>() { Id = result });
    }

    [HttpPost]
    [ProducesResponseType(typeof(GetBrandByIdResponse<int?>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetTypeById(GetBrandByIdRequest request)
    {
        var result = await _catalogBrandService.GetBrandById(request.Id);
        return Ok(new GetBrandByIdResponse<CatalogBrandDto>() { Id = result });
    }
}