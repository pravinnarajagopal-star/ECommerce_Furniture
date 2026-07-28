using CustomerAuthService.DAL;
using CustomerAuthService.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomerAuthService.DAL
{
    public class CustomerAddressRepository : ICustomerAddressRepository
    {
        private readonly AppDbContext _context;

        public CustomerAddressRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CustomerAddress>> GetAllByCustomerIdAsync(Guid customerId)
        {
            return await _context.CustomerAddresses
                                 .Where(x => x.CustomerId == customerId)
                                 .OrderByDescending(x => x.CreatedDate)
                                 .ToListAsync();
        }

        public async Task<CustomerAddress?> GetByAddressIdAsync(Guid addressId)
        {
            return await _context.CustomerAddresses
                                 .FirstOrDefaultAsync(x => x.AddressId == addressId);
        }

        public async Task AddAsync(CustomerAddress customerAddress)
        {
            await _context.CustomerAddresses.AddAsync(customerAddress);
        }

        public void Update(CustomerAddress customerAddress)
        {
            _context.CustomerAddresses.Update(customerAddress);
        }

        public void Delete(CustomerAddress customerAddress)
        {
            _context.CustomerAddresses.Remove(customerAddress);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}