<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<RTDealsWebApplication.Models.DealsConfigModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Edit
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script src="../../Scripts/MicrosoftAjax.js" type="text/javascript"></script>
    <script src="../../Scripts/MicrosoftMvcAjax.js" type="text/javascript"></script>
    <h2>Edit</h2>
     <form method="post" action="ProcessDeals" id="frm">
    <table>
    <%foreach (RTDealsWebApplication.Models.DealsSourceModel dsm in (List<RTDealsWebApplication.Models.DealsSourceModel>)ViewData["DealSource"])
      {%>
          <tr>
             <td>
                 <%=Html.Encode(dsm.SourceName)%>
                 <input type="checkbox" name="<%=dsm.SourceID%>" id="<%=dsm.SourceID%>"  value="<%=dsm.SourceID%>" onclick="GetDealType(this.value)"/>   
            </td>  
                 <td><div id="Updates<%=dsm.SourceID%>"></div></td>
          </tr>
         

      <%} %>

    
      </table>
   
 
    <input name="button" value="Send" type="submit" />

  

  

    <script language="javascript" type="text/javascript">

        function GetDealType(id) {
            var temp =1;
            if (!document.getElementById(id.toString()).checked) {
                temp = -1;
            }
            if (window.XMLHttpRequest) {
                xmlhttp = new XMLHttpRequest();
            }
            else {
                xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
            }
            xmlhttp.onreadystatechange = function () {
                if (xmlhttp.readyState == 4 && xmlhttp.status == 200) {

                    document.getElementById("Updates"+id).innerHTML = xmlhttp.responseText;
                }
            }
             xmlhttp.open("POST", "GetDealType?id=" + id*temp, true);
             xmlhttp.send();
        }


    </script>

      </form>


</asp:Content>
