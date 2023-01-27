using System.Threading;
using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Requests;
using Catalog.Host.Models.Requests.Type;

namespace Catalog.UnitTests.Services;

public class CatalogTypeServiceTest
{
    private readonly ICatalogTypeService _catalogService;

    private readonly Mock<ICatalogTypeRepository> _catalogTypeRepository;
    private readonly Mock<Host.Services.Interfaces.IDbContextWrapper<ApplicationDbContext>> _dbContextWrapper;
    private readonly Mock<ILogger<CatalogService>> _logger;

    private readonly CatalogType _testItem = new CatalogType()
    {
        Type = "Type",
        Id = 1
    };

    public CatalogTypeServiceTest()
    {
        _catalogTypeRepository = new Mock<ICatalogTypeRepository>();
        _dbContextWrapper = new Mock<Host.Services.Interfaces.IDbContextWrapper<ApplicationDbContext>>();
        _logger = new Mock<ILogger<CatalogService>>();

        var dbContextTransaction = new Mock<IDbContextTransaction>();
        _dbContextWrapper.Setup(s => s.BeginTransactionAsync(CancellationToken.None))
            .ReturnsAsync(dbContextTransaction.Object);

        _catalogService =
            new CatalogTypeService(_dbContextWrapper.Object, _logger.Object, _catalogTypeRepository.Object);
    }

    [Fact]
    public async Task AddAsync_Success()
    {
        // arrange
        var testResult = 1;

        _catalogTypeRepository.Setup(s => s.Add(
            It.IsAny<int>(),
            It.IsAny<string>())).ReturnsAsync(testResult);

        // act
        var result = await _catalogService.Add(_testItem.Id, _testItem.Type);

        // assert
        result.Should().Be(testResult);
    }

    [Fact]
    public async Task AddAsync_Failed()
    {
        // arrange
        int? testResult = null;

        _catalogTypeRepository.Setup(s => s.Add(
            It.IsAny<int>(),
            It.IsAny<string>())).ReturnsAsync(testResult);

        // act
        var result = await _catalogService.Add(_testItem.Id, _testItem.Type);

        // assert
        result.Should().Be(testResult);
    }

    [Fact]
    public async Task UpdateAsync_Success()
    {
        // arrange
        var testResult = new CatalogType();

        _catalogTypeRepository.Setup(s => s.UpdateTypeAsync(
            It.IsAny<CatalogType>())).ReturnsAsync(testResult);

        // act
        var result = await _catalogService.UpdateTypeAsync(new UpdateTypeRequest());

        // assert
        result.Should().NotBe(null);
    }

    [Fact]
    public async Task UpdateAsync_Failed()
    {
        // arrange
        CatalogType? testResult = null;

        _catalogTypeRepository.Setup(s => s.UpdateTypeAsync(
            It.IsAny<CatalogType>())) !.ReturnsAsync(testResult);

        // act
        var result = await _catalogService.UpdateTypeAsync(new UpdateTypeRequest());

        // assert
        result.Should().Be(null);
    }

    [Fact]
    public async Task DeleteAsync_Success()
    {
        // arrange
        var testResult = true;

        _catalogTypeRepository.Setup(s => s.DeleteTypeAsync(
            It.IsAny<int>())).ReturnsAsync(testResult);

        // act
        var result = await _catalogService.DeleteTypeAsync(_testItem.Id);

        // assert
        result.Should().BeTrue();
    }

    [Fact]
    public async Task DeleteAsync_Failed()
    {
        // arrange
        var testResult = false;

        _catalogTypeRepository.Setup(s => s.DeleteTypeAsync(
            It.IsAny<int>())).ReturnsAsync(testResult);

        // act
        var result = await _catalogService.DeleteTypeAsync(_testItem.Id);

        // assert
        result.Should().BeFalse();
    }

    [Fact]
    public async Task GetByIdAsync_Success()
    {
        // arrange
        var testResult = new CatalogType();

        _catalogTypeRepository.Setup(s => s.GetById(
            It.IsAny<int>())).ReturnsAsync(testResult);

        // act
        var result = await _catalogService.GetTypeById(_testItem.Id);

        // assert
        result.Should().Be(testResult);
    }

    [Fact]
    public async Task GetByIdAsync_Failed()
    {
        // arrange
        var testResult = new CatalogType();

        _catalogTypeRepository.Setup(s => s.GetById(
            It.IsAny<int>())).ReturnsAsync(testResult);

        // act
        var result = await _catalogService.GetTypeById(_testItem.Id);

        // assert
        result.Should().Be(null);
    }
}