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
            return View();
        }

        //
        // GET: /RightTypes/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /RightTypes/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /RightTypes/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        //
        // GET: /RightTypes/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /RightTypes/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /RightTypes/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /RightTypes/Delete/5

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
