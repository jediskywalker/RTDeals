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
    public class CustomerController : Controller
    {
        //
        // GET: /Customer/

        public ActionResult Index()
        {
            //ViewData["Customer"] = CustomerDB.getCustomer();
            ViewData["LoginCustomer"] =Session["Customer"];
 
            //CustomerModel cm=(CustomerModel)Session["Customer"];


           return RedirectToAction("Edit");
        }

        //
        // GET: /Customer/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Customer/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Customer/Create

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
        // GET: /Customer/Edit/5
 
        public ActionResult Edit(int? id)
        {
            ViewData["Category"] = CategoryDB.GetCategory();
            return View();
        }

        //
        // POST: /Customer/Edit/5

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
        // GET: /Customer/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Customer/Delete/5

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

        public ActionResult CustomerKeywordsSetup()
        {
            ViewData["Category"] = CategoryDB.GetCategory();

            return View();
        }


        public string ShowCategoryKeywords(string id)
        {
            CustomerModel cm = (CustomerModel)Session["Customer"];
            string[] temp = id.Split(',');
            if (temp[1] == "0")
                return "";
            List<CategoryKeywordsModel> lckm = CategoryDB.GetCategoryKeywordsByName(temp[0]);
            StringBuilder sb = new StringBuilder();
            int i = 0;
            sb.Append("<div><br />");
            sb.Append("<b>" + temp[0] + "</b><br />");
            foreach (CategoryKeywordsModel ckm in lckm)
            {
              if (i % 12 == 0)
                  sb.Append("<tr>");
              sb.Append("<td>");
              sb.Append(ckm.Keyword);
              sb.Append("</td>");
              sb.Append("<td>");
              if (CategoryDB.IsExsitCustomerCategoryKeywords(cm.CustomerID, ckm.CategoryKeywordID))
                  sb.Append("<input type='checkbox' name='Ckb" + temp[0] + "' id='" + ckm.Keyword + "' value='" + ckm.Keyword + "," + ckm.CategoryKeywordID + "," + cm.CustomerID + "' checked='checked' onclick='UpdateCustomerCategoryKeywords(this.value)' />");
              else
                  sb.Append("<input type='checkbox' name='Ckb" + temp[0] + "' id='" + ckm.Keyword + "' value='" + ckm.Keyword + "," + ckm.CategoryKeywordID + "," + cm.CustomerID + "' onclick='UpdateCustomerCategoryKeywords(this.value)' />");
              
              sb.Append("</td>");

              if (i % 12 ==11)
                  sb.Append("</tr>");

              i++;
            }

            sb.Append("</div>");


                 //<td><%=cm.Name%></td>  
                 //<td><input type="checkbox" name="CkbCategory" id="<%=cm.CategoryID%>" onclick="UpdateValues('<%=cm.Name%>')" /></td>
            return sb.ToString();
        }

        public void UpdateCustomerCategoryKeywords(string Values)
        {
            try
            {
                string[] temp = Values.Split(',');
                int CategoryKeywordID = Convert.ToInt16(temp[0]);
                int CustomerID = Convert.ToInt32(temp[1]);
                bool isIn = false;
                if (temp[2] == "1")
                    isIn = true;
                else
                    isIn = false;

                CategoryDB.UpdateCustomerCategoryKeywords(CustomerID, CategoryKeywordID, isIn);

            }
            catch (Exception e)
            {

                string s = e.Message;
            }


        }





    }
}
