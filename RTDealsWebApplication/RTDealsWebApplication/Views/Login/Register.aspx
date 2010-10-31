<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<RTDealsWebApplication.Models.CustomerModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Register
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Register</h2>

    <% using (Html.BeginForm()) {%>
        <%: Html.ValidationSummary(true) %>

        <fieldset>
            <legend>Customer Info</legend>
   
            <div class="editor-label">
               <%=Html.Label("Email")%><%=Html.TextBox("txtEmail")%>
            </div>
             <div class="editor-label">
               <%=Html.Label("Confirm Email")%><%=Html.TextBox("txtConfirmEmail")%>
            </div>
             <div class="editor-label">
               <%=Html.Label("Password")%><%=Html.TextBox("txtPassword")%>
            </div>
            <div class="editor-label">
               <%=Html.Label("Confirm Password")%><%=Html.TextBox("txtConfirmPassword")%>
            </div>
          
            
          
  
            <p>
                <input type="submit" value="Create" />
            </p>
        </fieldset>

    <% } %>

    <div>
        <%: Html.ActionLink("Back to List", "Index") %>
    </div>

</asp:Content>

