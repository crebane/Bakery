using BakeryLabb.Classes;
using Blazored.LocalStorage;

namespace BakeryLabb.Data;

public class ShoppingCartService
{
    private const string CartKey = "userCart";

    //försöker få siffran vid varukorgen att uppdateras
    //public event Action<int> OnShoppingCartChanged;


    private readonly ILocalStorageService _localStorage;

    public List<ShoppingCartProduct> ShoppingCartProducts { get; set; } = new();

    public ShoppingCartService(BakeryDbContext context, ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
    }

    private UserInformation UserInformation { get; set; } = new UserInformation();
    //public event Action<int> OnShoppingCartChanged;

    public async Task InitializeShoppingCart()
    {
        ShoppingCartProducts = await _localStorage.GetItemAsync<List<ShoppingCartProduct>>(CartKey) ?? new List<ShoppingCartProduct>();
    }

    public async Task<bool> AddToCart(CartProductToAdd cartProductToAdd)
    {
        try
        {
            var existingProduct = ShoppingCartProducts.FirstOrDefault(p => p.ProductId == cartProductToAdd.ProductId);

            if (existingProduct != null)
            {
                // Produkten finns redan i varukorgen, öka kvantiteten
                existingProduct.Qty += cartProductToAdd.Qty;
            }
            else
            {
                // Produkten finns inte i varukorgen, lägg till den
                ShoppingCartProducts.Add(new ShoppingCartProduct
                {
                    ProductId = cartProductToAdd.ProductId,
                    Name = cartProductToAdd.Name,
                    Price = cartProductToAdd.Price,
                    Qty = cartProductToAdd.Qty
                });

            }
            
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
            ShoppingCartProducts = products;
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
        await _localStorage.SetItemAsync(CartKey, ShoppingCartProducts);

        // Uppdatera antalet varor i varukorgen när varukorgen ändras
        //OnShoppingCartChanged?.Invoke(_shoppingCart.ShoppingCartProducts.Count);
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
    //public void RaiseEventOnShoppingCartChanged(int totalQty)
    //{
    //    if (OnShoppingCartChanged != null)
    //    {
    //        OnShoppingCartChanged.Invoke(totalQty);
    //    }
    //}

    public async Task RemoveProductFromCart(int productId)
    {
        var existingProduct = ShoppingCartProducts.FirstOrDefault(p => p.Id == productId);

        if (existingProduct != null)
        {

            if (existingProduct.Qty > 1)
            {
                // Om det finns fler än en instans av produkten i varukorgen, minska bara Qty
                existingProduct.Qty--;
            }
            else
            {
                // Om det bara finns en instans, ta bort hela produkten från varukorgen
                ShoppingCartProducts.Remove(existingProduct);
            }

            await SaveShoppingCart();

        }
    }

    public decimal GetTotal()
    {
        var total = ShoppingCartProducts.Sum(product => product.Price * product.Qty);
        return total;
    }

    public async Task<bool> UpdateQuantity(int productId, int newQuantity)
    {
        try
        {
            var existingProduct = ShoppingCartProducts.FirstOrDefault(p => p.ProductId == productId);

            if (existingProduct != null)
            {
                // Uppdatera kvantiteten för befintlig produkt
                existingProduct.Qty = newQuantity;

                // Spara varukorgen med uppdaterad kvantitet
                await SaveShoppingCart();

                return true;
            }
            else
            {
                Console.WriteLine($"Produkt med ID {productId} hittades inte i varukorgen.");
                return false;
            }
        }
        catch (Exception ex)
        {
            // Logga eventuella fel eller hantera dem på annat sätt
            Console.WriteLine($"Fel vid uppdatering av kvantitet i varukorgen: {ex.Message}");
            return false;
        }
    }

    public int GetShoppingCartItemsCount()
    {
        // Returnera antalet varor i varukorgen
        return ShoppingCartProducts.Count;
    }
}