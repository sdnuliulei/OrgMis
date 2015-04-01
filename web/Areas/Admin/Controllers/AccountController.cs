using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ray.Plat.Controls;

namespace web.Areas.Admin.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/

        public ActionResult LogOn()
        {
            return View();
        }

        public ActionResult GetValidateCode()
        {
            string code = "";
            byte[] bytes = VerifyCode.CreateVerifyCode(out code);
            Session["ValidateCode"] = code;
            return File(bytes, @"image/jpeg");
        }
    }
}
