using OrderService.Models;
using System.Net.Http.Json;
namespace OrderService.UserService
{
    public class UserServiceClient: IUserServiceClient
    {
        private readonly HttpClient _httpClient;

        public UserServiceClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<UserDto?> GetUserAsync(int userId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/Customers/{userId}");

                if (!response.IsSuccessStatusCode)
                    return null;

                return await response.Content.ReadFromJsonAsync<UserDto>();
            }
            catch (Exception)
            {
                // Log exception if needed
                return null;
            }
        }
    }
}
