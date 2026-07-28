using OrderService.DAL;
using OrderService.Models;
using System.Net.Http.Headers;

public class ProductApiClient : IProductApiClient
{
    private readonly HttpClient _httpClient;
    private readonly IHttpContextAccessor _httpContextAccessor;


    public ProductApiClient(
        HttpClient httpClient,
        IHttpContextAccessor httpContextAccessor)
    {
        _httpClient = httpClient;
        _httpContextAccessor = httpContextAccessor;
    }


    public async Task<ProductDto?> GetProductAsync(int productId)
    {
        Console.WriteLine(
            $"Calling Product API: {_httpClient.BaseAddress}api/Product/{productId}"
        );


        var response = await _httpClient.GetAsync(
            $"api/Product/{productId}"
        );


        Console.WriteLine(
            $"Product API Response: {response.StatusCode}"
        );


        response.EnsureSuccessStatusCode();


        return await response.Content
            .ReadFromJsonAsync<ProductDto>();
    }

}
