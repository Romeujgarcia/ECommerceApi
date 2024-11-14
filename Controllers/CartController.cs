using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/cart")]
public class CartController : ControllerBase
{
    private readonly CartService _cartService;

    public CartController(CartService cartService)
    {
        _cartService = cartService;
    }

    // Endpoint para obter o carrinho do usuário
    [HttpGet("{userId}")]
    public async Task<IActionResult> GetCart(string userId)
    {
        var cart = await _cartService.GetCartByUserIdAsync(userId);
        return Ok(cart);
    }

    [HttpPost("{userId}")]
    public async Task<IActionResult> AddToCart(string userId, [FromBody] CartItemDto cartItemDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var cartItem = new CartItem
        {
            ProductId = cartItemDto.ProductId,
            Quantity = cartItemDto.Quantity
        };

        await _cartService.AddItemToCartAsync(userId, cartItem);
        return Ok();
    }

}


