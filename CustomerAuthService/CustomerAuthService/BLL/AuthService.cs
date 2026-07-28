using Azure.Core;
using CustomerAuthService.DAL;
using CustomerAuthService.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace CustomerAuthService.BLL;

public class AuthService
{
    private readonly CustomerRepository _customerRepo;
    private readonly RefreshTokenRepository _tokenRepo;
    private readonly JwtService _jwt;

    public AuthService(CustomerRepository customerRepo,
                       RefreshTokenRepository tokenRepo,
                       JwtService jwt)
    {
        _customerRepo = customerRepo;
        _tokenRepo = tokenRepo;
        _jwt = jwt;
    }

    public async Task Register(string name,  string lastname, string email, string password)
    {
        var hash = Hash(password);

        var customer = new Customer
        {
            FirstName = name,
            LastName=lastname,
            Email = email,
            PasswordHash = hash,
            MobileNumber="9003100445",
            CreatedDate = DateTime.UtcNow
        };

        await _customerRepo.AddAsync(customer);
    }

    public async Task<(bool success, string message, string? jwt, string? refreshToken, Customer? user)> Login(
    string email,
    string password)
    {
        var user = await _customerRepo.GetByEmailAsync(email);

        if (user == null)
        {
            return (
                false,
                "Oops! We couldn't find an account with that email.",
                null,
                null,
                null
            );
        }


        bool isPasswordValid =
            BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);


        if (!isPasswordValid)
        {
            return (
                false,
                "Invalid username or password",
                null,
                null,
                null
            );
        }


        var jwt = _jwt.GenerateToken(user);

        var refreshToken = GenerateRefreshToken();


        var token = new RefreshToken
        {
            Token = refreshToken,
            UserId = user.CustomerId,
            CreatedDate = DateTime.UtcNow,
            IsRevoked = false,
            ExpiryDate = DateTime.UtcNow.AddDays(7)
        };


        await _customerRepo.AddRefrehTokenAsync(token);


        return (
            true,
            "Login successful",
            jwt,
            refreshToken,
            user
        );
    }


    public string GenerateRefreshToken()
    {
        return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
    }

    public string Hash(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }


    public async Task SaveRefreshToken(string refreshToken)
    {
        
    }
    // LOGOUT
    public async Task Logout(string refreshToken)
         => await _customerRepo.Logout(refreshToken);   





}