<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true"
    CodeBehind="WebForm1.aspx.cs" Inherits="ResourceMerge.DemoWebApp.WebForm1" EnableViewState="false"  %>

<%@ Register Src="WebUserControl1.ascx" TagName="WebUserControl1" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <ResourceMerge:RawResource ID="RawResource11" runat="server" Url="~/resource/2.css"/>
  
<div class="btn_blue">sdfsfsdf</div>
    <resourcemerge:rawresource id="RawResource1" runat="server" url="http://images001.5173cdn.com/css/header/header_v32.css" IsMerge="false" >
    </resourcemerge:rawresource>
    <ResourceMerge:RawResource ID="RawResource2" runat="server" Url="http://images001.5173cdn.com/css/index/index_v32.css" IsMerge="false" 
        ResourceType="Style">
    </ResourceMerge:RawResource>
    <ResourceMerge:RawResource ID="RawResource3" runat="server" Url="http://images.5173cdn.com/JS/JScript/PagesScript/www/LoginMz.js" 
        Charset="gb2312" ResourceType="Script">
    </ResourceMerge:RawResource>
    <ResourceMerge:RawResource ID="RawResource4" runat="server" Url="http://images.5173cdn.com/JS/JScript/PagesScript/www/DefaultNewV1.js" Charset="gb2312"
        ResourceType="Script">
    </ResourceMerge:RawResource>
    <ResourceMerge:RawResource ID="RawResource5" runat="server" Url="http://images.5173cdn.com/5173/js/layer/layer31.js"
        ResourceType="Script">
    </ResourceMerge:RawResource>
    <ResourceMerge:RawResource ID="RawResource6" runat="server" Url="http://images001.5173cdn.com/js/jquery.1.3.2.js"     RenderLocation="FormTop" IsMerge="false" 
        ResourceType="Script">
    </ResourceMerge:RawResource>
    <ResourceMerge:RawResource ID="RawResource7" runat="server" Url="http://images001.5173cdn.com/js/my5173/v3/1m/site_sms_messages.js"
        ResourceType="Script">
    </ResourceMerge:RawResource>
    <ResourceMerge:RawResource ID="RawResource8" runat="server" Url="http://images.5173cdn.com/js/jScript/SearchBar/Version_3.05/SearchCheckKefu.js"
        Charset="gb2312" ResourceType="Script">
    </ResourceMerge:RawResource>
    <ResourceMerge:RawResource ID="RawResource9" runat="server" Url="http://images.5173cdn.com/JS/JScript/PagesScript/www/DefaultNewV1.js"
        ResourceType="Script">
    </ResourceMerge:RawResource>
    <uc1:WebUserControl1 ID="WebUserControl11" runat="server" />
        <ResourceMerge:RawResource ID="RawResource12" runat="server" Url="~/resource/4.css"/>
    <ResourceMerge:RawResource ID="RawResource10" runat="server" ResourceType="Script" RenderPriority="100">
    
//输入的是否是价格数字
function Check_Digital(ConID) {
    // 正则表达式对象
    var re = new RegExp("(^[\\d]+\\.[\\d]{1,2}$)|([\\d]+$)", "g");
    var num=document.getElementById(ConID).value;

    // 验证是否刚好匹配
    if(num.match(re)==num||num=="") {
        return true
    } else {
        alert("请输入正确的价格");
        document.getElementById(ConID).value="";
        return false;
    }
}

//线上交易行数据是否为数字
function Check_Number(ConID,name) {
    // 正则表达式对象
    var re = new RegExp("(^[\\d]+\\.[\\d]{1,2}$)|([\\d]+$)", "g");
    var num=document.getElementById(ConID).value;

    // 验证是否刚好匹配
    if(num.match(re)==num||num=="") {
        return true
    } else {
        alert("请输入正确的 " + name + " 数值" );
        document.getElementById(ConID).value="";
        return false;
    }
}

