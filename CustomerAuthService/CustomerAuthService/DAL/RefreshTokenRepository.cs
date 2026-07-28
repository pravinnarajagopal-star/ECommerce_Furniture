using CustomerAuthService.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomerAuthService.DAL;

public class RefreshTokenRepository
{
    private readonly AppDbContext _context;

    public RefreshTokenRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task Add(RefreshToken token)
    {
        await _context.RefreshTokens.AddAsync(token);
        await _context.SaveChangesAsync();
    }

    public async Task<RefreshToken?> Get(string token)
    {
        return await _context.RefreshTokens.FirstOrDefaultAsync(x => x.Token == token && !x.IsRevoked);
    }
}