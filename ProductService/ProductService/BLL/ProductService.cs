using ProductService.DAL;
using ProductService.Models;

namespace ProductService.BLL
{
    public class ProductsService : IProductService
    {
        private readonly IProductRepository _repository;

        public ProductsService(IProductRepository repository)
        {
            _repository = repository;
        }

        public Task<List<Product>> GetAllAsync()
            => _repository.GetAllAsync();

        public Task<Product?> GetByIdAsync(int id)
            => _repository.GetByIdAsync(id);

        public Task<Product> AddAsync(Product product)
            => _repository.AddAsync(product);

        public Task<Product> UpdateAsync(Product product)
            => _repository.UpdateAsync(product);

        public Task DeleteAsync(int id)
            => _repository.DeleteAsync(id);
    }
}
