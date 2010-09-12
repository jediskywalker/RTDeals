using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RTDealsWebApplication.DBAccess;
using RTDealsWebApplication.Models;
using System.Text;


namespace RTDealsWebApplication.Controllers
{
    public class CategoryController : Controller
    {
        //
        // GET: /Category/

        public ActionResult Index(string button)
        {
           // ViewData["CategoryKeywords"] = CategoryDB.GetCategoryKeywordsByID(1);
          //  ViewData["category"] = CategoryDB.GetCategory();

            if (button == "New")
                return RedirectToAction("Create");


            return View();
        }

        //
        // GET: /Category/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Category/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Category/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection,string Name)
        {
            try
            {
                // TODO: Add insert logic here


                CategoryModel cm = new CategoryModel();
                cm.Name = Name;
                CategoryDB.InsertCategory(cm);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        //
        // GET: /Category/Edit/5
 
        public ActionResult Edit(int? id)
        {

            return View();
        }

        //
        // POST: /Category/Edit/5

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
        // GET: /Category/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }


        public string GetCategoryKeywords(int id)  // Dynamiclly show sub menus
        {
            ViewData["CategoryKeywords"] = CategoryDB.GetCategoryKeywordsByID(id);

            return "";
        }



        public string BuildCategoryPage(int id)
        {

            StringBuilder sb = new StringBuilder();
            List<CategoryModel> lcm = CategoryDB.GetCategory();
            sb.Append("<table>");
            sb.Append("<ajaxToolkit:ToolkitScriptManager runat='Server' ID='ScriptManager1' CombineScripts='false' EnablePartialRendering='true' ScriptMode='Release' />");
            foreach (CategoryModel cm in lcm)
            {
                List<CategoryKeywordsModel> lckm = CategoryDB.GetCategoryKeywordsByID(cm.CategoryID);
               // sb.Append("<tr><td>" + cm.Name);
                //sb.Append("<input type='checkbox' name='"+cm.Name+ "' id='"+cm.Name+"' value='" +cm.ID+ "'</td></tr>");
                sb.Append("<tr><td><input type='button' name='" + cm.Name + "' id='" + cm.Name + "' value='" + cm.Name + "'/></td><tr> ");
                sb.Append("<asp:Panel ID='Panel1"+cm.Name+"' runat='server' Style='display: none; background-color:#F0F0F0'> ");
                sb.Append("<asp:Panel ID='Panel3"+cm.Name+"' Height='20px' runat='server' Style='cursor:move;background-color:#DDDDDD;border:solid 1px Gray;color:Black'> ");
                sb.Append("<div>Choose keywords:</div> </asp:Panel> ");
                sb.Append("<div> ");
                foreach(CategoryKeywordsModel ckm in lckm)
                {
                    sb.Append("<td> ");
                    sb.Append("<input type='checkbox' name='"+ckm.Keyword +"' id='"+ ckm.Keyword + "' value='"+ckm.Keyword +"'/> ");
                    sb.Append(ckm.Keyword + "</td> ");

                }
                sb.Append("</div></asp:Panel> ");        
                sb.Append("<ajaxToolkit:ModalPopupExtender ID='ModalPopupExtender"+cm.Name +"' runat='server' "); 
                sb.Append("TargetControlID='"+cm.Name +"' "); 
                sb.Append("PopupControlID='Panel1" +cm.Name +"' "); 
                sb.Append("OnOkScript='onOk()' ");
                sb.Append("DropShadow='true' ");
                sb.Append("PopupDragHandleControlID='Panel3"+cm.Name +"' ");
                sb.Append("BackgroundCssClass='modalBackground' /> ");
                sb.Append("</td></tr> ");

            }
           
            sb.Append("</table>");

            string result = sb.ToString();

            return result;
        }









        //
        // POST: /Category/Delete/5

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
