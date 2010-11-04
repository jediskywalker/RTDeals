<%@ Page Title="" Language="C#"  MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<RTDealsWebApplication.Models.CustomerModel>" %>
 <%@ Register TagPrefix="recaptcha" Namespace="Recaptcha" Assembly="Recaptcha" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Register
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<script runat="server">
    void btnSubmit_Click(object sender, EventArgs args)
    {
        if (Page.IsValid) {
            lblResult.Text = "You Got It!";
            lblResult.ForeColor = System.Drawing.Color.Green;
        } else {
            lblResult.Text = "Incorrect";
            lblResult.ForeColor = System.Drawing.Color.Red;
        }
    }
</script>
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
            <form  runat="server"> 
             <asp:Label Visible="true" ID="lblResult" runat="server" Text="123" />
             <recaptcha:RecaptchaControl ID="recaptcha" runat="server" Theme="blue" PublicKey="6Ld6WL4SAAAAAHvD-48HpJYeu-sK_vwjMYOmn29-"     PrivateKey="6Ld6WL4SAAAAANVP9bYst-NzoN87Eb-UxKUUY_bE"     />
          
  
            <p>
                <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
               <%-- <input type="submit" value="Create"  onclick="btnSubmit_Click" />--%>
            </p>
            </form> 
        </fieldset>

    <% } %>

    <div>
        <%: Html.ActionLink("Back to List", "Index") %>
    </div>


</asp:Content>

