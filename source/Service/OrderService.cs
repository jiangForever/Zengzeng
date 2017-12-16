using Repository.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class OrderService
    {
        private OrderRepository orderRep = new OrderRepository();

        public async Task Test()
        {
            await orderRep.Test();
        }
    }
}
