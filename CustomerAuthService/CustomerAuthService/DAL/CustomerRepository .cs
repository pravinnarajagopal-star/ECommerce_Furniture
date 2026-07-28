using Azure.Core;
using CustomerAuthService.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomerAuthService.DAL;

public class CustomerRepository : ICustomerRepository
{
    private readonly AppDbContext _context;

    public CustomerRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Customer>> GetAllAsync()
        => await _context.Customers.ToListAsync();

    public async Task<Customer?> GetByIdAsync(int id)
        => await _context.Customers
        .SingleOrDefaultAsync(c => c.CustomerId == id);

    public async Task<Customer?> GetByEmailAsync(string email)
       => await _context.Customers
        .SingleOrDefaultAsync(c => c.Email == email);

    public async Task AddAsync(Customer customer)
    {
        await _context.Customers.AddAsync(customer);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Customer customer)
    {
        _context.Customers.Update(customer);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var customer = await _context.Customers.FindAsync(id);
        if (customer != null)
        {
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
        }
    }


    // LOGOUT
    public async Task Logout(string refreshToken)
    {
        var token = await _context.RefreshTokens
            .FirstOrDefaultAsync(x => x.Token == refreshToken);

        if (token != null)
        {
            token.IsRevoked = true;
            await _context.SaveChangesAsync();
        }
    }


    public async Task<bool> UpdateRoleAsync(int customerId, int roleId)
    {
        var customer = await _context.Customers.FindAsync(customerId);

        if (customer == null)
            return false;

        customer.RoleId = roleId;

        await _context.SaveChangesAsync();

        return true;
    }


    public async Task AddRefrehTokenAsync(RefreshToken reftoken)
    {
        await _context.RefreshTokens.AddAsync(reftoken);
        await _context.SaveChangesAsync();
    }

    public  Customer GetUserByRefreshToken(RefreshRequest request)
    { 

       var refTokenData=   _context.RefreshTokens
        .FirstOrDefaultAsync(r => r.Token == request.RefreshToken);

        Customer customer = _context.Customers.FirstOrDefault(x => x.CustomerId == refTokenData.Result.UserId);

        return customer;
    }





}
