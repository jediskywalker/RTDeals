<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Select Delivery Plan
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<script language="javascript" type="text/javascript" src="<%=Url.Content("~/Scripts/MicrosoftAjax.js") %>"></script>
<script language="javascript" type="text/javascript" src="<%=Url.Content("~/Scripts/MicrosoftMvcAjax.js") %>"></script>
<script language="javascript" type="text/javascript">

    function setcolor(id) {
        if (id == 0) {
         for (var i = 1; i < 8; i++) {
             var cellida = "td" + i;
            document.getElementById(cellida).style.backgroundColor = "green";
            }
        }
        
        if (id > 0) {
            var cellid = "td" + id;
            var current = document.getElementById(cellid).style.backgroundColor;
            if (current != "green")
                document.getElementById(cellid).style.backgroundColor = "green";
            else
                document.getElementById(cellid).style.backgroundColor = "#fff";
        }

    }

    function showtxt(value){
        if (value == -1) {
           
            document.getElementById('txtminutes').style.visibility = "visible";
        } else {
            
            document.getElementById('txtminutes').style.visibility = "hidden"; 
        }
        document.getElementById('realtime').checked = true;
        timeoptionshowhide(1);
    }
    function showtxt2() {
        document.getElementById('fixedtime').checked = true;
        timeoptionshowhide(0);
    }
    function timeoptionshowhide(value) {
        if (value == 1) {
            var items = new Array();
            items = document.getElementsByName("fixed");
            for (var i = 0; i < items.length; i++) {
                items[i].disabled = true;
            }
            document.getElementById('txtminutes').disabled = false;
            document.getElementById('sDeliveryInterval').disabled = false;
        }
        if (value == 0) {
            var items2 = new Array();
            items2 = document.getElementsByName("fixed");
            for (var j = 0; j < items2.length; j++) {
                items2[j].disabled = false;
            }
            document.getElementById('txtminutes').disabled = true;
            document.getElementById('sDeliveryInterval').disabled = true;
        }
    }

    function movetime() {
        var time = document.getElementById("hr").value + ":" + document.getElementById("mi").value + " " + document.getElementById("ma").value;
        if (document.getElementById("personalTime").length < 5) {
            AddItem(document.getElementById("personalTime"), time, time);
        } else if (document.getElementById("personalTime").length == 5) {
        alert("MAX 5 times allowed, double click to remove");
        }
        showtxt2();
    }

    function removetime(selected) {
        RemoveItem(document.getElementById("personalTime"), selected);
    }

    function RemoveItem(objListBox, strId) {

        var intIndex = GetItemIndex(objListBox, strId);
        if (intIndex != -1)
            objListBox.remove(intIndex);

         
    }

    // need do a sorting here?    
    function AddItem(objListBox, strText, strId) {
        var newOpt;
        newOpt = document.createElement("OPTION");
        newOpt = new Option(strText, strText);
        newOpt.id = strId;
        objListBox.add(newOpt);      

    }

    function GetItemIndex(objListBox, strId) {
        for (var i = 0; i < objListBox.children.length; i++) {
            var strCurrentValueId = objListBox.children[i].id;
            if (strId == strCurrentValueId) {
                return i;
            }
        }
        return -1;
    }

    function validation() {

        // week days.....
        var weekdays = '';
        for (var i = 1; i < 8; i++) {
            var cellid = "td" + i;
            var current = document.getElementById(cellid).style.backgroundColor;
            if (current == "green") {
                document.getElementById(cellid).style.backgroundColor = "green";
                weekdays += ',' + i;
            }
        }
        if (weekdays == '') {
            alert("Need select at least one week day");
            return false;
        }
        // week days end

        var radioRT = document.getElementById("realtime").checked;
        var radioFT = document.getElementById("fixedtime").checked;
        //alert(radioRT + ' ' + radioFT);
        if (radioFT == false && radioRT == false) {
            alert("Need select a time plan");
            return false;
        }

        var tmpinterval=0;
        if (radioRT == true) {  // real time
            if (document.getElementById("sDeliveryInterval").value == -1) {
                tmpinterval = document.getElementById("txtminutes").value;

                var re = new RegExp('[0-9]+');
                if (tmpinterval.match(re)) {
                    //  alert("Successful match");
                } else {
                    alert("ONLY accept numbers for delivery interval");
                    return false;
                }
            } else {
            tmpinterval = document.getElementById("sDeliveryInterval").value;
            }
        }

        var times='';
        if (radioFT == true) {  // fixed time
            var tmpcnt = document.getElementById("personalTime").length;
            if (tmpcnt == 0) {
                alert("Need select at least one delivery time");
                return;
            }
            for (var i = 0; i < tmpcnt; i++) {
                times += ',' + document.getElementById("personalTime").children[i].id;
            }
        }
        // nightpause ?
        var np = document.getElementById("nightpause").checked;
       // alert(np);
        if (window.XMLHttpRequest) {
            xmlhttp = new XMLHttpRequest();
        }
        else {
            xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
        }
        
        xmlhttp.onreadystatechange = function () {
            if (xmlhttp.readyState == 4 && xmlhttp.status == 200) {               
                alert(xmlhttp.responseText);
            }
        }

        xmlhttp.open("POST", "DeliveryPlan/InsertUpdateSchedule?weekdays=" + weekdays + "&tmpinterval=" + tmpinterval + "&times=" + times + "&np=" + np, true);
        xmlhttp.send();

    }
   

