using Entity;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Website.Admin.Models;

namespace Website.Admin.API.Controllers
{
    public class GoodsController : ApiController
    {
        private GoodsService goodsService = new GoodsService();

        /// <summary>
        /// 查询用户数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResponseModel<Pager<Goods>>> GetGoodsPage(GoodsSearchModel request)
        {
            ResponseModel<Pager<Goods>> result = new ResponseModel<Pager<Entity.Goods>>();
            result.Code = 1;

            Goods goods = new Entity.Goods() { ID = request.ID };
            result.Data = await goodsService.GetGoods(goods, request.PageCount, request.PageNumber);
            return result;
        }

        /// <summary>
        /// 添加商品
        /// </summary>
        /// <param name="goods"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResponseModel> AddGoods(Goods goods)
        {
            ResponseModel result = new ResponseModel();
            result.Code = 1;
            await goodsService.AddGoods(goods);
            return result;
        }

        /// <summary>
        /// 修改商品
        /// </summary>
        /// <param name="goods"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResponseModel> UpdateGoods(Goods goods)
        {
            ResponseModel result = new ResponseModel();
            result.Code = 1;
            await goodsService.UpdateGoods(goods);
            return result;
        }
    }
}