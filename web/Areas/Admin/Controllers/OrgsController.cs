using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using Ray.Plat.Common;
using Ray.Plat.Controls;
using KNet.Data.Entity;
using web.Models;

namespace web.Areas.Admin.Controllers
{
    public class OrgsController : Controller
    {
        DataContext context = new DataContext("OM");

        //
        // GET: /Orgs/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string root)
        {
            IList<OM_Org> OM_Orgs = context.Gets<OM_Org>(null);
            IList<zTreeNode> zTreeNodes = zTreeNodeHelper.ObjTozTreeNodes<OM_Org>(OM_Orgs);
            IList<TreeNode> TreeNodes = EncodeTree.initTree(zTreeNodes,root);
            return Json(TreeNodes);
        }

        //创建部门信息
        // POST: /Orgs/Create

        [HttpPost]
        public ActionResult Create(OM_Org OM_Org)
        {
            try
            {
                // TODO: Add insert logic here
                OM_Org.Org_id = OM_Org.Org_pId + RandUtil.GenerateCode(6);
                context.Insert<OM_Org>(OM_Org);
                return Json(new { errorMsg=false});
            }
            catch(Exception ex)
            {
                return Json(new { errorMsg = ex.Message });
            }
        }


        public ActionResult Edit(string Org_id)
        {
            Expression<Func<OM_Org, bool>> match = c => c.Org_id == Org_id;
            OM_Org OM_Org = context.Get<OM_Org>(match);
            return Json(OM_Org,JsonRequestBehavior.AllowGet);
        }


        //编辑部门信息
        // POST: /Orgs/Edit/5

        [HttpPost]
        public ActionResult Edit(string Org_id,OM_Org OM_Org)
        {
            try
            {
                // TODO: Add update logic here
                context.Update<OM_Org>(OM_Org);
                return Json(new { errorMsg = false });
            }
            catch(Exception ex)
            {
                return Json(new { errorMsg = ex.Message });
            }
        }

        //删除部门信息
        // POST: /Orgs/Delete/5

        [HttpPost]
        public ActionResult Delete(string Org_id)
        {
            try
            {
                // TODO: Add delete logic here
                Expression<Func<OM_Org, bool>> match = c => c.Org_id == Org_id;
                OM_Org OM_Org = context.Get<OM_Org>(match);
                bool result = context.Delete<OM_Org>(OM_Org);
                if (result)
                    return Json(new { success = true });
                else
                    return Json(new { success = false });
            }
            catch(Exception ex)
            {
                return Json(new { errorMsg = ex.Message });
            }
        }
    }
}
