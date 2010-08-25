<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<RTDealsWebApplication.Models.DealsConfigModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Edit
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Edit</h2>
    <table>
    <%foreach (RTDealsWebApplication.Models.DealsSourceModel dsm in (List<RTDealsWebApplication.Models.DealsSourceModel>)ViewData["DealSource"])
      {%>
          <tr>
             <td>
                 <%=Html.Encode(dsm.SourceName)%>
                 <input type="checkbox" name="ckbSource" id="ckbSource" />   
             </td>
          
          </tr>
         

      <%} %>

      </table>
    <form method="post" action="ProcessDeals">
    
       <input name="button" value="Send" type="submit" />
    
    
    </form>

    


</asp:Content>
