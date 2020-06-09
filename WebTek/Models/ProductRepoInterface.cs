using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebTek.Models
{
    public interface ProductRepoInterface
    {
        IEnumerable<Product> Products { get; }
        void SaveProduct(Product product);
        Product DeleteProduct(int productID);
        Product DeleteSellerProduct(string Seller);
        Product RemoveQuantity(int productID);
        Product AddQuantity(int productID);


    }
}
