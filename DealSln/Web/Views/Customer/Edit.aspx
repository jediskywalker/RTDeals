<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Web.Models.CategoryModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	CustomerKeywordsSetup
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">


    <h2>CustomerKeywordsSetup</h2>
      <div><h3>Category</h3> <br />
        <%foreach (Web.Models.CategoryModel cm in (List<Web.Models.CategoryModel>)ViewData["Category"]){%>
          <td><%=cm.Name%></td>  
                 <td><input type="checkbox" name="<%=cm.Name%>" id="<%=cm.CategoryID%>"  onclick="UpdateValues('<%=cm.Name%>,<%=cm.CategoryID%>')" /></td>
   
       <%} %>
      </div>
         <% using (Html.BeginForm())
       { %>
       <%foreach (Web.Models.CategoryModel cm in (List<Web.Models.CategoryModel>)ViewData["Category"]){%>
          <div id="<%=cm.Name%>"></div>

            <%} %>

                 <% } %>


         <div>
              <b>Customerize:</b>
               <br />
               Use Combination:
              <input type="checkbox"  name="ckbCombination" id="ckbCombination" value="-1" onclick="ShowCombination(this.value)"  />
              <br />
              Input Keywords seperate by ","
              <input type="text" name="CustomerKeywords" id="CustomerKeywords" />
              <div id="Combination"></div>
              <input type="submit" name="button" id="button" value="Save" />
         </div>



       <script language="javascript" type="text/javascript">
               function UpdateValues(id) {
                   var Para = id.split(',');

                   var status;
                   if (document.getElementById(Para[1]).checked)
                       status = 1;
                   else
                       status = 0;

                   var result = Para[0] + "," + status;
                   if (window.XMLHttpRequest) {
                       xmlhttp = new XMLHttpRequest();
                   }
                   else {
                       xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
                   }
                   xmlhttp.onreadystatechange = function () {
                       if (xmlhttp.readyState == 4 && xmlhttp.status == 200) {
                           document.getElementById(Para[0]).innerHTML = xmlhttp.responseText;
                       }

                   }
                   xmlhttp.open("POST", "ShowCategoryKeywords?id=" + result, true);
                   xmlhttp.send();

               }

  
        function UpdateCustomerCategoryKeywords(idd) {

            var Para = idd.split(',');

            var status;
            if (document.getElementById(Para[0]).checked)
                status = 1;
            else
                status = 0;

            var result = Para[1] + "," + Para[2] + "," + status;
            if (window.XMLHttpRequest) {
                xmlhttp = new XMLHttpRequest();

            }
            else {
                xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");

            }

            xmlhttp.open("POST", "UpdateCustomerCategoryKeywords?Values=" + result, true);
            xmlhttp.send();
        }

  
          function ShowCombination(id) {

              var status;
              if (document.getElementById('ckbCombination').checked)
                  status = 1;
              else
                  status = 0;

    
              var result = id + "," + status;
              if (window.XMLHttpRequest) {
                  xmlhttp = new XMLHttpRequest();
              }
              else {
                  xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
              }
              xmlhttp.onreadystatechange = function () {
                  if (xmlhttp.readyState == 4 && xmlhttp.status == 200) {
                      document.getElementById("Combination").innerHTML = xmlhttp.responseText;
                  }

              }
              xmlhttp.open("POST", "ShowCombination?id=" + result, true);
              xmlhttp.send();
              
          }

 
           function AddLine(id) {

               if (window.XMLHttpRequest) {
                   xmlhttp = new XMLHttpRequest();
               }
               else {
                   xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
               }
               xmlhttp.onreadystatechange = function () {
                   if (xmlhttp.readyState == 4 && xmlhttp.status == 200) {
                       document.getElementById("Combination").innerHTML += xmlhttp.responseText;
                   }

               }
               xmlhttp.open("POST", "AddLine?id=" + id, true);
               xmlhttp.send();

           }


           function removeDiv(id) {
               //alert(id);
               //var temp = id.toString();

               document.getElementById(id).style.display = 'none';
                
            
           }


    </script>


</asp:Content>
