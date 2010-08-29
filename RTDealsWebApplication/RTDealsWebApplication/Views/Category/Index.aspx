<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<RTDealsWebApplication.Models.CategoryModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Index</h2>

    <form action="category" method="post">
     <table>
       <%foreach (RTDealsWebApplication.Models.CategoryModel cm in (List<RTDealsWebApplication.Models.CategoryModel>)ViewData["Category"])
      {%>
          <tr>
             <td>
                 <%=Html.Encode(cm.Name)%>
                 <input type="checkbox" name="<%=cm.ID%>" id="<%=cm.ID%>"  value="<%=cm.ID%>" onclick="GetDealType(this.value)"/>   
            </td>  
                 <td><div id="Updates<%=cm.ID%>"></div></td>
          </tr>
         

      <%} %>


      </table>
      <input type="submit" name="button" value="New"/>
      </form>
</asp:Content>
