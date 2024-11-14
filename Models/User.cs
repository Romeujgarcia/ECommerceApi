using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

public class User : IdentityUser
{
    // Relações
    public List<Cart> Carts { get; set; } = new List<Cart>();
    public List<Order> Orders { get; set; } = new List<Order>();
}




