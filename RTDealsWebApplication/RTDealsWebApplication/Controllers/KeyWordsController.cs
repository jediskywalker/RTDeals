using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RTDealsWebApplication.Models;
using RTDealsWebApplication.DBAccess;
using System.Text;

namespace RTDealsWebApplication.Controllers
{
    public class KeyWordsController : Controller
    {
        //
        // GET: /KeyWords/

        public ActionResult Index()
        {
            ViewData["Category"] = CategoryDB.GetCategory();
            return View();
        }

        [HttpPost]
        public ActionResult Index(FormCollection collection)
        {

            CategoryModel cm = new CategoryModel();
            cm.Name = collection["Category"];
            CategoryDB.InsertCategory(cm);

            return RedirectToAction("Index");

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
 
        public ActionResult Edit(int? id)
        {
            if (id != null)
            {

                ViewData["CategoryKeywords"] = CategoryDB.GetCategoryKeywordsByID(Convert.ToInt16(id));
                Session["CategoryID"] = id;
            }
            else
            {
                ViewData["CategoryKeywords"] = CategoryDB.GetCategoryKeywordsByID(Convert.ToInt32(Session["CategoryID"]));

            }

            return View();
        }

        //
        // POST: /KeyWords/Edit/5

        [HttpPost]
        public ActionResult Edit(int? id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
               // ViewData["CategoryKeywords"] = CategoryDB.GetCategoryKeywordsByID(Convert.ToInt16(Session["CategoryID"]));
               // return View("edit");
                return RedirectToAction("UpdateCategoryKeywords", new { id = collection["N"] });

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



        public ActionResult UpdateCategoryKeywords(string id)
        {
            try
            {
                if (id != null)
                {
                    CategoryKeywordsModel ckm = new CategoryKeywordsModel();
                    ckm.CategoryID = Convert.ToInt32(Session["CategoryID"]);
                    ckm.Keyword = id;
                    CategoryDB.InsertCategoryKeywords(ckm);
                }

                return RedirectToAction("Edit");
            }
            catch (Exception e)
            {

                throw (new Exception(e.Message));
            }
        
        }


    }
}
