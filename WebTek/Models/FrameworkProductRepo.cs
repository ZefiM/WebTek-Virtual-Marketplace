using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebTek.Models
{
    public class FrameworkProductRepo : ProductRepoInterface
    {
        private AppDataBase context;
        public FrameworkProductRepo(AppDataBase ctx)
        {
            context = ctx;
        }
        public IEnumerable<Product> Products
        {
            get
            {
                return context.Products.ToList();
            }
        }

        public void SaveProduct(Product product)
        {
            if (product.ProductID == 0)
            {
                context.Products.Add(product);
            }
            else
            {
                Product dbEntry = context.Products
                .FirstOrDefault(p => p.ProductID == product.ProductID);
                if (dbEntry != null)
                {
                    dbEntry.Name = product.Name;
                    dbEntry.Description = product.Description;
                    dbEntry.Price = product.Price;
                    dbEntry.Category = product.Category;
                    dbEntry.Condition = product.Condition;
                    dbEntry.Quantity = product.Quantity;
                    dbEntry.Seller = product.Seller;
                }
            }
            context.SaveChanges();
        }

        public Product DeleteProduct(int productID)
        {
            Product dbEntry = context.Products
            .FirstOrDefault(p => p.ProductID == productID);
            if (dbEntry != null)
            {
                context.Products.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }

        public Product DeleteSellerProduct(string Seller)
        {
            int count = 0;
            Product dbEntry = context.Products
            .FirstOrDefault(p => p.Seller == Seller);
            foreach (var pro in Products)
            {
                count++;
            }
            for(int i = 0; i < count - 1; i++)
            {
                if (dbEntry.Seller == Seller)
                {
                    context.Products.Remove(dbEntry);
                    context.SaveChanges();
                }
                else
                    if (dbEntry.Seller != Seller)
                    {
                        //Seller does not have any products
                        return dbEntry;
                    }

            }
            return dbEntry;
        }

        public Product RemoveQuantity(int productID)
        {
            Product dbEntry = context.Products
            .FirstOrDefault(p => p.ProductID == productID);
            if (dbEntry != null)
            {
                dbEntry.Quantity = dbEntry.Quantity - 1;
            }
            context.SaveChanges();
            return dbEntry;
        }

        public Product AddQuantity(int productID)
        {
            Product dbEntry = context.Products
            .FirstOrDefault(p => p.ProductID == productID);
            if (dbEntry != null)
            {
                dbEntry.Quantity = dbEntry.Quantity + 1;
            }
            context.SaveChanges();
            return dbEntry;
        }

    }
}
