namespace Api.Utils;

using Domain.Core.Products;
using Infrastructure.Identity;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

public static class DataHelper
{

    public static async Task ManageData(IServiceProvider serviceProvider)
    {
        //Service: An instance of db context
        var dataProvider = serviceProvider.GetRequiredService<DataContext>();
        var identityProvider = serviceProvider.GetRequiredService<IdentityDataContext>();

        //Migration: This is the programmatic equivalent to Update-Database
        await dataProvider.Database.MigrateAsync();
        await identityProvider.Database.MigrateAsync();

        //Seed:
        await SeedProducts(dataProvider);
    }

    private static async Task SeedProducts(DataContext dataContext)
    {

        if (!await dataContext.Set<Product>().AnyAsync())
        {
            var productShoes = Product.Create("Shoes", "Description of shoes", 99.99m);
            var productGlasses = Product.Create("Glasses", "Description of glasses", 199.99m);

            dataContext.Set<Product>().AddRange(productShoes, productGlasses);
            await dataContext.SaveChangesAsync();
        }

    }

}
