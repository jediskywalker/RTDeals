<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<RTDealsWebApplication.Models.CategoryModel>" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script src="../../Scripts/MicrosoftAjax.js" type="text/javascript"></script>
    <script src="../../Scripts/MicrosoftMvcAjax.js" type="text/javascript"></script>
    <h2>Edit</h2>
     <form method="post" action="ProcessDeals" id="frm">
 
 
      <input type="checkbox" name="123" id="Checkbox1"  value="1" onclick="GetDealType(this.value)"/>

      <div id="Updates1"></div>

  

    <script language="javascript" type="text/javascript">

        function GetDealType(id) {

            alert(1);
            if (window.XMLHttpRequest) {
                xmlhttp = new XMLHttpRequest();
            }
            else {
                xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
            }
            xmlhttp.onreadystatechange = function () {
                if (xmlhttp.readyState == 4 && xmlhttp.status == 200) {

                    document.getElementById("Updates1").innerHTML = xmlhttp.responseText;
                }
            }
            xmlhttp.open("POST", "BuildCategoryPage?id=" + id, true);
            xmlhttp.send();
        }


    </script>

      </form>
</asp:Content>
