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
        var token = _httpContextAccessor.HttpContext?
      .Request.Headers["Authorization"]
      .FirstOrDefault();

        if (!string.IsNullOrEmpty(token))
        {
            _httpClient.DefaultRequestHeaders.Remove("Authorization");
            _httpClient.DefaultRequestHeaders.Add("Authorization", token);
        }
        Console.WriteLine( $"Calling Product API: {_httpClient.BaseAddress}api/Product/{productId}");
        var response = await _httpClient.GetAsync($"api/Product/{productId}");

        Console.WriteLine($"Product API Response: {response.StatusCode}");

        response.EnsureSuccessStatusCode();
        return await response.Content
            .ReadFromJsonAsync<ProductDto>();
    }

    public async Task<ProductDto?> UpdateProductAsync(ProductDto product)
    {
        var token = _httpContextAccessor.HttpContext?
       .Request.Headers["Authorization"]
       .FirstOrDefault();

        if (!string.IsNullOrEmpty(token))
        {
            _httpClient.DefaultRequestHeaders.Remove("Authorization");
            _httpClient.DefaultRequestHeaders.Add("Authorization", token);
        }

        Console.WriteLine( $"Calling Product API Update: {_httpClient.BaseAddress}api/Product/{product.ProductId}");

        var response = await _httpClient.PutAsJsonAsync("api/Product",product);

        Console.WriteLine(
            $"Product API Update Response: {response.StatusCode}"
        );

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();

            Console.WriteLine($"Status Code: {response.StatusCode}");
            Console.WriteLine($"Error: {error}");

            throw new Exception(error);
        }

        return await response.Content
            .ReadFromJsonAsync<ProductDto>();
    }

}
