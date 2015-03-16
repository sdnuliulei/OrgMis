using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KNet.Data.Entity;
using Ray.Plat.Controls;

namespace web.Controllers
{
    public class RightTypesController : Controller
    {
        //
        // GET: /RightTypes/
        DataContext context = new DataContext("OM");

        public ActionResult Index()
        {
            IList<Models.OM_RightType> OM_RightTypes = context.Gets<Models.OM_RightType>(null);
            return Json(OM_RightTypes);
        }

        /// <summary>
        /// 获取权限类别
        /// </summary>
        /// <returns></returns>
        public ActionResult GetRightTypes()
        {
            IList<Models.OM_RightType> OM_RightTypes = context.Gets<Models.OM_RightType>(null);
            IList<TreeNode> TreeNodes = new List<TreeNode>();
            foreach (Models.OM_RightType model in OM_RightTypes)
            {
                TreeNode node = new TreeNode();
                node.id = model.RTID.ToString();
                node.text = model.RTypeName;
                TreeNodes.Add(node);
            }
            List<TreeNode> rootNode = new List<TreeNode>() { new TreeNode { id = "0", text = "权限类型", state = "open", children = TreeNodes } };
            return Json(rootNode,JsonRequestBehavior.AllowGet);
        }

    }
}
