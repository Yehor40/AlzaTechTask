using Microsoft.EntityFrameworkCore;

namespace AlzaTechTask.Data;
public class ProductsContext:DbContext{
    public ProductsContext(DbContextOptions<ProductsContext> options) : base(options)
    {
    }
    public DbSet<Product>Products { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>().HasData(
            new Product { Id = 1, Name = "Product A", ImgUri = "/images/productA.jpg", Price = 99.99m, Description = "qwertyu"},
            new Product { Id = 2, Name = "Product B", ImgUri = "/images/productB.jpg", Price = 199.99m, Description = "dfghjk"},
            new Product { Id = 3, Name = "Product AB", ImgUri = "/images/productAB.jpg", Price = 195.99m, Description = "fghjkl"},
            new Product { Id = 4, Name = "Product BA", ImgUri = "/images/productBA.jpg", Price = 198.99m , Description = "ertyuik"},
            new Product { Id = 5, Name = "Product AA", ImgUri = "/images/productAA.jpg", Price = 167.99m , Description = "ertyuffdk"},
            new Product { Id = 6, Name = "Product BB", ImgUri = "/images/productBB.jpg", Price = 139.99m , Description = "ertyccxsuik"}
        );
       
    }
    
}