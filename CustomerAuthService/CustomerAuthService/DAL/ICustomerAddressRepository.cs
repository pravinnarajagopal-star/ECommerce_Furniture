using CustomerAuthService.Models;

namespace CustomerAuthService.DAL
{
    public interface ICustomerAddressRepository
    {
        Task<IEnumerable<CustomerAddress>> GetAllByCustomerIdAsync(Guid customerId);

        Task<CustomerAddress?> GetByAddressIdAsync(Guid addressId);

        Task AddAsync(CustomerAddress customerAddress);

        void Update(CustomerAddress customerAddress);

        void Delete(CustomerAddress customerAddress);

        Task SaveChangesAsync();
    }
}
