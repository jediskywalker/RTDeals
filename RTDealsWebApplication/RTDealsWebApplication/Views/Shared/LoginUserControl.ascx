<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%if (HttpContext.Current.Session["Customer"] != null)
  { %>
<div class="loginout" id="LoggedIn">
    Welcome,<%= ((RTDealsWebApplication.Models.CustomerModel)HttpContext.Current.Session["Customer"]).Username%> <%= Html.ActionLink("Logout", "Logout", "Home") %>
</div>
<%}
  else
  { 
%>
<div class="loginout" id="Login">
    <%= Html.ActionLink("Login", "Index", "Login") %>
   <%-- <%= Html.ActionLink("LogOn", "LogOn", "Account") %>--%>
</div>
<%--<form action="/Login/Index" method="post">
    <div class="col2Fieldset">
        <fieldset id="loginFieldset">
            <label for="Login">
                Username:</label>
            <input type="text" id="userName" name="UserName" tabindex="1" value="" title="User Name">
            <label for="password">
                Password:</label>
            <input type="password" id="password" name="Password" tabindex="2" title="Password">
            <input type="Submit" id="Login" name="Login" value="Login" /><br />
        </fieldset>
    </div>
</form>--%>
<%  
} %>