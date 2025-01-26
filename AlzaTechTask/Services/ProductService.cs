namespace AlzaTechTask.Services;

public class ProductService : IProductService
{
    private readonly ProductsContext _context;

    public ProductService(ProductsContext context)
    {
        _context = context;
    }

    public IEnumerable<Product> GetAllProducts()
    {
        return _context.Products.ToList();
    }

    public (IEnumerable<Product>, int totalItems) GetAllProductsPaginated(int pageNumber, int pageSize)
    {
        var totalItems = _context.Products.Count();
        var products = _context.Products
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();
        return (products, totalItems);
    }

    public Product? GetProductById(int id)
    {
        return _context.Products.Find(id);
    }

    public void UpdateDescription(int id, string description)
    {
        var product = _context.Products.Find(id);
        if (product != null)
        {
            product.Description = description;
            _context.SaveChanges();
        }
    }
}