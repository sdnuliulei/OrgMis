using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using KNet.Data.Entity;
using Ray.Plat.Common;
using web.Models;

namespace web.Areas.Admin.Controllers
{
    public class UsersController : BaseController
    {
        DataContext context = new DataContext("OM");

        //
        // GET: /Users/

        public ActionResult Index()
        {
            return View();
        }

        //加载用户列表
        //
        [HttpPost]
        public ActionResult Index(int page,int rows,string Org_id)
        {
            int start = (page - 1) * rows;
            int length = rows;
            int count = 0;
            Expression<Func<OM_User, bool>> match = null;
            if (!string.IsNullOrEmpty(Org_id))
            {
                match = c => c.Org_id.Like(Org_id+"%");
            }
            Expression<Func<OM_User,bool>> order=c=>c.CreateTime.Desc();
            IList<OM_User> OM_Users = context.Gets<OM_User>(match, start, length, out count, order);
            return Json(new { total = count, rows = OM_Users });
        }


        //
        // POST: /Users/Create

        [HttpPost]
        public ActionResult Create(OM_User OM_User)
        {
            try
            {
                // TODO: Add insert logic here
                OM_User.UserID = OM_User.Org_id + RandUtil.GenerateCode(6);
                OM_User.Password = DEncrypt.GetMD5(OM_User.Password);
                OM_User.Status = true;
                OM_User.CreateTime = DateTime.Now;
                context.Insert<OM_User>(OM_User);
                return Json(new { errorMsg = false });
            }
            catch
            {
                return Json(new { errorMsg = true });
            }
        }
        
        //
        // GET: /Users/Edit/5
 
        public ActionResult Edit(string UserID)
        {
            Expression<Func<OM_User, bool>> match = c => c.UserID == UserID;
            OM_User OM_User = context.Get<OM_User>(match);
            return Json(OM_User,JsonRequestBehavior.AllowGet);
        }

        //编辑用户
        // POST: /Users/Edit/5

        [HttpPost]
        public ActionResult Edit(string UserID, OM_User OM_User)
        {
            try
            {
                // TODO: Add update logic here
                OM_User.CreateTime=DateTime.Now;
                context.Update<OM_User>(OM_User);
                return Json(new { errorMsg = false });
            }
            catch
            {
                return Json(new { errorMsg = true });
            }
        }

        //
        // POST: /Users/Delete/5

        [HttpPost]
        public ActionResult Delete(string UserID)
        {
            try
            {
                // TODO: Add delete logic here
                Expression<Func<OM_User, bool>> match = c => c.UserID == UserID;
                context.Delete<OM_User>(match);
                return Json(new { success = true });
            }
            catch(Exception ex)
            {
                return Json(new { errorMsg = ex.Message });
            }
        }

        [HttpPost]
        public ActionResult IsUserSingle(string UserName)
        {
            Expression<Func<OM_User, bool>> match = c => c.UserName == UserName;
            bool result = context.Exists<OM_User>(match);
            return Json(new { result = result });
        }
    }
}
