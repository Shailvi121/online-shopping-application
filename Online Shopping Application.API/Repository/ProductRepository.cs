using Microsoft.EntityFrameworkCore;

namespace Online_Shopping_Application.API.Repository
{
    public class ProductRepository : Repository<Product>, IProduct
    {
        private readonly FammsContext _context;

        public ProductRepository(FammsContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Product> GetProductsAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            return product;
        }

        public async Task<Product> CreateAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }
    }
}
