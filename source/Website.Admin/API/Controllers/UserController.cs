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
    public class UserController : ApiController
    {
        private UserService userService = new UserService();

        /// <summary>
        /// 查询用户数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResponseModel<Pager<User>>> GetUserPage(UserSearchModel request)
        {
            ResponseModel<Pager<User>> result = new ResponseModel<Pager<Entity.User>>();
            result.Code = 1;

            User user = new Entity.User() { ID = request.ID, Mobile = request.Mobile };
            result.Data = await userService.GetUsers(user, request.PageCount, request.PageNumber);
            return result;
        }
    }


}