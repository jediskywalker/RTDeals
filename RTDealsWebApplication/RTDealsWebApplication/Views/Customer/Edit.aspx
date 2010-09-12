<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<RTDealsWebApplication.Models.CategoryModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	CustomerKeywordsSetup
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">


    <h2>CustomerKeywordsSetup</h2>
      <div><h3>Category</h3> <br />
        <%foreach (RTDealsWebApplication.Models.CategoryModel cm in (List<RTDealsWebApplication.Models.CategoryModel>)ViewData["Category"]){%>
          <td><%=cm.Name%></td>  
                 <td><input type="checkbox" name="<%=cm.Name%>" id="<%=cm.CategoryID%>"  onclick="UpdateValues('<%=cm.Name%>,<%=cm.CategoryID%>')" /></td>
            

  
     
     
       <%} %>
    
      </div>
         <% using (Html.BeginForm())
       { %>
       <%foreach (RTDealsWebApplication.Models.CategoryModel cm in (List<RTDealsWebApplication.Models.CategoryModel>)ViewData["Category"]){%>
          <div id="<%=cm.Name%>"></div>

            <%} %>

                 <% } %>
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

    </script>



    <script language="javascript" type="text/javascript">

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



    </script>








</asp:Content>
