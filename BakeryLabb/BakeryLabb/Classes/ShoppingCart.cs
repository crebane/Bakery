namespace BakeryLabb.Classes;

public class ShoppingCart
{
    public int Id { get; set; }
    public List<Product> Products { get; set; }
    public List<ShoppingCartProduct> ShoppingCartProducts { get; set; } = new List<ShoppingCartProduct>();

    public List<ShoppingCartProduct> GetShoppingCart()
    {
        return ShoppingCartProducts;
    }
}
