<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<form action="/home/search" method="post">
<title>iDeals</title>
<meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
<link href="../../Content/style.css" rel="stylesheet" type="text/css" />
<script src="../../Scripts/MicrosoftAjax.js" type="text/javascript"></script>
<script src="../../Scripts/MicrosoftMvcAjax.js" type="text/javascript"></script>

<div class="main">
  
  <div class="header">
    <div class="header_resize">
      <div class="header_top_text">&nbsp;</div>
    <div class="logo"><img src="../../images/logo.gif" width="374" height="143" border="0" alt="logo" /></div>
 <%--  <div class="login">  
       <a href="login.html"><img src="images/login.gif" border="0" alt="login"  /></a>
      </div>--%>
      <div class="clr"></div>
    </div>
  </div>
  <div class="clr"></div>
  
        <table align="center">
           <tr>
             <td><input type="text" id="keyword" name="keyword" value=" Find your deals here i.e. camera" style="width:700px;height:30px" onfocus="if (this.value==this.defaultValue) {this.value=''; this.style.color='#2b2f30';}" class="text" /></td> 
              <td><input type="submit" name="button" id="button" value="Search" /></td>
           <%--  <td><img src="../../images/search.gif" width="141" height="47" border="0" alt="logo" /></td> --%>
           </tr>
        </table>
     
 
   



  <div class="clr"></div>

<%--<div class="footer">
  <div class="footer_resize">
    <div class="textt">
      <p><a href="#">Home</a> | <a href="#">About us</a> | <a href="#">Contact us</a> | <a href="#">Support </a></p>
    </div>
    <div class="loggo"> <a href="#"><img src="images/footer_logo.gif" alt="picture" width="212" height="93" border="0" /></a> </div>
    <div class="textt2">
       <p>© Copyright 2010. Real Time Deals. All Rights Reserved </p>
    </div>
  </div>
  <div class="clr"></div>
</div>--%>
</form>
   
</asp:Content>
