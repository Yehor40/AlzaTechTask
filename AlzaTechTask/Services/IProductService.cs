namespace AlzaTechTask.Services;

public interface IProductService
{
    IEnumerable<Product> GetAllProducts();
    (IEnumerable<Product>, int totalItems) GetAllProductsPaginated(int pageNumber, int pageSize);
    Product? GetProductById(int id);
    void UpdateDescription(int id, string description);
}