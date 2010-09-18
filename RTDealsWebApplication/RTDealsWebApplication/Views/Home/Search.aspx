<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<RTDealsWebApplication.Models.HomePageSearchModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Search Result
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Search Result</h2>
    
<table>
     <%foreach (RTDealsWebApplication.Models.HomePageSearchModel hpsm in (List<RTDealsWebApplication.Models.HomePageSearchModel>)ViewData["SearchResult"])
         { %>
            <tr>
               <td><a href="<%=Html.Encode(hpsm.URL)%>"><%=Html.Encode(hpsm.Title)%></a> <span style="color:Gray"><%=Html.Encode(hpsm.InTime)%></span></td>
            </tr>

            
         <%} %>
 
 
</table>
</asp:Content>
