using System.Threading;
using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Requests;

namespace Catalog.UnitTests.Services;

public class CatalogItemServiceTest
{
    private readonly ICatalogItemService _catalogService;

    private readonly Mock<ICatalogItemRepository> _catalogItemRepository;
    private readonly Mock<Host.Services.Interfaces.IDbContextWrapper<ApplicationDbContext>> _dbContextWrapper;
    private readonly Mock<ILogger<CatalogService>> _logger;

    private readonly CatalogItem _testItem = new CatalogItem()
    {
        Name = "Name",
        Description = "Description",
        Price = 1000,
        AvailableStock = 100,
        CatalogBrandId = 1,
        CatalogTypeId = 1,
        PictureFileName = "1.png",
    };

    public CatalogItemServiceTest()
    {
        _catalogItemRepository = new Mock<ICatalogItemRepository>();
        _dbContextWrapper = new Mock<Host.Services.Interfaces.IDbContextWrapper<ApplicationDbContext>>();
        _logger = new Mock<ILogger<CatalogService>>();

        var dbContextTransaction = new Mock<IDbContextTransaction>();
        _dbContextWrapper.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(dbContextTransaction.Object);

        _catalogService = new CatalogItemService(_dbContextWrapper.Object, _logger.Object, _catalogItemRepository.Object);
    }

    [Fact]
    public async Task AddAsync_Success()
    {
        // arrange
        var testResult = 1;

        _catalogItemRepository.Setup(s => s.Add(
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.IsAny<decimal>(),
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<string>())).ReturnsAsync(testResult);

        // act
        var result = await _catalogService.Add(_testItem.Name, _testItem.Description, _testItem.Price, _testItem.AvailableStock, _testItem.CatalogBrandId, _testItem.CatalogTypeId, _testItem.PictureFileName);

        // assert
        result.Should().Be(testResult);
    }

    [Fact]
    public async Task AddAsync_Failed()
    {
        // arrange
        int? testResult = null;

        _catalogItemRepository.Setup(s => s.Add(
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.IsAny<decimal>(),
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<string>())).ReturnsAsync(testResult);

        // act
        var result = await _catalogService.Add(_testItem.Name, _testItem.Description, _testItem.Price, _testItem.AvailableStock, _testItem.CatalogBrandId, _testItem.CatalogTypeId, _testItem.PictureFileName);

        // assert
        result.Should().Be(testResult);
    }

    [Fact]
    public async Task UpdateAsync_Success()
    {
        // arrange
        var testResult = new CatalogItem();

        _catalogItemRepository.Setup(s => s.UpdateItemAsync(
            It.IsAny<CatalogItem>())).ReturnsAsync(testResult);

        // act
        var result = await _catalogService.UpdateItemAsync(new UpdateItemRequest());

        // assert
        result.Should().NotBe(null);
    }

    [Fact]
    public async Task UpdateAsync_Failed()
    {
        // arrange
        CatalogItem? testResult = null;

        _catalogItemRepository.Setup(s => s.UpdateItemAsync(
            It.IsAny<CatalogItem>())) !.ReturnsAsync(testResult);

        // act
        var result = await _catalogService.UpdateItemAsync(new UpdateItemRequest());

        // assert
        result.Should().Be(null);
    }

    [Fact]
    public async Task DeleteAsync_Success()
    {
        // arrange
        var testResult = true;

        _catalogItemRepository.Setup(s => s.DeleteItemAsync(
            It.IsAny<int>())).ReturnsAsync(testResult);

        // act
        var result = await _catalogService.DeleteItemAsync(_testItem.Id);

        // assert
        result.Should().BeTrue();
    }

    [Fact]
    public async Task DeleteAsync_Failed()
    {
        // arrange
        var testResult = false;

        _catalogItemRepository.Setup(s => s.DeleteItemAsync(
            It.IsAny<int>())).ReturnsAsync(testResult);

        // act
        var result = await _catalogService.DeleteItemAsync(_testItem.Id);

        // assert
        result.Should().BeFalse();
    }

    [Fact]
    public async Task GetByIdAsync_Success()
    {
        // arrange
        var testResult = new CatalogItem();

        _catalogItemRepository.Setup(s => s.GetById(
            It.IsAny<int>())).ReturnsAsync(testResult);

        // act
        var result = await _catalogService.GetItemById(_testItem.Id);

        // assert
        result.Should().Be(testResult);
    }

    [Fact]
    public async Task GetByIdAsync_Failed()
    {
        // arrange
        var testResult = new CatalogItem();

        _catalogItemRepository.Setup(s => s.GetById(
            It.IsAny<int>())).ReturnsAsync(testResult);

        // act
        var result = await _catalogService.GetItemById(_testItem.Id);

        // assert
        result.Should().Be(null);
    }

    [Fact]
    public async Task GetTypeAsync_Success()
    {
        // arrange
        var testResult = new CatalogItem();

        _catalogItemRepository.Setup(s => s.GetType(
            It.IsAny<int>())).ReturnsAsync(testResult);

        // act
        var result = await _catalogService.GetType(_testItem.CatalogTypeId);

        // assert
        result.Should().Be(testResult.CatalogTypeId);
    }

    [Fact]
    public async Task GetTypeAsync_Failed()
    {
        // arrange
        var testResult = new CatalogItem();

        _catalogItemRepository.Setup(s => s.GetType(
            It.IsAny<int>())).ReturnsAsync(testResult);

        // act
        var result = await _catalogService.GetType(_testItem.CatalogTypeId);

        // assert
        result.Should().Be(null);
    }

    [Fact]
    public async Task GetBrandAsync_Success()
    {
        // arrange
        var testResult = new CatalogItem();

        _catalogItemRepository.Setup(s => s.GetBrand(
            It.IsAny<int>())).ReturnsAsync(testResult);

        // act
        var result = await _catalogService.GetBrand(_testItem.CatalogBrandId);

        // assert
        result.Should().Be(testResult.CatalogBrandId);
    }

    [Fact]
    public async Task GetBrandAsync_Failed()
    {
        // arrange
        var testResult = new CatalogItem();

        _catalogItemRepository.Setup(s => s.GetBrand(
            It.IsAny<int>())).ReturnsAsync(testResult);

        // act
        var result = await _catalogService.GetBrand(_testItem.CatalogBrandId);

        // assert
        result.Should().Be(null);
    }
}