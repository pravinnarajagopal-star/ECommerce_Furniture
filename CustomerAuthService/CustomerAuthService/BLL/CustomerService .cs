using Azure.Core;
using CustomerAuthService.BLL;
using CustomerAuthService.DAL;
using CustomerAuthService.Models;
using Microsoft.EntityFrameworkCore;


namespace CustomerAuthService.BLL;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _repo;

    public CustomerService(ICustomerRepository repo)
    {
        _repo = repo;
    }

    public Task<IEnumerable<Customer>> GetCustomers()
        => _repo.GetAllAsync();

    public Task<Customer?> GetCustomer(int  id)
        => _repo.GetByIdAsync(id);

    public Task CreateCustomer(Customer customer)
    {
        customer.CreatedDate = DateTime.UtcNow;
        customer.CreatedBy = customer.FirstName;
        var existingCustomer =  _repo.GetByEmailAsync(customer.Email); 
        //if (existingCustomer != null) 
        //{ 
        //    throw new Exception("Email already exists");
        //}
        customer.PasswordHash = BCrypt.Net.BCrypt.HashPassword(customer.PasswordHash);

        return _repo.AddAsync(customer);
    }

    public Task UpdateCustomer(Customer customer)
        => _repo.UpdateAsync(customer);

    public Task DeleteCustomer(Guid id)
        => _repo.DeleteAsync(id);

    

    public Task<Customer?> GetByEmail(string email)
       => _repo.GetByEmailAsync(email);

    public Customer GetUserByRefreshToken(RefreshRequest request)
    {
        return _repo.GetUserByRefreshToken(request);
    }


    public async Task<bool> UpdateRoleAsync(int customerId, int roleId)
    {
        return await _repo.UpdateRoleAsync(customerId, roleId);
    }



}