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


        public string ShowCombination(string id)
        {

            string[] temp = id.Split(',');
            if (temp[1]=="0")
                return "";

            StringBuilder sb = new StringBuilder();
            sb.Append("<div id='div1'>");
            sb.Append("Keyword : <input type='text' name='customerkeyword1' id='customerkeyword1'/> include <input type='text' name='customerkeyword2' id='customerkeyword2'/> and <input type='text' name='customerkeyword3' id='customerkeyword3'/> <input type='submit' name='button' id='button' value='add' onclick='AddLine(this.value)'/>");
            sb.Append("</div>");


            return sb.ToString(); //"<table><tr><td><input type='submit' name='eee' id='eee' value='sss' onclick='AddLine(this.value)' /></td></tr></table>";
        }


        public string AddLine(string id)
        {
            Random ro = new Random();
            int result = 0;
            result = ro.Next();
            StringBuilder sb = new StringBuilder();
           // sb.Append("<br />");
            sb.Append("<div id=/div"+result.ToString()+ "/>");
            sb.Append("Keyword : <input type='text' name='customerkeyword1' id='txt" + result.ToString() + "a'/> include <input type='text' name='customerkeyword2' id='txt" + result.ToString() + "b'/> and <input type='text' name='customerkeyword3' id='txt" + result.ToString() + "c'/> <input type='submit' name='button' id='button' value='Delete' onclick='removeDiv(/div"+ result.ToString() +"/)'/> ");  //<a href='javascript:removeDiv('div" + result.ToString() + "')'>Delete</a>   <a href='javascript:removeDiv('Combination','div2')'>Delete</a>"   <a href='javascript:removeDiv('div2')'>Delete</a>
            sb.Append("</div>");

            return sb.ToString();
        }


    }
}
