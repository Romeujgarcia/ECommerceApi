using ECommerceApi.Models;
using Microsoft.EntityFrameworkCore;

public class RecommendationService
{
    private readonly ApplicationDbContext _context;

    public RecommendationService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Product>> GetRecommendationsAsync(string  userId)
    {
        var orders = await _context.Orders
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.Product) // Certifique-se de que a propriedade Product exista em OrderItem
            .Where(o => o.UserId == userId)
            .SelectMany(o => o.OrderItems)
            .GroupBy(oi => oi.ProductId)
            .OrderByDescending(g => g.Count())
            .Select(g => g.First().Product)
            .ToListAsync(); // Use ToListAsync para operações assíncronas

        return orders.Where(p => p != null).ToList();
    }
}
