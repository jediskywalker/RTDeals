<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<RTDealsWebApplication.Models.CategoryModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Create
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Create</h2>
    <form action="Create" method="post">
    <table> 
    <th>Name:</th><th><input type="text" name="name" id="name"/></th>
     <th><input type="submit" name="button" value="Create" /></th>

    </table>
    </form>
</asp:Content>

