<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<RTDealsWebApplication.DBAccess.CustomerDB>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<%--<form action="customer" method="post">  
    <h2>Index</h2>
    <table>
       <tr>
            <td>Username</td>
           <td>First Name</td>
           <td>Last Name</td>
       </tr>
       <%foreach (RTDealsWebApplication.Models.CustomerModel cm in (List<RTDealsWebApplication.Models.CustomerModel>)ViewData["Customer"])
         { %>
            <tr>
                <td><%=Html.Encode(cm.Username)%></td>
                <td><%=Html.Encode(cm.FirstName)%></td>
                <td><%=Html.Encode(cm.LastName)%></td>
            </tr>

            
         <%} %>
    </table>
    <input  type="submit" name="button" value="Submit"/>
    </form>--%>
    <%RTDealsWebApplication.Models.CustomerModel cm = (RTDealsWebApplication.Models.CustomerModel)ViewData["LoginCustomer"];%>
    <a href="javascript:window.open('/Customer/edit/<%=cm.CustomerID%>','_blank')">Config Keywords</a>
</asp:Content>
