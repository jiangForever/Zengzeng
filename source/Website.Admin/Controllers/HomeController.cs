using Service;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Website.Admin.Controllers
{
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index()
        {
            OrderService service = new OrderService();
            await service.Test();
            return View();
        }

        [HttpPost]
        public ActionResult DoLogin(string userName, string password)
        {
            string _userName = ConfigurationManager.AppSettings["UserName"];
            string _password = ConfigurationManager.AppSettings["Password"];
            if (_userName == userName && password == _password)
            {
                Session.Add("IsLogin", true);
                return Redirect("/User/Index");
            }
            return Redirect("/");
        }

    }
}
