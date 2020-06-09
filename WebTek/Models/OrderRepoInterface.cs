using System.Collections.Generic;

namespace WebTek.Models
{
    public interface OrderRepoInterface
    {
        IEnumerable<Order> Orders { get; }
        void SaveOrder(Order order);
    }
}
