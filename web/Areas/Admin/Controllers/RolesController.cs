using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using KNet.Data.Entity;
using web.Models;


namespace web.Areas.Admin.Controllers
{
    public class RolesController : Controller
    {
        //
        // GET: /Roles/
        DataContext context = new DataContext("OM");

        public ActionResult Index()
        {
            return View();
        }

        //加载角色列表
        //POST:/Roles/Index
        [HttpPost]
        public ActionResult Index(int page,int rows)
        {
            int start = (page - 1) * rows;
            int length = rows;
            int count = 0;
            Expression<Func<OM_Role, bool>> match = null;
            Expression<Func<OM_Role, bool>> order = null;
            order=c=>c.RoleID.Asc();
            IList<OM_Role> OM_Roles = context.Gets<OM_Role>(match, start, length, out count,order:order);
            return Json(new { total = count, rows = OM_Roles });
        }

        //添加新角色
        // POST: /Roles/Create
        [HttpPost]
        public ActionResult Create(OM_Role OM_Role)
        {
            try
            {
                // TODO: Add insert logic here
                bool result = context.Insert<OM_Role>(OM_Role);
                return Json(new { errorMsg=false});
            }
            catch(Exception ex)
            {
                return Json(new { errorMsg = ex.Message });
            }
        }
        

        //编辑角色名称
        // POST: /Roles/Edit/5
        [HttpPost]
        public ActionResult Edit(int RoleID,OM_Role OM_Role)
        {
            try
            {
                // TODO: Add update logic here
                bool result = context.Update<OM_Role>(OM_Role);
                return Json(new { errorMsg = false });
            }
            catch(Exception ex)
            {
                return Json(new { errorMsg = ex.Message });
            }
        }

        //删除角色，注意与权限的关联性
        // GET: /Roles/Delete/5
        [HttpPost]
        public ActionResult Delete(int RoleID)
        {
            Expression<Func<OM_Role, bool>> match = c => c.RoleID == RoleID;
            OM_Role OM_Role = context.Get<OM_Role>(match);
            bool result = context.Delete<OM_Role>(OM_Role);
            return Json(new { success = result });
        }

        public ActionResult List()
        {
            IList<OM_Role> OM_Roles = context.Gets<OM_Role>(null);
            return Json(OM_Roles);
        }
    }
}
