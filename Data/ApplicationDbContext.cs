
using ECommerceApi.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : IdentityDbContext<User>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<CartItem> CartItems { get; set; }
   
   protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    base.OnModelCreating(modelBuilder); // Certifique-se de chamar o método base

   modelBuilder.Entity<Cart>()
        .HasOne<User>()
        .WithMany(u => u.Carts)
        .HasForeignKey(c => c.UserId);

    // Relação entre User e Order
    modelBuilder.Entity<Order>()
        .HasOne<User>()
        .WithMany(u => u.Orders)
        .HasForeignKey(o => o.UserId);

    // Relação entre Cart e CartItem
    modelBuilder.Entity<Cart>()
        .HasMany(c => c.CartItems)
        .WithOne(ci => ci.Cart) // Definindo a relação de volta para CartItem
        .HasForeignKey(ci => ci.CartId);

    // Relação entre Order e OrderItem
    modelBuilder.Entity<Order>()
        .HasMany(o => o.OrderItems)
        .WithOne(oi => oi.Order) // Definindo a relação de volta para OrderItem
        .HasForeignKey(oi => oi.OrderId);

    // Relação entre CartItem e Product
    modelBuilder.Entity<CartItem>()
        .HasOne(ci => ci.Product)
        .WithMany()
        .HasForeignKey(ci => ci.ProductId);

    // Relação entre OrderItem e Product
    modelBuilder.Entity<OrderItem>()
        .HasOne(oi => oi.Product)
        .WithMany()
        .HasForeignKey(oi => oi.ProductId);
}


}

