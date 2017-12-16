using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class WechatService
    {
        const string appid = "wx0f9e0d66f55bd8b3";
        const string secret = "84477040951d2a51593853b453dc75ec";

        /// <summary>
        /// 获取openid
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public async Task<AccessToken> GetWechatOpenId(string code)
        {
            string url = "https://api.weixin.qq.com/sns/oauth2/access_token?appid=" + appid + "&secret=" + secret + "&code=" + code + "&grant_type=authorization_code ";
            AccessToken token = await NetUtils.GetResult<AccessToken>(url);
            return token;
        }

        /// <summary>
        /// 获取用户基础信息
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="openid"></param>
        /// <returns></returns>
        public async Task<UserInfo> GetWechatUserInfo(string access_token, string openid)
        {
            string url = "https://api.weixin.qq.com/sns/userinfo?access_token=" + access_token + "&openid=" + openid + "&lang=zh_CN ";
            UserInfo userinfo = await NetUtils.GetResult<UserInfo>(url);
            return userinfo;
        }
    }
}
