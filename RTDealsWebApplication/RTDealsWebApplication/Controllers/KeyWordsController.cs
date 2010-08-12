using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RTDealsWebApplication.Controllers
{
    public class KeyWordsController : Controller
    {
        //
        // GET: /KeyWords/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /KeyWords/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /KeyWords/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /KeyWords/Create

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
        // GET: /KeyWords/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /KeyWords/Edit/5

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
        // GET: /KeyWords/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /KeyWords/Delete/5

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
