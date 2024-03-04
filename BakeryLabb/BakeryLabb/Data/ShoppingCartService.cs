using BakeryLabb.Classes;
using BakeryLabb.Components.Pages;
using BakeryLabb.Migrations;
using Blazored.LocalStorage;
using Microsoft.EntityFrameworkCore;

namespace BakeryLabb.Data;

public class ShoppingCartService
{
    private const string CartKey = "userCart";

    //försöker få siffran vid varukorgen att uppdateras
    public event Action<int> OnShoppingCartChanged;


    private readonly BakeryDbContext _context;
    private readonly ILocalStorageService _localStorage;
    private readonly Classes.ShoppingCart _shoppingCart;

    public ShoppingCartService(BakeryDbContext context, ILocalStorageService localStorage, Classes.ShoppingCart shoppingCart)
    {
        _context = context;
        _localStorage = localStorage;
        _shoppingCart = shoppingCart;
    }

    private UserInformation UserInformation { get; set; } = new UserInformation();

    public async Task<bool> AddToCart(CartProductToAdd cartProductToAdd)
    {
        try
        {
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

    public async Task<List<ShoppingCartProduct>> GetShoppingCartProducts()
    {
        try
        {
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

    public void SetShippingAddress(UserInformation userInformation)
    {
        UserInformation = userInformation;
    }

    public UserInformation GetShippingAddress()
    {
        return UserInformation;
    }

    public async Task ClearShoppingCart()
    {
        await _localStorage.RemoveItemAsync(CartKey);
    }

    //försöker få siffran vid varukorgen att uppdateras
    public void RaiseEventOnShoppingCartChanged(int totalQty)
    {
        if (OnShoppingCartChanged != null)
        {
            OnShoppingCartChanged.Invoke(totalQty);
        }
    }

}