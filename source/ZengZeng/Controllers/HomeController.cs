using Entity;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ZengZeng.Core;

namespace ZengZeng.Controllers
{
    public class HomeController : Controller
    {
        const string appid = "wx0f9e0d66f55bd8b3";
        const string secret = "84477040951d2a51593853b453dc75ec";


        private UserService userService = new UserService();

        private WechatService wechatService = new WechatService();

        /// <summary>
        /// 获取你的赠赠卡
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Index()
        {
            string code = Request["code"];
            string openid = "";
            //并非回调回来
            if (string.IsNullOrEmpty(code))//
            {
                //判断是否有token，如果有的话，就认为其已经登录
                string token = string.Empty;
                var tokenCookie = Request.Cookies.Get("token");
                if (tokenCookie != null)
                {
                    token = System.Web.HttpUtility.UrlDecode(tokenCookie.Value);
                    openid = Cipher.Decrypt3DES(token);
                }
                openid = "om28r0QUuRtqZBaqOxzS0A4lEqTk";
                if (string.IsNullOrEmpty(openid))//跳转授权页面
                {
                    string redirecturl = Request.Url.ToString();
                    string url = "https://open.weixin.qq.com/connect/oauth2/authorize?appid=" + appid + "&redirect_uri=" + redirecturl + "&response_type=code&scope=snsapi_base&state=STATE#wechat_redirect";
                    return Redirect(url);
                }

                //如果已经生成卡片，就跳转保存分享卡片页面
                User user = new Entity.User();
                if ((user = await userService.GetUser(openid, "")) != null)
                {
                    string urlShare = "/Home/ShareCard";
                    return Redirect(urlShare);
                }
                //拿到openid，返回页面
                ViewBag.OpenId = openid;
                return View();
            }
            //如果有code，则进行拿到openid，并生成token，放入cookies中，返回页面
            var accessToken = await wechatService.GetWechatOpenId(code);
            if (accessToken == null)
            {
                //跳转错误页
            }
            openid = accessToken.openid;
            string newtoken = Cipher.Encrypt3DES(accessToken.openid);
            HttpCookie cookies = new HttpCookie("token");
            cookies.Domain = ".zengzenggift.xin";
            cookies.Expires = DateTime.Now.AddDays(30);
            cookies.Value = HttpUtility.UrlEncode(newtoken);
            Response.SetCookie(cookies);
            ViewBag.OpenId = openid;
            return View();
        }

        /// <summary>
        /// 卡片分享页面
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> ShareCard()
        {
            string openid = string.Empty;
            //判断是否有token，如果有的话，就认为其已经登录
            string token = string.Empty;
            var tokenCookie = Request.Cookies.Get("token");
            if (tokenCookie != null)
            {
                token = System.Web.HttpUtility.UrlDecode(tokenCookie.Value);
                openid = Cipher.Decrypt3DES(token);
            }
            openid = "om28r0QUuRtqZBaqOxzS0A4lEqTk";
            if (string.IsNullOrEmpty(openid))
            {
                return RedirectToAction("Index");
            }
            User user = await userService.GetUser(openid, "");
            return View(user);
        }
    }
}
