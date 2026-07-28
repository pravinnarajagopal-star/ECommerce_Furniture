using CustomerAuthService.Models;

namespace CustomerAuthService.BLL;

public interface ICustomerService
{
    Task<IEnumerable<Customer>> GetCustomers();
    Task<Customer?> GetCustomer(int id);
    Task CreateCustomer(Customer customer);
    Task UpdateCustomer(Customer customer);
    Task DeleteCustomer(Guid id);
    Customer GetUserByRefreshToken(RefreshRequest request);

    Task<bool> UpdateRoleAsync(int customerId, int roleId);
}