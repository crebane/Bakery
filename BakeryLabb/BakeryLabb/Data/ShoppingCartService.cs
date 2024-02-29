using BakeryLabb.Classes;
using BakeryLabb.Components.Pages;
//using BakeryLabb.Components.Pages;
using Blazored.LocalStorage;
using Microsoft.EntityFrameworkCore;

namespace BakeryLabb.Data;

public class ShoppingCartService
{
    private const string CartKey = "userCart";

    private readonly BakeryDbContext _bakeryDbContext;
    private readonly ILocalStorageService _localStorage;
    private readonly Classes.ShoppingCart _shoppingCart;

    public ShoppingCartService(BakeryDbContext bakeryDbContext, ILocalStorageService localStorage, Classes.ShoppingCart shoppingCart)
    {
        _bakeryDbContext = bakeryDbContext;
        _localStorage = localStorage;
        _shoppingCart = shoppingCart;
    }

    //private async Task<bool> CartItemExists(int productId)
    //{
    //    return await _bakeryDbContext.ShoppingCartProducts.AnyAsync(c => c.ProductId == productId);
    //}

    //public async Task<ShoppingCartProduct> AddProduct(CartProductToAdd cartProductToAdd)
    //{
    //    if (await CartItemExists(cartProductToAdd.ProductId) == false)
    //    {
    //        var item = await (from product in _bakeryDbContext.Products
    //                          where product.Id == cartProductToAdd.ProductId
    //                          select new ShoppingCartProduct
    //                          {
    //                              Qty = cartProductToAdd.Qty,
    //                              ProductId = product.Id,
    //                          }).SingleOrDefaultAsync();

    //        if (item != null)
    //        {
    //            var result = await _bakeryDbContext.ShoppingCartProducts.AddAsync(item);
    //            await _bakeryDbContext.SaveChangesAsync();
    //            return result.Entity;
    //        }
    //    }
    //    return null;
    //}

    //public async Task<ShoppingCartProduct> DeleteItem(int id)
    //{
    //    var item = await _bakeryDbContext.ShoppingCartProducts.FindAsync(id);

    //    if (item != null)
    //    {
    //        _bakeryDbContext.ShoppingCartProducts.Remove(item);
    //        await _bakeryDbContext.SaveChangesAsync();
    //    }
    //    return item;
    //}


    //public async Task<bool> AddToCart(CartProductToAdd cartProductToAdd)
    //{
    //    try
    //    {
    //        var userCart = await _localStorage.GetItemAsync<List<ShoppingCartProduct>>(CartKey) ?? new List<ShoppingCartProduct>();

    //        // Kontrollerar om produkten redan finns i varukorgen
    //        var existingProduct = userCart.Find(p => p.ProductId == cartProductToAdd.ProductId);

    //        if (existingProduct != null)
    //        {
    //            // Produkten finns redan i varukorgen, ökar bara antalet
    //            existingProduct.Qty += cartProductToAdd.Qty;
    //        }
    //        else
    //        {
    //            // Produkten finns inte i varukorgen, lägger till den
    //            var newProduct = new ShoppingCartProduct
    //            {
    //                ProductId = cartProductToAdd.ProductId,
    //                Qty = cartProductToAdd.Qty
    //            };
    //            userCart.Add(newProduct);
    //        }

    //        await _localStorage.SetItemAsync(CartKey, userCart);

    //        return true; // Lyckades lägga till i varukorgen
    //    }
    //    catch (Exception ex)
    //    {
    //        // Logga eventuella fel eller hantera dem på annat sätt
    //        Console.WriteLine($"Fel vid läggning till i varukorgen: {ex.Message}");
    //        return false; // Misslyckades med att lägga till i varukorgen
    //    }
    //}

    //public async Task<List<ShoppingCartProduct>> GetShoppingCart()
    //{
    //    try
    //    {
    //        return await _localStorage.GetItemAsync<List<ShoppingCartProduct>>(CartKey) ?? new List<ShoppingCartProduct>();
    //    }
    //    catch (Exception ex)
    //    {
    //        // Logga eventuella fel eller hantera dem på annat sätt
    //        Console.WriteLine($"Fel vid hämtning av varukorgen: {ex.Message}");
    //        return new List<ShoppingCartProduct>();
    //    }
    //}


    //private List<ShoppingCartProduct> ShoppingCart { get; set; }

    //public ShoppingCartService()
    //{
    //    ShoppingCart = new List<ShoppingCartProduct>();
    //}

    public async Task<bool> AddToCart(CartProductToAdd cartProductToAdd)
    {
        try
        {
            //_shoppingCart.AddToCart(cartProductToAdd);
            _shoppingCart.ShoppingCartProducts.Add(new ShoppingCartProduct
            {
                ProductId = cartProductToAdd.ProductId,
                Name = cartProductToAdd.Name,
                Price = cartProductToAdd.Price,
                Qty = cartProductToAdd.Qty
            });

            await SaveShoppingCart();
            return true;
        }
        catch (Exception ex)
        {
            // Logga eventuella fel eller hantera dem på annat sätt
            Console.WriteLine($"Fel vid läggning till i varukorgen: {ex.Message}");
            return false;
        }
    }

    public async Task<List<ShoppingCartProduct>> GetShoppingCart()
    {
        try
        {
            return await Task.FromResult(_shoppingCart.GetShoppingCart());
            //var products = await _localStorage.GetItemAsync<List<ShoppingCartProductModel>>(CartKey) ?? new List<ShoppingCartProductModel>();
            //_shoppingCart.ShoppingCartProducts = products;
            //return products;
        }
        catch (Exception ex)
        {
            // Logga eventuella fel eller hantera dem på annat sätt
            Console.WriteLine($"Fel vid hämtning av varukorgen: {ex.Message}");
            return new List<ShoppingCartProduct>();
        }
    }

    private async Task SaveShoppingCart()
    {
        await _localStorage.SetItemAsync(CartKey, _shoppingCart.ShoppingCartProducts);
    }
}