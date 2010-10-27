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
                <%: Html.LabelFor(model => model.Username) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.Username) %>
                <%: Html.ValidationMessageFor(model => model.Username) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.LastName) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.LastName) %>
                <%: Html.ValidationMessageFor(model => model.LastName) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.FirstName) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.FirstName) %>
                <%: Html.ValidationMessageFor(model => model.FirstName) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.Password) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.Password) %>
                <%: Html.ValidationMessageFor(model => model.Password) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.Address1) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.Address1) %>
                <%: Html.ValidationMessageFor(model => model.Address1) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.Address2) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.Address2) %>
                <%: Html.ValidationMessageFor(model => model.Address2) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.Phone) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.Phone) %>
                <%: Html.ValidationMessageFor(model => model.Phone) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.State) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.State) %>
                <%: Html.ValidationMessageFor(model => model.State)%>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.ZipCode)%>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.ZipCode)%>
                <%: Html.ValidationMessageFor(model => model.ZipCode)%>
            </div>
  
            <div class="editor-label">
                <%: Html.LabelFor(model => model.Email)%>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.Email)%>
                <%: Html.ValidationMessageFor(model => model.Email)%>
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

