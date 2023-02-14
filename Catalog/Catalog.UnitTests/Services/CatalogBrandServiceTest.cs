using System.Threading;
using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Requests;
using Catalog.Host.Models.Requests.Brand;
using Catalog.Host.Models.Requests.Type;

namespace Catalog.UnitTests.Services;

public class CatalogBrandServiceTest
{
    private readonly ICatalogBrandService _catalogService;

    private readonly Mock<ICatalogBrandRepository> _catalogBrandRepository;
    private readonly Mock<Host.Services.Interfaces.IDbContextWrapper<ApplicationDbContext>> _dbContextWrapper;
    private readonly Mock<ILogger<CatalogService>> _logger;

    private readonly CatalogBrand _testItem = new CatalogBrand()
    {
        Brand = "Brand",
        Id = 1
    };

    public CatalogBrandServiceTest()
    {
        _catalogBrandRepository = new Mock<ICatalogBrandRepository>();
        _dbContextWrapper = new Mock<Host.Services.Interfaces.IDbContextWrapper<ApplicationDbContext>>();
        _logger = new Mock<ILogger<CatalogService>>();

        var dbContextTransaction = new Mock<IDbContextTransaction>();
        _dbContextWrapper.Setup(s => s.BeginTransactionAsync(CancellationToken.None))
            .ReturnsAsync(dbContextTransaction.Object);

        _catalogService =
            new CatalogBrandService(_dbContextWrapper.Object, _logger.Object, _catalogBrandRepository.Object);
    }

    [Fact]
    public async Task AddAsync_Success()
    {
        // arrange
        var testResult = 1;

        _catalogBrandRepository.Setup(s => s.Add(
            It.IsAny<int>(),
            It.IsAny<string>())).ReturnsAsync(testResult);

        // act
        var result = await _catalogService.Add(_testItem.Id, _testItem.Brand);

        // assert
        result.Should().Be(testResult);
    }

    [Fact]
    public async Task AddAsync_Failed()
    {
        // arrange
        int? testResult = null;

        _catalogBrandRepository.Setup(s => s.Add(
            It.IsAny<int>(),
            It.IsAny<string>())).ReturnsAsync(testResult);

        // act
        var result = await _catalogService.Add(_testItem.Id, _testItem.Brand);

        // assert
        result.Should().Be(testResult);
    }

    [Fact]
    public async Task UpdateAsync_Success()
    {
        // arrange
        var testResult = new CatalogBrand();

        _catalogBrandRepository.Setup(s => s.UpdateBrandAsync(
            It.IsAny<CatalogBrand>())).ReturnsAsync(testResult);

        // act
        var result = await _catalogService.UpdateBrandAsync(new UpdateBrandRequest());

        // assert
        result.Should().NotBe(null);
    }

    [Fact]
    public async Task UpdateAsync_Failed()
    {
        // arrange
        CatalogBrand? testResult = null;

        _catalogBrandRepository.Setup(s => s.UpdateBrandAsync(
            It.IsAny<CatalogBrand>())) !.ReturnsAsync(testResult);

        // act
        var result = await _catalogService.UpdateBrandAsync(new UpdateBrandRequest());

        // assert
        result.Should().Be(null);
    }

    [Fact]
    public async Task DeleteAsync_Success()
    {
        // arrange
        var testResult = true;

        _catalogBrandRepository.Setup(s => s.DeleteBrandAsync(
            It.IsAny<int>())).ReturnsAsync(testResult);

        // act
        var result = await _catalogService.DeleteBrandAsync(_testItem.Id);

        // assert
        result.Should().BeTrue();
    }

    [Fact]
    public async Task DeleteAsync_Failed()
    {
        // arrange
        var testResult = false;

        _catalogBrandRepository.Setup(s => s.DeleteBrandAsync(
            It.IsAny<int>())).ReturnsAsync(testResult);

        // act
        var result = await _catalogService.DeleteBrandAsync(_testItem.Id);

        // assert
        result.Should().BeFalse();
    }

    [Fact]
    public async Task GetByIdAsync_Success()
    {
        // arrange
        var testResult = new CatalogBrand();

        _catalogBrandRepository.Setup(s => s.GetById(
            It.IsAny<int>())).ReturnsAsync(testResult);

        // act
        var result = await _catalogService.GetBrandById(_testItem.Id);

        // assert
        result.Should().Be(testResult);
    }

    [Fact]
    public async Task GetByIdAsync_Failed()
    {
        // arrange
        var testResult = new CatalogBrand();

        _catalogBrandRepository.Setup(s => s.GetById(
            It.IsAny<int>())).ReturnsAsync(testResult);

        // act
        var result = await _catalogService.GetBrandById(_testItem.Id);

        // assert
        result.Should().Be(null);
    }
}