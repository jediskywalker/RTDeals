<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Web.Models.CategoryKeywordsModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	GetCateKeywords
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>GetCateKeywords</h2>
    <script src="../../Scripts/MicrosoftAjax.js" type="text/javascript"></script>
    <script src="../../Scripts/MicrosoftMvcAjax.js" type="text/javascript"></script>
 <form action="edit" method="post">
     <table>
     <%int i=0; %>
     <%int SelectedCategoryID = 0; %>
    <%foreach (Web.Models.CategoryKeywordsModel ckm in (List<Web.Models.CategoryKeywordsModel>)ViewData["CategoryKeywords"])
      {%>
      <%SelectedCategoryID = ckm.CategoryID; %>
        <%if (i % 10== 0)
          { %>
          <tr>
          <%} %>
             <td><%=Html.Encode(ckm.Keyword)%></td> 
              <td><input type="checkbox" name="<%=ckm.CategoryID%>" id="<%=ckm.CategoryID%>" value="<%=ckm.CategoryID%>"/></td>  
              <%--   <td><div id="Updates<%=dsm.SourceID%>"></div></td>--%>
 <%if (i % 10== 9 )
   {  %>
          </tr>
         <%} %>
    <%i++;%>
      <%} %>

       </table>
  <input type="text" id="N" name="N"/>
<%--   <input type="submit" id="N" name="N" value="New"  onclick="CreateNew(<%=SelectedCategoryID%>)"/>--%>
   <input type="submit" id="S" name="S" value="Save"  />

   <div id="Updates1"></div>

         <script language="javascript" type="text/javascript">
             function UpdateValues(id) {

                 if (window.XMLHttpRequest) {
                     xmlhttp = new XMLHttpRequest();
                 }
                 else {
                     xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");

                 }
                 alert(id);
                 xmlhttp.open("POST", "UpdateCategoryKeywords?id=" + id, true);
                 xmlhttp.send();
             }

         </script>

         </form>
<%--
       <script language="javascript" type="text/javascript">

           function CreateNew(id) {
               alert(<%=SelectedCategoryID%>);
               //alert(id);
               if (window.XMLHttpRequest) {
                   xmlhttp = new XMLHttpRequest();
               }
               else {
                   xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
               }
               xmlhttp.onreadystatechange = function () {
                   if (xmlhttp.readyState == 4 && xmlhttp.status == 200) {

                       document.getElementById("Updates1").innerHTML = xmlhttp.responseText;
                   }
               }
               xmlhttp.open("POST", "ShowCreateDiv?id=" + id, true);
               xmlhttp.send();
           }

      </script>
--%>



 
</asp:Content>
