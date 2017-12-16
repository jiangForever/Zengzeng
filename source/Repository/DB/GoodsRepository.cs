using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using Enum;

namespace Repository.DB
{
    public class GoodsRepository : BaseRepository<Goods>
    {
        /// <summary>
        /// 返回所有的商品
        /// </summary>
        /// <returns></returns>
        public async Task<List<Goods>> GetGoods()
        {
            var filter = Filter.Eq(nameof(Goods.State), GoodsState.In);
            var cusor = await Collection.FindAsync(filter);
            return await cusor.ToListAsync();
        }

        /// <summary>
        /// 得到商品列表
        /// </summary>
        /// <param name="goods"></param>
        /// <param name="pageCount"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        public async Task<Pager<Goods>> GetGoods(Goods goods, int pageCount, int pageNumber)
        {
            Pager<Goods> goodss = new Pager<Goods>();
            FilterDefinition<Goods> filter = Filter.Empty;

            if (!string.IsNullOrEmpty(goods.ID))
            {
                filter = filter & Filter.Eq(nameof(Goods.ID), goods.ID);
            }

            goodss.TotalCount = await this.Collection.CountAsync(filter);

            var sort = Sort.Descending(nameof(Goods.ID));
            FindOptions<Goods, Goods> findoption = new FindOptions<Goods, Goods>();
            findoption.Sort = sort;
            findoption.Limit = pageCount;
            findoption.Skip = (pageNumber - 1) * pageCount;
            var goodsSync = this.Collection.FindSync(filter, findoption);
            goodss.Data = await goodsSync.ToListAsync();
            return goodss;
        }

        /// <summary>
        /// 添加商品
        /// </summary>
        /// <param name="goods"></param>
        /// <returns></returns>
        public async Task AddGoods(Goods goods)
        {
            goods.State = GoodsState.In;
            await this.Collection.InsertOneAsync(goods);
        }

        /// <summary>
        /// 修改商品数据
        /// </summary>
        /// <param name="goods"></param>
        /// <returns></returns>
        public async Task UpdateGoods(Goods goods)
        {
            var filter = Filter.Eq(nameof(Goods.ID), goods.ID);
            var update = Update.Set(nameof(goods.Name), goods.Name)
                .Set(nameof(goods.Pictures), goods.Pictures)
                .Set(nameof(goods.Price), goods.Price)
                .Set(nameof(goods.Sort), goods.Sort)
                .Set(nameof(goods.Source), goods.Source)
                .Set(nameof(goods.SubTitle), goods.SubTitle)
                .Set(nameof(goods.Title), goods.Title);
            await this.Collection.UpdateOneAsync(filter, update);
        }
    }
}
