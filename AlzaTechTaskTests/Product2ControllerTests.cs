using AlzaTechTask.Controllers;
using AlzaTechTask.Data;
using AlzaTechTask.Services;
using Moq;
using Microsoft.AspNetCore.Mvc;

namespace AlzaTechTaskTests;

public class Product2ControllerTests
{
    private readonly Mock<IProductService> _mockService;
    private readonly Products2Controller _controller;

    public Product2ControllerTests()
    {
        _mockService = new Mock<IProductService>();
        _controller = new Products2Controller(_mockService.Object);
    }
 //get all products paginated test
    [Fact]
    public void GetAllProductsPaginatedTest()
    {
        int page =1;
        int pageSize = 1;
        var mockProducts = new List<Product>
        {
            new Product { Id = 1, Name = "I", Description = "can start working from", Price = 302,ImgUri = "/ddd"},
            new Product { Id = 2, Name = "I", Description = "prefer hybrid regime", Price = 100 ,ImgUri = "/cc"},
            new Product { Id = 3, Name = "I", Description = "would like to have minimum gross pay", Price = 65000 ,ImgUri = "/ds"}

        };
        var totalItems = mockProducts.Count;
        _mockService.Setup(s => s.GetAllProductsPaginated(page, pageSize))
            .Returns((mockProducts.Take(pageSize),totalItems));
        
        var result = _controller.GetAllProductsPaginated(page, pageSize);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var value = Assert.IsType<ValueTuple<IEnumerable<Product>,int>>(okResult.Value);
        
        Assert.Equal(pageSize,value.Item1.Count());  
        Assert.Equal(totalItems, value.Item2); 
    }
}