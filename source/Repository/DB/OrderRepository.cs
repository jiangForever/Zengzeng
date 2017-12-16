using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DB
{
    public class OrderRepository : BaseRepository<Order>
    {
        public async Task Test()
        {
            Order order = new Order()
            {
                LeaveMessage = "",
                PaymentDate = DateTime.Now,
                OrderDate = DateTime.Now,
            };
            await Collection.InsertOneAsync(order);
        }
    }
}
