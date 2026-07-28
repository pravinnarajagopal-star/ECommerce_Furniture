using OrderService.Models;

namespace OrderService.UserService
{
    public interface IUserServiceClient
    {

        Task<UserDto?> GetUserAsync(int userId);
    }
}
