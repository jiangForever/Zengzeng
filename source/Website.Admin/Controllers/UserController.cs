using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Website.Admin.Controllers
{
    public class UserController : Controller
    {
        public ActionResult Index()
        {
            if (Session["IsLogin"] == null || !Convert.ToBoolean(Session["IsLogin"]))
            {
                return Redirect("/");
            }
            return View();
        }

    }
}
