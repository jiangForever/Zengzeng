using Entity;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    /// <summary>
    /// 用户服务
    /// </summary>
    public class UserService
    {
        private UserRepository userRep = new UserRepository();

        /// <summary>
        /// 判断用户是否生成过赠赠卡
        /// </summary>
        /// <param name="openid"></param>
        /// <returns></returns>
        public async Task<User> GetUser(string openid, string unique)
        {
            return await userRep.GetUser(openid, unique);
        }

        /// <summary>
        ///  创建用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<User> CreateUser(User user)
        {
            return await userRep.CreateUser(user);
        }

        /// <summary>
        /// 查询用户列表
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<Pager<User>> GetUsers(User user,int pageCount,int pageIndex)
        {
            return await userRep.GetUsers(user, pageCount, pageIndex);
        }
    }
}
