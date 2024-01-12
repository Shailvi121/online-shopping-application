

namespace Online_Shopping_Application.API.Repository.Interface
{
    public interface IProduct: IRepository<Product>
    {
        Task<Product> GetProductsAsync(int id);
        Task<Product> CreateAsync(Product product);
    }
}
