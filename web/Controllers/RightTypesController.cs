using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KNet.Data.Entity;

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

    }
}
