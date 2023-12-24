using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineShop.DAL.Domain;

namespace OnlineShop.DAL.Classes
{
    public class SeedData
    {
        private StoreDbContext context;
        private ILogger logger;
        private static Product[] products = new Product[] {
            new Product { Name = "Socks" },
            new Product { Name = "Coat" },
            new Product { Name = "Chess" },
        };

        public SeedData(StoreDbContext dataContext, ILogger log)
        {
            context = dataContext;
            logger = log;
        }

        public void SeedDatabase()
        {
            context.Database.Migrate();
            if (context.Products?.Count() == 0)
            {
                logger.LogInformation("Preparing to seed database");
                context.Products.AddRange(products);
                context.SaveChanges();
                logger.LogInformation("Database seeded");
            }
            else
            {
                logger.LogInformation("Database not seeded");
            }
        }
    }
}
