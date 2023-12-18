using PixGuard.Api.Application.Contracts;
using Domain.Entities;
using Domain.DTOs;
using Domain.Contracts;
using PixGuard.Api.Application.Contracts.Mappers;
using System;
using System.Threading.Tasks;
using PixGuard.Api.Application;
using Moq;

namespace PixGuard.Tests.Api.Application.PixService;

[TestFixture]
public class PixAppServiceTests
{
    private Mock<IPixRepository> _mockPixRepository;
    private PixMapper _pixMapper;
    private PixAppService _pixAppService;
    [SetUp]
    public void Setup()
    {
        _mockPixRepository = new Mock<IPixRepository>();
        _pixMapper = new PixMapper(); 
        _pixAppService = new PixAppService(_mockPixRepository.Object, _pixMapper);
    }
    [Test]
    public async Task GetById_ShouldReturnPixDto()
    {
        Guid id = Guid.NewGuid();

        var mockPixRepository = new Mock<IPixRepository>();

  
        var pixMapper = new PixMapper();

        var pixExpected = new Pix();
        
        mockPixRepository.Setup(repo => repo.GetById(id)).ReturnsAsync(pixExpected);

        var pixAppService = new PixAppService(mockPixRepository.Object, pixMapper);
        
        var result = await pixAppService.GetById(id);
        
        Assert.IsNotNull(result);
    }
    
    [Test]
    public async Task GetAll_ShouldReturnListOfPixDto()
    {
        
        var pixList = new List<Pix>(); 
        _mockPixRepository.Setup(repo => repo.GetAll()).ReturnsAsync(pixList);

        
        var result = await _pixAppService.GetAll();

       
        Assert.IsNotNull(result);
        Assert.IsInstanceOf<List<PixDto>>(result);
    }

    [Test]
    public async Task Add_ShouldReturnGuid()
    {
      
        var createDto = new CreatePixDto(); 

 
        var result = await _pixAppService.Add(createDto);

       
        Assert.IsNotNull(result);
        Assert.IsInstanceOf<Guid>(result);
    }

    [Test]
    public async Task GetByValue_ShouldReturnListOfPixDto()
    {
      
        var value = "someValue";
        var pixList = new List<Pix>(); 
        _mockPixRepository.Setup(repo => repo.GetByValue(value)).ReturnsAsync(pixList);
        
        var result = await _pixAppService.GetByValue(value);
        
        Assert.IsNotNull(result);
        Assert.IsInstanceOf<List<PixDto>>(result);
    }
}
