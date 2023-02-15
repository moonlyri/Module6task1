using System.Threading;
using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Enums;
using Catalog.Host.Models.Requests;

namespace Catalog.UnitTests.Services;

public class CatalogItemServiceTest
{
    private readonly ICatalogItemService _catalogService;

    private readonly Mock<ICatalogItemRepository> _catalogItemRepository;
    private readonly Mock<Host.Services.Interfaces.IDbContextWrapper<ApplicationDbContext>> _dbContextWrapper;
    private readonly Mock<ILogger<CatalogService>> _logger;
    private readonly Mock<IMapper> _mapper;

    private readonly CatalogItem _testItemSuccess = new CatalogItem()
    {
        Name = "Name",
        Description = "Description",
        Price = 1000,
        AvailableStock = 100,
        CatalogBrandId = 1,
        CatalogTypeId = 1,
        PictureFileName = "1.png",
    };
    private readonly CatalogItemDto _testItemDtoSuccess = new CatalogItemDto()
    {
        Name = "Test",
        Description = "Test",
        PictureUrl = "Test",
        AvailableStock = 1,
        Price = 50m
    };

    public CatalogItemServiceTest()
    {
        _catalogItemRepository = new Mock<ICatalogItemRepository>();
        _dbContextWrapper = new Mock<Host.Services.Interfaces.IDbContextWrapper<ApplicationDbContext>>();
        _logger = new Mock<ILogger<CatalogService>>();
        _mapper = new Mock<IMapper>();

        var dbContextTransaction = new Mock<IDbContextTransaction>();
        _dbContextWrapper.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(dbContextTransaction.Object);

        _catalogService = new CatalogItemService(_dbContextWrapper.Object, _logger.Object, _mapper.Object, _catalogItemRepository.Object);
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
        var result = await _catalogService.Add(_testItemSuccess.Name, _testItemSuccess.Description, _testItemSuccess.Price, _testItemSuccess.AvailableStock, _testItemSuccess.CatalogBrandId, _testItemSuccess.CatalogTypeId, _testItemSuccess.PictureFileName);

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
        var result = await _catalogService.Add(_testItemSuccess.Name, _testItemSuccess.Description, _testItemSuccess.Price, _testItemSuccess.AvailableStock, _testItemSuccess.CatalogBrandId, _testItemSuccess.CatalogTypeId, _testItemSuccess.PictureFileName);

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
        var result = await _catalogService.DeleteItemAsync(_testItemSuccess.Id);

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
        var result = await _catalogService.DeleteItemAsync(_testItemSuccess.Id);

        // assert
        result.Should().BeFalse();
    }

    [Fact]
    public async Task GetByIdAsync_Success()
    {
        // arrange
        // arrange
        _catalogItemRepository.Setup(s => s.GetById(It.IsAny<int>())).ReturnsAsync(_testItemSuccess);
        _mapper.Setup(s => s.Map<CatalogItemDto>(It.Is<CatalogItem>(i => i.Equals(_testItemSuccess)))).Returns(_testItemDtoSuccess);

        // act
        var result = await _catalogService.GetItemById(It.IsAny<int>());

        // assert
        result.Should().Be(_testItemDtoSuccess);
    }

    [Fact]
    public async Task GetByIdAsync_Failed()
    {
        // arrange
        var testIdResult = 0;
        var testEmptyItem = new CatalogItemDto();
        CatalogItem? testNullResult = null!;
        _catalogItemRepository.Setup(s => s.GetById(It.IsAny<int>())).ReturnsAsync(testNullResult);

        // act
        var result = await _catalogService.GetItemById(It.IsAny<int>());

        // assert
        result.Should().NotBeNull();
        result?.Id.Should().Be(testIdResult);
        result?.Name.Should().BeNullOrEmpty();
        result?.Description.Should().BeNullOrEmpty();
        result?.AvailableStock.Should().Be(testIdResult);
        result?.PictureFileName.Should().BeNullOrEmpty();
        result?.Price.Should().Be((decimal)testIdResult);
    }

    [Fact]
    public async Task GetTypeAsync_Success()
    {
        // arrange
        var testPageIndex = 0;
        var testPageSize = 6;
        var testTypeId = 1;
        var testTotal = 1;
        var testType = "Type";
        var testItem = new PaginatedItems<CatalogItem>()
        {
            Data = new List<CatalogItem>()
            {
                new CatalogItem()
                {
                    CatalogTypeId = testTypeId
                }
            },
            TotalCount = testTotal
        };
        var filter = new Dictionary<CatalogTypeFilter, int>();
        filter.Add(CatalogTypeFilter.Type, 1);

        _catalogItemRepository.Setup(s => s.GetType(testType, testPageIndex, testPageSize)).ReturnsAsync(testItem);
        _mapper.Setup(s => s.Map<CatalogItemDto>(It.Is<CatalogItem>(i => i.Equals(_testItemSuccess)))).Returns(_testItemDtoSuccess);

        // act
        var result = await _catalogService.GetType(testType, testPageIndex, testPageSize);

        // assert
        result.Should().NotBeNull();
        result?.Data.Should().NotBeNull();
        result?.TotalCount.Should().Be(testTotal);
    }

    [Fact]
    public async Task GetTypeAsync_Failed()
    {
        // arrange
        var testPageIndex = 100;
        var testPageSize = 500;

        _catalogItemRepository.Setup(s => s.GetType(It.IsAny<string>(), testPageIndex, testPageSize)).ReturnsAsync((Func<PaginatedItems<CatalogItem>>)null!);

        // act
        var result = await _catalogService.GetType(It.IsAny<string>(), testPageIndex, testPageSize);

        // assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetBrandAsync_Success()
    {
        // arrange
        var testPageIndex = 0;
        var testPageSize = 6;
        var testBrandId = 1;
        var testTotal = 1;
        var testBrand = "Brand";
        var testItem = new PaginatedItems<CatalogItem>()
        {
            Data = new List<CatalogItem>()
            {
                new CatalogItem()
                {
                    CatalogBrandId = testBrandId
                }
            },
            TotalCount = testTotal
        };
        var filter = new Dictionary<CatalogTypeFilter, int>();
        filter.Add(CatalogTypeFilter.Brand, 1);

        _catalogItemRepository.Setup(s => s.GetBrand(testBrand, testPageIndex, testPageSize)).ReturnsAsync(testItem);
        _mapper.Setup(s => s.Map<CatalogItemDto>(It.Is<CatalogItem>(i => i.Equals(_testItemSuccess)))).Returns(_testItemDtoSuccess);

        // act
        var result = await _catalogService.GetBrand(testBrand, testPageIndex, testPageSize);

        // assert
        result.Should().NotBeNull();
        result?.Data.Should().NotBeNull();
        result?.TotalCount.Should().Be(testTotal);
    }

    [Fact]
    public async Task GetBrandAsync_Failed()
    {
        // arrange
        var testPageIndex = 100;
        var testPageSize = 500;

        _catalogItemRepository.Setup(s => s.GetBrand(It.IsAny<string>(), testPageIndex, testPageSize)).ReturnsAsync((Func<PaginatedItems<CatalogItem>>)null!);

        // act
        var result = await _catalogService.GetBrand(It.IsAny<string>(), testPageIndex, testPageSize);

        // assert
        result.Should().BeNull();
    }
}