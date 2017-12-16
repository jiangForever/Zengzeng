using Entity;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using ZengZeng.Models;

namespace ZengZeng.API.Controllers
{
    public class CardController : ApiController
    {
        private UserService userService = new UserService();

        /// <summary>
        /// 生成卡
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ReponseModel<string>> GenerateCard(User user)
        {
            ReponseModel<string> result = new ReponseModel<string>();
            result.Code = 1;
            if (string.IsNullOrEmpty(user.Address) || string.IsNullOrEmpty(user.Avator)
               || string.IsNullOrEmpty(user.CardNo) || string.IsNullOrEmpty(user.Mobile) || string.IsNullOrEmpty(user.NickName)
               || string.IsNullOrEmpty(user.OpenId))
            {
                result.Code = -1;
                result.Message = "对不起，信息不全，请重试！";
                return result;
            }
            await userService.CreateUser(user);
            result.Data = user.Unique;
            return result;
        }
    }
}