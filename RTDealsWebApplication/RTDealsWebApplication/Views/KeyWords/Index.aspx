<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<RTDealsWebApplication.Models.CategoryModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script src="../../Scripts/MicrosoftAjax.js" type="text/javascript"></script>
    <script src="../../Scripts/MicrosoftMvcAjax.js" type="text/javascript"></script>
    <h2>Index</h2>

    <table>
     <% using (Html.BeginForm())
        { %>
 
     <%int i=0; %>
    <%foreach (RTDealsWebApplication.Models.CategoryModel cm in (List<RTDealsWebApplication.Models.CategoryModel>)ViewData["Category"])
      {%>
        <%if (i % 10 == 0)
          { %>
          <tr>
          <%} %>
              <td><%=Html.Encode(cm.Name)%></td> 
              <td><input type="checkbox" name="<%=cm.CategoryID%>" id="<%=cm.CategoryID%>"  value="<%=cm.CategoryID%>" onclick="javascript:window.open('/Keywords/edit/<%=cm.CategoryID%>','_blank')"/></td>  
              <%--   <td><div id="Updates<%=dsm.SourceID%>"></div></td>--%>
 <%if (i % 10 ==9)
   { %>
          </tr>
         <%} %>
    <%i++; %>
      <%} %>

   </table>
   <input type="text" name="Category" id="Category" />
   <input  type="submit" name="button" id="button" value="Save"/>
         <%} %>
</asp:Content>
