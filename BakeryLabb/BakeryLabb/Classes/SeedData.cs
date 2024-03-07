using BakeryLabb.Data;

namespace BakeryLabb.Classes;

public static class SeedData
{
    public static void SeedProducts(BakeryDbContext context)
    {
        if (!context.Products.Any())
        {
            context.AddRange(GetSampleProducts());
            context.SaveChanges();
        }
    }

    private static List<Product> GetSampleProducts()
    {
        return new List<Product>
        {
            new Product
            {
                Name = "Piece of cake",
                Price = 40,
                Description = "Dagens smak är chokladbottnar, nutellamousse och en spegel av hallonsylt.",
                ImageURL = "/images/Tårtbit.jpg",
                Qty = 15
            },
            new Product
            {
                Name = "Cookie",
                Price = 15,
                Description = "Vår kundfavorit Caramel & Sea salt är äntligen tillbaka! Missa inte att beställa, vem vet när de finns att köpa igen.",
                ImageURL = "/images/Småkakor.jpg",
                Qty = 50
            },
            new Product
            {
                Name = "Donut",
                Price = 15,
                Description = "Donut med citronglasyr doppad i polkaströssel, fylld med krämig vaniljkräm.",
                ImageURL = "/images/Donut.jpg",
                Qty = 25
            },
            new Product
            {
                Name = "Éclair",
                Price = 20,
                Description = "Fyllda med en len chokladkräm.",
                ImageURL = "/images/Éclair.jpg",
                Qty = 20
            },
            new Product
            {
                Name = "Sugarbun",
                Price = 25,
                Description = "Härligt nybakade kardemummabulle doppade i strösocker.",
                ImageURL = "/images/Sockerbulle.jpg",
                Qty = 45
            },
            new Product
            {
                Name = "Tartlette",
                Price = 30,
                Description = "En minipaj fylld av vaniljgrädde, toppad med färska bär.",
                ImageURL = "/images/Tartlette.jpg",
                Qty = 20
            }
        };
    }
}