</script>


    <h2>Delivery Time Plan</h2>

    <form id="ddeliveryplan" runat="server">

    <div id="weekday">
    <table>
   
   <tr>
   <td colspan="8"> Please select the day you want receive alert.</td>
   </tr>

    <tr height="30px">
        <td><input type="button" value="ALl Week Days" onclick="setcolor(0)" />  </td>
         <td id="td1" name="weekday"> <input type="button" value="Sunday" onclick="setcolor(1)" /> </td>
         <td id="td2" name="weekday"><input type="button" value="Monday" onclick="setcolor(2)" /> </td> 
         <td id="td3" name="weekday"><input type="button" value="Tuesday" onclick="setcolor(3)" /></td>
         <td id="td4" name="weekday"><input type="button" value="Wednesday"  onclick="setcolor(4)"/></td>
         <td id="td5" name="weekday"> <input type="button" value="Thursday" onclick="setcolor(5)" /></td> 
         <td id="td6" name="weekday"> <input type="button" value="Friday" onclick="setcolor(6)" /></td>
         <td id="td7" name="weekday"> <input type="button" value="Saturday" onclick="setcolor(7)" /></td>  </tr>
    </table>
    
    </div>
    <br />
    <br />
    <br />
    <br />

    <div id="Time">
    <table>
    <tr>
    <td>
        Please select the time <br/> you want receive the <br/>alert  on your selected day. <br/>

    </td>


    <td>
        <table>
        <tr>
        <td>
        <input type="radio" name="deliveryTime" id="realtime" value="1" onclick="timeoptionshowhide(this.value)"   />RealTime Delivery <br/>
            &nbsp &nbsp  &nbsp &nbsp        <select id="sDeliveryInterval" onchange="showtxt(this.value)"  >      
                    <option value="0">Without Delay</option>
                    <option value="15">Every 15 minutes</option>
                    <option value="30">Every 30 minutes</option>                   
                    <option value="60">Every one hour</option>
                    <option value="120">Every two hours</option>
                    <option value="-1">Define your own (minutes)</option>
                    </select>
                    <br/>
                    <br/>
                 &nbsp &nbsp  &nbsp &nbsp      <input type="text" id="txtminutes" style=" visibility:hidden" value="input your minutes here" />
                 <br/>
                 <br/>

        </td>
        </tr>
        
        <tr>
        <td>
        <input type="radio" name="deliveryTime" id="fixedtime" value="0" onclick="timeoptionshowhide(this.value)"  />Fixed Time <br/>
              &nbsp &nbsp  &nbsp &nbsp           
              <select id="hr" name="fixed" onclick="showtxt2()">                
                <option value="01">01</option>
                <option value="02">02</option>
                <option value="03">03</option>
                <option value="04">04</option>
                <option value="05">05</option>
                <option value="06">06</option>
                <option value="07">07</option>
                <option value="08">08</option>
                <option value="09">09</option>
                <option value="10">10</option>
                <option value="11">11</option>
                <option value="12">12</option>
              </select>

              <select id="mi" name="fixed" onclick="showtxt2()">
<option value="00">00</option>
<option value="01">01</option>
<option value="02">02</option>
<option value="03">03</option>
<option value="04">04</option>
<option value="05">05</option>
<option value="06">06</option>
<option value="07">07</option>
<option value="08">08</option>
<option value="09">09</option>
<option value="10">10</option>
<option value="11">11</option>
<option value="12">12</option>
<option value="13">13</option>
<option value="14">14</option>
<option value="15">15</option>
<option value="16">16</option>
<option value="17">17</option>
<option value="18">18</option>
<option value="19">19</option>
<option value="20">20</option>
<option value="21">21</option>
<option value="22">22</option>
<option value="23">23</option>
<option value="24">24</option>
<option value="25">25</option>
<option value="26">26</option>
<option value="27">27</option>
<option value="28">28</option>
<option value="29">29</option>
<option value="30">30</option>
<option value="31">31</option>
<option value="32">32</option>
<option value="33">33</option>
<option value="34">34</option>
<option value="35">35</option>
<option value="36">36</option>
<option value="37">37</option>
<option value="38">38</option>
<option value="39">39</option>
<option value="40">40</option>
<option value="41">41</option>
<option value="42">42</option>
<option value="43">43</option>
<option value="44">44</option>
<option value="45">45</option>
<option value="46">46</option>
<option value="47">47</option>
<option value="48">48</option>
<option value="49">49</option>
<option value="50">50</option>
<option value="51">51</option>
<option value="52">52</option>
<option value="53">53</option>
<option value="54">54</option>
<option value="55">55</option>
<option value="56">56</option>
<option value="57">57</option>
<option value="58">58</option>
<option value="59">59</option>
</select>
              
              <select id="ma" name="fixed" onclick="showtxt2()">
                <option value="AM">AM</option>
                <option value="PM">PM</option>
              </select> 
              <input type="button" value="add (upto 5)" onclick="movetime()" name="fixed"/> <br/><br/>
              &nbsp &nbsp  &nbsp &nbsp   &nbsp &nbsp  &nbsp &nbsp   
              <select name="drop1" id="personalTime" size="5" multiple="multiple" style=" height:100px; width:100px" 
              ondblclick="removetime(this.value)" name="fixed" onclick="showtxt2()">
    
               </select>

                    <br/>
                    <br/>
                 &nbsp &nbsp  &nbsp &nbsp      
                 <br/>
                 <br/>

        </td>
        </tr>
        </table>
    </td>
    
    </tr>
    
    </table>
    
    </div>

    <input type="checkbox" id="nightpause"  /> By checking this, we won't send alert to you after 11:00PM till next day 7:00AM <br/>

    <input type="button" value="Save" onclick="validation()" />

  </form>

</asp:Content>
