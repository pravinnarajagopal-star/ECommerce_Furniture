using CustomerAuthService.Models;

namespace CustomerAuthService.DAL
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetAllAsync();
        Task<Customer?> GetByIdAsync(int id);
        Task AddAsync(Customer customer);
        Task UpdateAsync(Customer customer);
        Task DeleteAsync(Guid id);

        Task<Customer?> GetByEmailAsync(string email);

        Task Logout(string refreshToken);

        Customer GetUserByRefreshToken(RefreshRequest request);

        Task<bool> UpdateRoleAsync(int customerId, int roleId);
    }
}
