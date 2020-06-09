using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace WebTek.Models
{
    public class FrameworkOrderRepo : OrderRepoInterface
    {
        private AppDataBase content;
        public FrameworkOrderRepo(AppDataBase cnt)
        {
            content = cnt;
        }
        public IEnumerable<Order> Orders => content.Orders
        .Include(o => o.Lines)
       .ThenInclude(l => l.Product);
        public void SaveOrder(Order order)
        {
            content.AttachRange(order.Lines.Select(l => l.Product));
            if (order.OrderID == 0)
            {
                content.Orders.Add(order);
            }
            content.SaveChanges();
        }
    }
}