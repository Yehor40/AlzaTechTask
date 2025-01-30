using AlzaTechTask.Controllers;
using AlzaTechTask.Data;
using AlzaTechTask.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace AlzaTechTaskTests;

public class ProductControllerTests
{
    private readonly Mock<IProductService> _mockService;
    private readonly ProductsController _controller;

    public ProductControllerTests()
    {
        _mockService = new Mock<IProductService>();
        _controller = new ProductsController(_mockService.Object);
    }
    //get all products test
    [Fact]
    public void GetAllProductsTest()
    {
        var mockProducts = new List<Product>
        {
            new Product { Id = 1, Name = "I", Description = "can start working from", Price = 302 ,ImgUri = "/qwerty"},
            new Product { Id = 2, Name = "I", Description = "prefer hybrid regime", Price = 100 ,ImgUri="/dg"},
            new Product { Id = 3, Name = "I", Description = "would like to have minimum gross pay", Price = 65000 ,ImgUri = "/xxc"}

        };
        _mockService.Setup(s => s.GetAllProducts()).Returns(mockProducts);

        var result = _controller.GetAllProducts();

        var okResult = Assert.IsType<OkObjectResult>(result);
        var products = Assert.IsType<List<Product>>(okResult.Value);
        Assert.Equal(3, products.Count);
    }
    //product got by id exists test
    [Fact]
    public void GetProductByIdExists()
    {
        var product = new Product { Id = 1, Name = "I", Description = "can start working from", Price = 302 ,ImgUri = "/kk"};
        _mockService.Setup(s => s.GetProductById(1)).Returns(product);

        var result = _controller.GetProductById(1);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedProduct = Assert.IsType<Product>(okResult.Value);
        Assert.Equal(1, returnedProduct.Id);
        
    }
    //product got by id not exists test
    [Fact]
    public void GetProductByIdNotExist()
    {
        _mockService.Setup(s => s.GetProductById(99)).Returns((Product)null);

        var result = _controller.GetProductById(99);

        Assert.IsType<NotFoundResult>(result);
    }
    //update description test
    [Fact]
    public void UpdateProductDescription()
    {
        var productId = 1;
        var newDescription = "why do people leave your company?";

        var result = _controller.UpdateProductDescription(productId, newDescription);

        Assert.IsType<OkResult>(result);
    }
}
