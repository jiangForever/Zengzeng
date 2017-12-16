using Entity;
using Repository.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class GoodsService
    {
        private GoodsRepository goodsRep = new GoodsRepository();

        /// <summary>
        /// 返回所有的商品
        /// </summary>
        /// <returns></returns>
        public async Task<List<Goods>> GetGoods()
        {
            return await goodsRep.GetGoods();
        }

        public async Task<Pager<Goods>> GetGoods(Goods goods, int pageCount, int pageNumber)
        {
            return await goodsRep.GetGoods(goods, pageCount, pageNumber);
        }

        /// <summary>
        /// 添加商品
        /// </summary>
        /// <param name="goods"></param>
        /// <returns></returns>
        public async Task AddGoods(Goods goods)
        {
            await goodsRep.AddGoods(goods);
        }

        /// <summary>
        /// 修改商品数据
        /// </summary>
        /// <param name="goods"></param>
        /// <returns></returns>
        public async Task UpdateGoods(Goods goods)
        {
            await goodsRep.UpdateGoods(goods);
        }
    }
}
