<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<%--<script type="text/javascript">
    $(function(){  $('#Username').focus(); });
</script>--%>
    <p>
        Please enter your username and password. <%: Html.ActionLink("Register", "Register") %> if you don't have an account.
    </p>
    <h2>Login</h2>
<form id="loginForm" action="/Login/Index" method="post">


<center>
<div style="width:450px; text-align:center">
    <div class="col2Fieldset">
        <fieldset id="loginFieldset">
            <label for="Login">
                Username:</label>
            <input type="text" id="userName" name="UserName" class="focusfirst" tabindex="1" value="<%: ViewData["Username"] %>" title="User Name"/> <br />
            <label for="password">
                Password:</label>
            <input type="password" id="password" name="Password" tabindex="2" title="Password"/> <br />
            <input type="submit" id="Login" name="Login" value="Login" /><br />
            <span class="errorMessage"> <%: ViewData["Message"] %> </span>
        </fieldset>
    </div>
</div>
</center>
</form>
</asp:Content>
