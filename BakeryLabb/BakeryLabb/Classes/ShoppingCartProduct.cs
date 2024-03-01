using BakeryLabb.Components.Pages;
using System.ComponentModel.DataAnnotations.Schema;

namespace BakeryLabb.Classes;

public class ShoppingCartProduct
{
    public int Id { get; set; }
    public int ShoppingCartId { get; set; }
    public int ProductId { get; set; }
    public Product Product { get; set; }
    public ShoppingCart Shopping { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Qty { get; set; }
    public decimal TotalPrice => Price * Qty;
}
