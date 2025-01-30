using AlzaTechTask.Controllers;
using AlzaTechTask.Data;
using AlzaTechTask.Services;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace AlzaTechTaskTests;

public class ProductControllerRealDBTests:IDisposable
{
    private readonly IProductService _service;
    private readonly ProductsController _controller;
    private readonly ProductsContext _context;

    public ProductControllerRealDBTests()
    {
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        var options = new DbContextOptionsBuilder<ProductsContext>()
            .UseMySQL(config.GetConnectionString("Database"))
            .Options;
        
        _context = new ProductsContext(options);

        _context.Database.EnsureCreated(); 
        _service = new ProductService(_context);
        _controller = new ProductsController(_service);
        
    }
    //get all products paginated test
    [Fact]
    public void GetAllProductsTestRealDB()
    {
        var result = _controller.GetAllProducts();
        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var products = Assert.IsType<List<Product>>(okResult.Value);
        Assert.Equal(10,products.Count());
    }
    // get product by id test
    [Fact]
    public void GetProductById()
    {
        var result = _controller.GetProductById(1);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedProduct = Assert.IsType<Product>(okResult.Value);
        Assert.Equal(1, returnedProduct.Id);
    }
    //product got by id not exists test
    [Fact]
    public void GetProductByIdNotExist()
    {
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
    
    public void Dispose()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }
}