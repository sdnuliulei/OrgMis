using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using KNet.Data.Entity;

namespace web.Controllers
{
    public class RightsController : Controller
    {
        DataContext context = new DataContext("OM");

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Rights/
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page">页数</param>
        /// <param name="rows">当前页记录数</param>
        /// <param name="RTID">所属权限类型</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Index(int page,int rows,int RTID)
        {
            int start = (page - 1) * rows;
            int length = rows;
            int count=0;
            Expression<Func<Models.OM_Right, bool>> match = null;//构造match
            if (RTID != 0)
            {
                match = c => c.RTID == RTID;
            }
            Expression<Func<Models.OM_Right, bool>> order = null;//构造order
            order = c => c.RID.Asc();
            IList<Models.OM_Right> OM_Rights = context.Gets<Models.OM_Right>(match, start, length, out count,order);
            return Json(new { total = count, rows = OM_Rights });
        }

        //
        // POST: /Rights/Create

        [HttpPost]
        public ActionResult Create(Models.OM_Right OM_Right)
        {
            try
            {
                context.Insert<Models.OM_Right>(OM_Right);
                return Json(new { errorMsg = false });//解决返回值关闭弹出窗口的问题
            }
            catch
            {
                return View();
            }
        }
        
        //
        // GET: /Rights/Edit/5
 
        public ActionResult Edit(int RID)
        {
            Expression<Func<Models.OM_Right,bool>>match=c=>c.RID==RID;
            Models.OM_Right OM_Right = context.Get<Models.OM_Right>(match);
            return Json(OM_Right,JsonRequestBehavior.AllowGet);
        }

        //
        // POST: /Rights/Edit/5

        [HttpPost]
        public ActionResult Edit(int RID,Models.OM_Right OM_Right)
        {
            try
            {
                // TODO: Add update logic here
                context.Update<Models.OM_Right>(OM_Right);
                return Json(new { errorMsg = false });
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Rights/Delete/5

        [HttpPost]
        public ActionResult Delete(int RID)
        {
            Expression<Func<Models.OM_Right, bool>> match = c => c.RID == RID;
            Models.OM_Right OM_Right = context.Get<Models.OM_Right>(match);
            bool result = context.Delete<Models.OM_Right>(OM_Right);
            return Json(new { success = result });
        }

        protected override void HandleUnknownAction(string actionName)
        {
            base.HandleUnknownAction(actionName);
        }
    }
}
