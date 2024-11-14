using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class OrderItemsController : ControllerBase
{
    private readonly OrderService _orderService;

    public OrderItemsController(OrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpPost]
    public async Task<IActionResult> AddOrderItem([FromBody] OrderItemDto orderItemDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState); // Retorna os erros de validação
        }

        // Converte o DTO para o modelo de domínio
        var orderItem = new OrderItem
        {
            ProductId = orderItemDto.ProductId,
            OrderId = orderItemDto.OrderId,
            Quantity = orderItemDto.Quantity
        };

        await _orderService.AddOrderItemAsync(orderItem);
        return CreatedAtAction(nameof(GetOrderItem), new { id = orderItem.Id }, orderItemDto);
    }

    // Exemplo de método para obter um OrderItem por ID
    [HttpGet("{id}")]
    public async Task<ActionResult<OrderItemDto>> GetOrderItem(int id)
    {
        var orderItem = await _orderService.GetOrderItemByIdAsync(id);

        if (orderItem == null)
        {
            return NotFound();
        }

        // Converte o modelo de domínio para DTO
        var orderItemDto = new OrderItemDto
        {
            Id = orderItem.Id,
            ProductId = orderItem.ProductId,
            OrderId = orderItem.OrderId,
            Quantity = orderItem.Quantity
        };

        return Ok(orderItemDto);
    }
}

