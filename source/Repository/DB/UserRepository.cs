using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;

namespace Repository
{
    public class UserRepository : BaseRepository<User>
    {
        /// <summary>
        /// 判断用户是否生成过赠赠卡
        /// </summary>
        /// <param name="openid"></param>
        /// <returns></returns>
        public async Task<User> GetUser(string openid, string unique)
        {
            User user = null;
            FilterDefinition<User> filter = null;
            if (!string.IsNullOrEmpty(openid))
            {
                filter = Filter.Eq(nameof(User.OpenId), openid);
            }
            else
            {
                filter = Filter.Eq(nameof(User.Unique), unique);
            }
            var list = await Collection.Find(filter).ToListAsync();
            if (list.Any())
            {
                user = list.First();
            }
            return user;
        }

        /// <summary>
        ///  创建用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<User> CreateUser(User user)
        {
            user.CreateDate = DateTime.Now;
            user.Unique = DateTime.Now.Ticks.ToString();
            await this.Collection.InsertOneAsync(user);
            return user;
        }


        /// <summary>
        /// 查询用户列表
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<Pager<User>> GetUsers(User user, int pageCount, int pageIndex)
        {
            Pager<User> users = new Pager<User>();
            FilterDefinition<User> filter = Filter.Empty;

            if (!string.IsNullOrEmpty(user.ID))
            {
                filter = filter & Filter.Eq(nameof(User.ID), user.ID);
            }
            if (!string.IsNullOrEmpty(user.Mobile))
            {
                filter = filter & Filter.Eq(nameof(User.Mobile), user.Mobile);
            }

            users.TotalCount = await this.Collection.CountAsync(filter);

            var sort = Sort.Ascending(nameof(User.CreateDate));
            FindOptions<User, User> findoption = new FindOptions<User, User>();
            findoption.Sort = sort;
            findoption.Limit = pageCount;
            findoption.Skip = (pageIndex - 1) * pageCount;
            var usersSync = this.Collection.FindSync(filter, findoption);
            users.Data = await usersSync.ToListAsync();
            return users;
        }
    }
}
