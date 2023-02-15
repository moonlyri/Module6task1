using System.Threading;
using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Requests;
using Catalog.Host.Models.Requests.Brand;
using Catalog.Host.Models.Requests.Type;

namespace Catalog.UnitTests.Services;

public class CatalogTypeServiceTest
{
    private readonly ICatalogTypeService _catalogTypeService;

    private readonly Mock<ICatalogTypeRepository> _catalogTypeRepository;
    private readonly Mock<Host.Services.Interfaces.IDbContextWrapper<ApplicationDbContext>> _dbContextWrapper;
    private readonly Mock<ILogger<CatalogService>> _logger;
    private readonly Mock<IMapper> _mapper;

    private readonly CatalogType _testTypeSuccess = new CatalogType()
    {
        Type = "Type",
        Description = "Description",
        Id = 1
    };
    private readonly CatalogTypeDto _testItemDtoSuccess = new CatalogTypeDto()
    {
        Type = "Type",
        Description = "Test",
        Id = 1
    };

    public CatalogTypeServiceTest()
    {
        _catalogTypeRepository = new Mock<ICatalogTypeRepository>();
        _dbContextWrapper = new Mock<Host.Services.Interfaces.IDbContextWrapper<ApplicationDbContext>>();
        _logger = new Mock<ILogger<CatalogService>>();
        _mapper = new Mock<IMapper>();

        var dbContextTransaction = new Mock<IDbContextTransaction>();
        _dbContextWrapper.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(dbContextTransaction.Object);

        _catalogTypeService = new CatalogTypeService(_dbContextWrapper.Object, _logger.Object, _mapper.Object, _catalogTypeRepository.Object);
    }

    [Fact]
    public async Task AddAsync_Success()
    {
        // arrange
        var testResult = 1;

        _catalogTypeRepository.Setup(s => s.Add(
            It.IsAny<string>(),
            It.IsAny<string>())).ReturnsAsync(testResult);

        // act
        var result = await _catalogTypeService.Add(_testTypeSuccess.Type, _testTypeSuccess.Description);

        // assert
        result.Should().Be(testResult);
    }

    [Fact]
    public async Task AddAsync_Failed()
    {
        // arrange
        int? testResult = null;

        _catalogTypeRepository.Setup(s => s.Add(
            It.IsAny<string>(),
            It.IsAny<string>())).ReturnsAsync(testResult);

        // act
        var result = await _catalogTypeService.Add(_testTypeSuccess.Type, _testTypeSuccess.Description);

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
        var result = await _catalogTypeService.UpdateTypeAsync(new UpdateTypeRequest());

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
        var result = await _catalogTypeService.UpdateTypeAsync(new UpdateTypeRequest());

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
        var result = await _catalogTypeService.DeleteTypeAsync(_testTypeSuccess.Id);

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
        var result = await _catalogTypeService.DeleteTypeAsync(_testTypeSuccess.Id);

        // assert
        result.Should().BeFalse();
    }

    [Fact]
    public async Task GetByIdAsync_Success()
    {
        // arrange
        // arrange
        _catalogTypeRepository.Setup(s => s.GetById(It.IsAny<int>())).ReturnsAsync(_testTypeSuccess);
        _mapper.Setup(s => s.Map<CatalogTypeDto>(It.Is<CatalogType>(i => i.Equals(_testTypeSuccess)))).Returns(_testItemDtoSuccess);

        // act
        var result = await _catalogTypeService.GetTypeById(It.IsAny<int>());

        // assert
        result.Should().Be(_testItemDtoSuccess);
    }

    [Fact]
    public async Task GetByIdAsync_Failed()
    {
        // arrange
        var testIdResult = 0;
        var testEmptyItem = new CatalogTypeDto();
        string testBrand = null!;
        CatalogType? testNullResult = null!;
        _catalogTypeRepository.Setup(s => s.GetById(It.IsAny<int>())).ReturnsAsync(testNullResult);

        // act
        var result = await _catalogTypeService.GetTypeById(It.IsAny<int>());

        // assert
        result.Should().NotBeNull();
        result?.Id.Should().Be(testIdResult);
        result?.Description.Should().BeNullOrEmpty();
        result?.Type.Should().Be(testBrand);
    }
}