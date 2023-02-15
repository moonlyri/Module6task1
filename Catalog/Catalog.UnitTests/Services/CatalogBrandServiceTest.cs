using System.Threading;
using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Requests;
using Catalog.Host.Models.Requests.Brand;
using Catalog.Host.Models.Requests.Type;

namespace Catalog.UnitTests.Services;

public class CatalogBrandServiceTest
{
    private readonly ICatalogBrandService _catalogBrandService;

    private readonly Mock<ICatalogBrandRepository> _catalogBrandRepository;
    private readonly Mock<Host.Services.Interfaces.IDbContextWrapper<ApplicationDbContext>> _dbContextWrapper;
    private readonly Mock<ILogger<CatalogService>> _logger;
    private readonly Mock<IMapper> _mapper;

    private readonly CatalogBrand _testBrandSuccess = new CatalogBrand()
    {
        Brand = "Brand",
        Description = "Description",
        Id = 1
    };
    private readonly CatalogBrandDto _testItemDtoSuccess = new CatalogBrandDto()
    {
        Brand = "Brand",
        Description = "Test",
        Id = 1
    };

    public CatalogBrandServiceTest()
    {
        _catalogBrandRepository = new Mock<ICatalogBrandRepository>();
        _dbContextWrapper = new Mock<Host.Services.Interfaces.IDbContextWrapper<ApplicationDbContext>>();
        _logger = new Mock<ILogger<CatalogService>>();
        _mapper = new Mock<IMapper>();

        var dbContextTransaction = new Mock<IDbContextTransaction>();
        _dbContextWrapper.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(dbContextTransaction.Object);

        _catalogBrandService = new CatalogBrandService(_dbContextWrapper.Object, _logger.Object, _mapper.Object, _catalogBrandRepository.Object);
    }

    [Fact]
    public async Task AddAsync_Success()
    {
        // arrange
        var testResult = 1;

        _catalogBrandRepository.Setup(s => s.Add(
            It.IsAny<string>(),
            It.IsAny<string>())).ReturnsAsync(testResult);

        // act
        var result = await _catalogBrandService.Add(_testBrandSuccess.Brand, _testBrandSuccess.Description);

        // assert
        result.Should().Be(testResult);
    }

    [Fact]
    public async Task AddAsync_Failed()
    {
        // arrange
        int? testResult = null;

        _catalogBrandRepository.Setup(s => s.Add(
            It.IsAny<string>(),
            It.IsAny<string>())).ReturnsAsync(testResult);

        // act
        var result = await _catalogBrandService.Add(_testBrandSuccess.Brand, _testBrandSuccess.Description);

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
        var result = await _catalogBrandService.UpdateBrandAsync(new UpdateBrandRequest());

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
        var result = await _catalogBrandService.UpdateBrandAsync(new UpdateBrandRequest());

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
        var result = await _catalogBrandService.DeleteBrandAsync(_testBrandSuccess.Id);

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
        var result = await _catalogBrandService.DeleteBrandAsync(_testBrandSuccess.Id);

        // assert
        result.Should().BeFalse();
    }

    [Fact]
    public async Task GetByIdAsync_Success()
    {
        // arrange
        // arrange
        _catalogBrandRepository.Setup(s => s.GetById(It.IsAny<int>())).ReturnsAsync(_testBrandSuccess);
        _mapper.Setup(s => s.Map<CatalogBrandDto>(It.Is<CatalogBrand>(i => i.Equals(_testBrandSuccess)))).Returns(_testItemDtoSuccess);

        // act
        var result = await _catalogBrandService.GetBrandById(It.IsAny<int>());

        // assert
        result.Should().Be(_testItemDtoSuccess);
    }

    [Fact]
    public async Task GetByIdAsync_Failed()
    {
        // arrange
        var testIdResult = 0;
        var testEmptyItem = new CatalogBrandDto();
        string testBrand = null!;
        CatalogBrand? testNullResult = null!;
        _catalogBrandRepository.Setup(s => s.GetById(It.IsAny<int>())).ReturnsAsync(testNullResult);

        // act
        var result = await _catalogBrandService.GetBrandById(It.IsAny<int>());

        // assert
        result.Should().NotBeNull();
        result?.Id.Should().Be(testIdResult);
        result?.Description.Should().BeNullOrEmpty();
        result?.Brand.Should().Be(testBrand);
    }
}