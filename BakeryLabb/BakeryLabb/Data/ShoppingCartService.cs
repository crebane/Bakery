using BakeryLabb.Classes;
using BakeryLabb.Components.Pages;
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
            //return await Task.FromResult(_shoppingCart.GetShoppingCart());
            var products = await _localStorage.GetItemAsync<List<ShoppingCartProduct>>(CartKey) ?? new List<ShoppingCartProduct>();
            _shoppingCart.ShoppingCartProducts = products;
            return products;
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