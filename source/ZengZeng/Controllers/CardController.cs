using Entity;
using Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ZengZeng.Core;

namespace ZengZeng.Controllers
{
    public class CardController : Controller
    {

        private GoodsService goodsService = new GoodsService();

        private UserService userService = new UserService();

        /// <summary>
        /// 赠赠卡详情
        /// </summary>
        /// <param name="unique"></param>
        /// <returns></returns>
        public async Task<ActionResult> Detail(string unique)
        {
            //通过unique查询原赠赠卡人员信息
            User user = await userService.GetUser("", unique);

            //查询当前商品列表
            List<Goods> goods = await goodsService.GetGoods();
            ViewBag.User = user;
            ViewBag.Goods = goods;
            return View();
        }

        /// <summary>
        /// 生成截图
        /// </summary>
        /// <param name="unique"></param>
        /// <returns></returns>
        public ActionResult ShareImage(string unique)
        {
            string filepath = Server.MapPath("../Image/unique/") + unique + ".jpg";
            if (!System.IO.Directory.Exists(Server.MapPath("../Image/Unique/")))
            {
                Directory.CreateDirectory(Server.MapPath("../Image/Unique/"));
                Directory.CreateDirectory(Server.MapPath("../Image/Temp/"));
            }
            if (!System.IO.File.Exists(filepath))
            {
                string oldImageBack = Server.MapPath(@"\asserts\image\zzk.png");
                string tempFilePath = Server.MapPath(@"\Image\Temp\qr" + unique + ".jpg");
                string url = "http://localhost:3432/d/" + unique;
                QRUtils.GenerateQR(url, tempFilePath);
                ImageUtils.CombinImage(oldImageBack, tempFilePath, filepath);
            }
            return File(filepath, "image/jpeg");
        }

    }
}