//提交
function submit() {
    var url=document.getElementById('TradingSieve_hd_url').value;
    var sql_section="";
    
    
    if(document.getElementById("TradingSieve_ckb_Honestry")) {
        if(document.getElementById("TradingSieve_ckb_Honestry").checked) {
            url+="&hn=true";
            sql_section=" and SumLockedImpawn > 0 ";
        }
    }
      // 新增闪电发货
    if(document.getElementById("TradingSieve_ckb_Quick")) {
        if(document.getElementById("TradingSieve_ckb_Quick").checked) {
            url+="&qf=true";
            sql_section+="and qf = 1 ";
        }
    }
    // 新增打折账号
    if(document.getElementById("TradingSieve_ckb_Discount")) {
        if(document.getElementById("TradingSieve_ckb_Discount").checked) {
            url+="&dc=true";
        }
    }
    // 新增付费审核
    if(document.getElementById("TradingSieve_ckb_IsPaidForAuditing")) {
        if(document.getElementById("TradingSieve_ckb_IsPaidForAuditing").checked) {
            url+="&pa=true";
        }
    }
    //新增角色搜索 
    if(parseInt("0") != 0)
    {   
        var roleCount = parseInt("0");
        var controlValue = "";
        var count = 0;
        var rc = "";
        for(var i =0; i< roleCount ;i++)
        {   
            //var controlName = "RoleInfoInput" + i ;
            controlValue = Trim(document.getElementById("RoleInfoInput" + i).value);
            if(controlValue != "")
            {   
                rc +=  controlValue + "_";
                sql_section+=" and name like '%" + controlValue.replace("'","") + "%' ";
                count ++;
            }   
        }
        
        if(count != 0)
            url += "&rc=" + escape(rc.substring(0,rc.length-1));
    }
   
    // 新增线上交易行
    if(parseInt("0") != 0)
    {
        var itemCount = parseInt("0");
        var count = 0;
        var name = "";
        var searchColumn = "";
        var lp = 0;
        var hp = 0;
        var gi = "";
        for(var i = 0; i < itemCount; i++)
        {   
            name = document.getElementById("prop" + i + "_tag").innerHTML;
            searchColumn = document.getElementById("prop" + i + "_sp").value;
            lp = Trim(document.getElementById("prop" + i + "_lp").value);
            hp = Trim(document.getElementById("prop" + i + "_hp").value);
            if(lp !=""&& hp != "")
            {   
                if(Check_Number("prop" + i + "_lp",name) && Check_Number("prop" + i + "_hp",name))
                {
                    lp = parseInt(Trim(document.getElementById("prop" + i + "_lp").value));
                    hp = parseInt(Trim(document.getElementById("prop" + i + "_hp").value));
                    if(lp<hp)
                    {
                        gi += searchColumn + "_" + lp + "_" + hp + "|";
                    }
                    else if(lp>hp)
                    {
                        gi += searchColumn + "_" + hp + "_" + lp + "|";
                    }
                    else
                    {
                        gi +=  searchColumn + "_" + lp + "_" + (hp+1) + "|";
                    }
                    count++;
                }
                else
                    return;
            }
            else if ((lp=="" && hp!="") || (hp=="" && lp!="")) 
            {
                alert("请将 "+ name +" 范围填写完整");
                return;
            }    
        }
        ;
        if(count != 0)
            url += "&gi=" + escape(gi.substring(0,gi.length-1));
        
    }
    

    if(document.getElementById("TradingSieve_ckb_Transfer")) {
        if(document.getElementById("TradingSieve_ckb_Transfer").checked) {
            url+="&tf=true";
            sql_section+="and IsRevisableByRegInfo ='true'";
        }
    }
  
    
    var gameRaceId = document.getElementById("TradingSieve_ddlGameRace").value;
    if (gameRaceId != "-1") {
        url += "&grid=" + gameRaceId;
        sql_section += " and GameRaceId = '" + gameRaceId + "' ";
    }
     // 新增小额交易
    if(document.getElementById("TradingSieve_ckb_Cheap"))
    {
        if(document.getElementById("TradingSieve_ckb_Cheap").checked) 
            url+="&hp=50&lp=0";
        else
        {
            var num1= document.getElementById("TradingSieve_txt_Price1").value;
            var num2= document.getElementById("TradingSieve_txt_Price2").value;
            if(num1!=""&&num2!="") {
            if(Check_Digital("TradingSieve_txt_Price1")&&Check_Digital("TradingSieve_txt_Price2")) {
                    if(parseFloat(num1)>parseFloat(num2)) {
                url+="&hp="+num1+"&lp="+num2;
                sql_section+="and UnitSalePrice between "+num1+" and "+num2;
            } else if(parseFloat(num1)<parseFloat(num2)) {
                url+="&hp="+num2+"&lp="+num1;
                sql_section+="and UnitSalePrice between "+num1+" and "+num2;
            } else {
                alert("价格范围不能相同!");
                document.getElementById("TradingSieve_txt_Price1").value="";
                return;
            }
        }
     } else if((num1=="" && num2!="") || (num2=="" && num1!="")) {
        alert("请将价格范围填写完整");
        return;
     }
        }
    }
     
     
     document.cookie="sql_section="+sql_section+";";
     //document.forms.item(0).action=url;
     //document.forms(0).submit();
     window.open(url,'_self','','');
}
// 选择低价商品 屏蔽价格范围选择
function CheckCheapProducts()
{
    if(document.getElementById("TradingSieve_ckb_Cheap").checked) 
    {
        document.getElementById("TradingSieve_txt_Price1").value = "0";
        document.getElementById("TradingSieve_txt_Price2").value = "50";
        document.getElementById("TradingSieve_txt_Price1").disabled = true;
        document.getElementById("TradingSieve_txt_Price2").disabled = true;
    }
    else
    {
        document.getElementById("TradingSieve_txt_Price1").value = "";
        document.getElementById("TradingSieve_txt_Price2").value = "";
        document.getElementById("TradingSieve_txt_Price1").disabled = false;
        document.getElementById("TradingSieve_txt_Price2").disabled = false;
    }
}

    //从右往左去空格   
    function  Rtrim(stringObj)   
    {   
        while   (stringObj.charCodeAt(stringObj.length   -   1)   ==   32)   
         {   
            stringObj   =   stringObj.substring(0,stringObj.length   -   1);   
         }   
        return   stringObj;   
    }   
    //从左往右去空格   
    function  Ltrim(stringObj)   
    {   
        while   (stringObj.charCodeAt(0)   ==   32)   
          {   
            stringObj   =   stringObj.substring(1,stringObj.length);   
          }   
        return   stringObj;   
    }   
   //去字符串左右两边的空格 
   function  Trim(stringObj)   
   {   
        return(Ltrim(Rtrim(stringObj)));   
   }

 function BackgroundColor(obj) {
    if(obj.className!="") {   
      obj.style.backgroundColor='#FBFBFB';
    } else {
      obj.style.backgroundColor='#ffffff';
    } 
} 
    function redirect(obj) {
	var url = location.href;
	var start = url.toLowerCase().indexOf('ts');
	var end = url.length;
	var tsStr=''
	var target = 'ts='+obj.value;	
	var prstart=url.toLowerCase().indexOf('pr');
	var gridStart = url.toLowerCase().indexOf('grid');
	if (gridStart != -1) {
	    var gridEnd = url.length;
	    for (var iGrid = gridStart; iGrid < url.length; iGrid++) {
	        if (url.substring(iGrid, iGrid+1) == '&') {
	            gridEnd = iGrid;
	            break;
	        }
	    }
	    var gridStr = url.substring(gridStart, gridEnd);
	    url = url.replace(gridStr, '');
	}
    if(prstart!=-1)	{
        var prend=url.length
        for(var ipr=prstart; ipr<url.length;ipr++) {
            if(url.substring(ipr,ipr+1) == '&') {
                prend=ipr;
                break;
            } 
        }
        var prStr=url.substring(prstart,prend);
        url = url.replace(prStr,'');
    }
    if(start != -1 ) {
        for(var i = start;i<url.length;i++) {	
            if(url.substring(i,i+1) == '&') {
                end = i;
                break;
            }
        }
        tsStr = url.substring(start,end);
        if(obj.value == 'NotDefined')
        {
            url = url.replace(tsStr,'');		
        }
         else 
         {
            if(obj.value=='CardService') 
            {
 
              location.href='http://dkjy.5173.com/bizoffer/cccard/viewlist.aspx?gameid=880';
            } 
            else 
            {
                url = url.replace(tsStr,target);
            }
        }
    }
    else if(url.indexOf('?') == -1) {
        url = url + '?' + target;	
    } 
    else 
    {
        url = url + '&' + target;	
        if(obj.value == 'CardService') 
        {
            location.href='http://dkjy.5173.com/bizoffer/cccard/viewlist.aspx?gameid=880';
            return;
        }		
    }

	while(url.substring(url.length-1,url.length) == '?' || url.substring(url.length-1,url.length) == '&') {
        url = url.substring(0,url.length-1);
	}
　　if(obj.value=='CardService') {
        location.href='http://dkjy.5173.com/bizoffer/cccard/viewlist.aspx?gameid=880';
        return;
	} else {
        location.href = urlChange(url);
	}
	/*跳转我要求购
	   Add by chenzheng 2008/12/26
	*/
	if(obj.value=='OfferList')
	{
	    if(location.href.indexOf("?")>0)
	    {
	        var p=location.href.split("?")[1];
	        if(location.href.toLowerCase().indexOf("search.aspx"))
	            location.href="http://need.5173.com/Sell/BuyList.aspx?gm=880&"+p;
	        else
	            location.href="http://need.5173.com/Sell/BuyList.aspx?gm=880&"+p;
	        return;
	    }
	    else
	    {
	        location.href="http://need.5173.com/Sell/BuyList.aspx?gm=880";
	        return;
	    }
	}
	else if(obj.value == 'GameAccount')
	{   
	    var urlBt = "";
	    urlBt=url.toLowerCase().indexOf('bt');
	    if(urlBt != -1)
	    {
	       urlBt=url.substring(urlBt,urlBt+35);
           url = url.replace(urlBt,"");  
	    }
	    url = url + "&bt=e5296215c29c4c2e97079b4d33357f1d";
	    location.href = url;
	    return;
	}
	else
	{
	    location.href=urlChange(url);
	}
}
function urlChange(url) {
    var urlBt = url.toLowerCase().indexOf('bt');
    urlBt=url.substring(urlBt,urlBt+35);

    var urlVar = url.indexOf('ts');
    var urlEscort=url.substring(urlVar,urlVar+9);

    var urlConsignment=url.substring(urlVar,urlVar+14);

    var urlApi=url.substring(urlVar,urlVar+6);

    var urlCardService=url.substring(urlVar,urlVar+14);

    if(urlEscort=="ts=Escort" && urlBt=="bt=e5296215c29c4c2e97079b4d33357f1d") {
        url=url.replace("bt=e5296215c29c4c2e97079b4d33357f1d","");
    } else if(urlConsignment=="ts=Consignment" && urlBt=="bt=e5296215c29c4c2e97079b4d33357f1d") {
        url=url.replace("bt=e5296215c29c4c2e97079b4d33357f1d","");
    } else if(urlApi=="ts=API" && urlBt=="bt=e5296215c29c4c2e97079b4d33357f1d") {
        url=url.replace("bt=e5296215c29c4c2e97079b4d33357f1d","");
    } else if(urlCardService=="ts=CardService" && urlBt=="bt=e5296215c29c4c2e97079b4d33357f1d") {
        url=url.replace("bt=e5296215c29c4c2e97079b4d33357f1d","");
    }
    return url;
}
function changeUrl() {
    var url = location.href;
    if(url.indexOf('&sort')!=-1) {
        var urlBA = url.indexOf('sort=MoneyAveragePriceAsc');
        if(urlBA!=-1) {
            url=url.replace("sort=MoneyAveragePriceAsc","sort=MoneyAveragePriceDesc");
        } else {
            var urBD=url.indexOf('sort=MoneyAveragePriceDesc');
            if(urBD!=-1) {
                url=url.replace("sort=MoneyAveragePriceDesc","sort=MoneyAveragePriceAsc");
            }
        }
    } else {
        url=url+"&sort=MoneyAveragePriceDesc";
    }
    location.href = url;
}
function redirectNew(obj,fullname) {
    var url = location.href;
    var start;
    var target = 'ts='+obj.value;	
    if(fullname=="bizOffersList_tradingTypeList") {
        start = url.toLowerCase().indexOf('ts');
        target = 'ts='+obj.value;
    } else {
        if(fullname=="bizOffersList_ddltradingSortList") {
            start = url.toLowerCase().indexOf('sort');
            target = 'sort='+obj.value;
        } else {
            start = url.toLowerCase().indexOf('pr');
            target = 'pr='+obj.value;
        }
    }
    var end = url.length;
    var tsStr=''

    if(start != -1 ) {
        for(var i = start;i<url.length;i++) {	
            if(url.substring(i,i+1) == '&') {
                end = i;
                break;
            }
        }
        tsStr = url.substring(start,end);
        if(obj.value == 'NotDefined'||obj.value=='-1') {
            url = url.replace(tsStr,'');
        } else {
            url = url.replace(tsStr,target);	
        }
    } else if(url.indexOf('?') == -1) {
        url = url + '?' + target;	
    } else {
        url = url + '&' + target;			
    }

    while(url.substring(url.length-1,url.length) == '?' || url.substring(url.length-1,url.length) == '&') {
        url = url.substring(0,url.length-1);
    }
    location.href = url;
}
</ResourceMerge:RawResource>

</asp:Content>
