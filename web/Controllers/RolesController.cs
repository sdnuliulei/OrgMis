using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using KNet.Data.Entity;
using web.Models;


namespace web.Controllers
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

        //
        // POST: /Roles/Create

        [HttpPost]
        public ActionResult Create(OM_Role OM_Role)
        {
            try
            {
                // TODO: Add insert logic here
                bool result = context.Insert<OM_Role>(OM_Role);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        //
        // GET: /Roles/Edit/5
 
        public ActionResult Edit(int RoleID)
        {
            return View();
        }

        //
        // POST: /Roles/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, OM_Role OM_Role)
        {
            try
            {
                // TODO: Add update logic here
                bool result = context.Update<OM_Role>(OM_Role);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Roles/Delete/5
 
        public ActionResult Delete(int RID)
        {
            return View();
        }

        //
        // POST: /Roles/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
