using Microsoft.EntityFrameworkCore;
using ECommerceApi.Models;
using System.Threading.Tasks;

public class CartService
{
    private readonly ApplicationDbContext _context;

    public CartService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Cart> GetCartByUserIdAsync(string userId)
    {
        return await _context.Carts.Include(c => c.CartItems)
                                   .FirstOrDefaultAsync(c => c.UserId == userId);
    }

    public async Task AddItemToCartAsync(string userId, CartItem cartItem)
    {
        var cart = await GetCartByUserIdAsync(userId);

        if (cart == null)
        {
            cart = new Cart { UserId = userId };
            await _context.Carts.AddAsync(cart);
        }

        // Associe o item ao carrinho
        cartItem.CartId = cart.Id;
        cart.CartItems.Add(cartItem);

        await _context.SaveChangesAsync();
    }
}



