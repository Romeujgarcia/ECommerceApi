using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

public class OrderService
{
    private readonly ApplicationDbContext _context;

    public OrderService(ApplicationDbContext context)
    {
        _context = context;
    }

    // Método para obter pedidos de um usuário específico
    public async Task<List<Order>> GetOrdersByUserIdAsync(string userId)
    {
        return await _context.Orders
            .Include(o => o.OrderItems) // Incluir itens do pedido
            .ThenInclude(oi => oi.Product) // Incluir detalhes do produto nos itens
            .Where(o => o.UserId == userId)
            .ToListAsync();
    }

    // Método para criar um novo pedido
    public async Task CreateOrderAsync(Order order)
    {
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();
    }

    // Método para adicionar um item ao pedido
    public async Task AddOrderItemAsync(OrderItem orderItem)
    {
        _context.OrderItems.Add(orderItem);
        await _context.SaveChangesAsync();
    }

    // Método para obter itens de um pedido por OrderId
    public async Task<OrderItem> GetOrderItemByIdAsync(int id)
    {
        return await _context.OrderItems
            .Include(oi => oi.Product) // Incluir o produto se necessário
            .FirstOrDefaultAsync(oi => oi.Id == id);
    }
}
