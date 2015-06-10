

//验证控件是否符合标准
function ActivationRegexsValue(varControl,spanContrl)
{
	
	var returnvar=true;	
	var strError='';	
	if(document.getElementById(varControl))
	{
		
		var Controlvalue=document.getElementById(varControl).value.replace(/(^\s*)|(\s*$)/g,'');		
		
	}
	switch(spanContrl)
	{
		
		
		//手机或小灵通
		case 'span_tbMobile':
		if(document.getElementById(varControl))
		{
			
			
			if(Controlvalue.length<=0)
			{
				
				returnvar=false;				
				
			}
			else 
			{
				
				//var reg=new RegExp("^[0-9\-]{10,12}$");	
				var reg=new RegExp("^[0-9]{1,20}$");				
				returnvar=reg.test(Controlvalue);				
				
			}
			
			
		}
		break;		
		
		//QQ
		case 'span_tbQQ':
		if(document.getElementById(varControl))
		{
			
			if(Controlvalue.length<=0)
			{
				
				returnvar=false;				
				
			}
			else 
			{
				
				var reg=new RegExp("^\\d+$");
				
				returnvar=reg.test(Controlvalue);				
				
			}
			
			
		}
		break;
		
		
		//固定电话号码
		case 'span_tbTelephone':
		if(document.getElementById(varControl))
		{
			
			if(Controlvalue.length<0)
			{
				
				returnvar=false;				
				
			}
			else if(Controlvalue.length>0)
			{
				
				//var reg = new RegExp("^[0-9\-]{10,12}$");
				var reg=/^0\d{2,3}\-\d{7,8}$/;
				
				returnvar=reg.test(Controlvalue);				
				
			}
			
			
		}
		break;
		
		
		
	}
	if($(varControl).readOnly==true)
	{
		
		returnvar=true;		
		
	}
	if(document.getElementById(varControl))
	{
		
		if(returnvar!=true)
		{
			
			ActivationWarmContrlInfo(document.getElementById(varControl),spanContrl);			
			ShowContrlStyle(spanContrl,'3');			
			
		}
		else 
		{
			
			ActivationRightsContrlInfo(document.getElementById(varControl),spanContrl);			
			ShowContrlStyle(spanContrl,'2');			
			
		}
		
	}
	
	return returnvar;	
	
}

//初始信息提示
function ActivationStrarContrlInfo(spanContrl)
{
	
	var oTarget=document.getElementById(spanContrl);	
    /*switch(spanContrl)
	{
		
		//手机或小灵通
		case 'span_tbMobile':
		InnerText(oTarget,'小灵通格式如:02188888888，手机格式如：13800138000！');		
		break;		
		
		//QQ
		case 'span_tbQQ':
		InnerText(oTarget,'请输入您用于交易的QQ号码！');		
		break;		
		
		//固定电话号码
		case 'span_tbTelephone':
		InnerText(oTarget,'固定电话格式如：021-62915173！');		
		break;		
		
		
		
	}*/
	
}


//错误信息提示
function ActivationWarmContrlInfo(varControl,spanContrl)
{
	
	var oTarget=document.getElementById(spanContrl);	
	switch(spanContrl)
	{
		
		//手机或小灵通
		case 'span_tbMobile':
		varControl.className="showred";
		InnerText(oTarget,'请输入正确的手机或小灵通号码！');	
		break;
		
		
		//QQ
		case 'span_tbQQ':
		varControl.className="showred";
		InnerText(oTarget,'请正确填写QQ号码！');	
		break;
		
		
		//固定电话号码
		case 'span_tbTelephone':
		varControl.className="showred";	
		InnerText(oTarget,'请正确填写固定电话号码！');
		break;
		
		
		
		
	}
	
}

//正确信息提示
function ActivationRightsContrlInfo(varControl,spanContrl)
{
	
	var oTarget=document.getElementById(spanContrl);	
	switch(spanContrl)
	{
		
		//手机或小灵通
		case 'span_tbMobile':
		varControl.className='';
		InnerText(oTarget,'              ');	
		break;		
		
		//QQ
		case 'span_tbQQ':
		varControl.className='';	
		InnerText(oTarget,'              ');	
		break;		
		
		//固定电话号码
		case 'span_tbTelephone':
		varControl.className='';		
		InnerText(oTarget,'              ');
		break;		
		
		
		
	}
	
}

//生成集合验证方法

function ActivationCheck()
{
	
	 
	var bl=true;
	
	var blArray=new Array(3);
	
	var msgArray=new Array(3);
	
	var mjArray=new Array(3);
	
	var errorMsg='温馨提示：由于以下可能原因，您需要调整输入信息。\r\n\r\n';
	
	function ErrorMsg(spanContrl)
	{
		
		
		if(document.getElementById(spanContrl))
		{
			
			
			return ReturnInnerText(document.getElementById(spanContrl));
			
			
			
		}
		else 
		{
			
			
			return ''
			
			
		}
		
		
	}
	blArray[0]=ActivationRegexsValue('tbMobile','span_tbMobile');
	
	msgArray[0]=ErrorMsg('span_tbMobile');
	
	mjArray[0]='#span_tbMobile';
	
	
	blArray[1]=ActivationRegexsValue('tbQQ','span_tbQQ');	
	msgArray[1]=ErrorMsg('span_tbQQ');
	
	mjArray[1]='#span_tbQQ';	
	
	blArray[2]=ActivationRegexsValue('tbTelephone','span_tbTelephone');	
	msgArray[2]=ErrorMsg('span_tbTelephone');
	
	mjArray[2]='#span_tbTelephone';
	
	
	
	
	var errornum=0;
	
	var maoji='';
	
	for(var i=0;i<blArray.length;i++)
	{
		
		
		if(blArray[i]==false)
		{
			
			
			if(maoji=='')
			{
				
				
				maoji=mjArray[i];
				
				
				
			}
			errornum=errornum+1;
			
			errorMsg=errorMsg+errornum+'：'+msgArray[i]+'\r\n';
			
			bl=false;
			
			
			
		}
		
		
	}
	if(bl==false)
	{
		
		
		window.location.hash=maoji;
		
		alert(errorMsg);
		
		
		
	}
	else 
	{
		
		var paras='methodName=SAVEUSERINFO&Mobile='+$('tbMobile').value+'&QQ='+$('tbQQ').value+'&Tel='+$('tbTelephone').value+'&TrueMobile='+$('hiddenMobile').value;		
		var _ajax=new Ajax.Request(ajaxPage,
		{
			
			method:'get',parameters:paras,onComplete:function (request){
				
				var flag=request.responseText;				
				if(flag=='true' || flag == 'unable')
				{
 
					ShowActivationDiv();					
					
				}
				else 
				{
					 
					alert(flag);					
					
				}
				
			}
			
		});		
		
		
		
		
	}
	
}

//激活与发布层切换
function SelectShow(bl)
{
	if(bl==true)
	{
		$('divSecond').style.display='';		
		$('AccountActivationInfo').style.display='none';		
		
	}
	else 
	{
		$('divfirst').style.display='none';	
		$('divSecond').style.display='none';		
		$('AccountActivationInfo').style.display='';
		scroll(0,0);		 	
	}
}



function GetUEDCallBack()
{
    var paras='methodName=GETUEDCALLBACK';
    var _ajax=new Ajax.Request(ajaxPage,
   {
        method:'get',parameters:paras,onComplete:function (request){
        
            var Uedcallback = request.responseText;	
            
            switch(Uedcallback)
            {
                case "BadArea":UED.layer31.show({id:'MobileBadArea',title:'5173友情提示',width:500,height:217,close:2,layerContainer:'BadAreaUED_lightbox'});break;
                case "DifferIP":UED.layer31.show({id:'MobileDifferIP',title:'5173友情提示',width:500,height:170,close:2,layerContainer:'DifferIPDivUED_lightbo'});break;
                case "Error":showTeleponeQQ();break;
                case "OK": showTeleponeQQ();break;
                default:showTeleponeQQ();break;
            }
        }
        
   });  
     
}



//取用户的个人信息
function GetUserInfo()
{
	
	
	var paras='methodName=GETUSERINFO';	
	var _ajax=new Ajax.Request(ajaxPage,
	{
		
		method:'get',parameters:paras,onComplete:function (request){
			
			var userinfo=request.responseText;	
			var myArray=userinfo.split("&");	
			var FinalMobile	= 	myArray[0];
			$('tbMobile').value=FinalMobile;	
			$('hiddenMobile').value = FinalMobile;		
			$('tbQQ').value=myArray[1];			
			$('tbTelephone').value=myArray[2];			
			
			if($('tbMobile').value!=""||$('tbTelephone').value!="")
			{
				
				
				if($('tbMobile').value!="")
				{
					
					$('tbMobile').value=showinfo(3,4,$('tbMobile').value);					
					$('tbMobile').readOnly=true;					
					ShowContrlStyle('span_tbMobile','2');
					InnerText($('span_tbMobile'),'');				
					
				}
				
				if($('tbQQ').value!="")
				{
					
					$('tbQQ').value=showinfo(3,4,$('tbQQ').value);					
					$('tbQQ').readOnly=true;					
					ShowContrlStyle('span_tbQQ','2');
					InnerText($('span_tbQQ'),'');				
					
				}
				
				if($('tbTelephone').value!="")
				{
					
					var total=$('tbTelephone').value.length;					
					var index=$('tbTelephone').value.indexOf("-");					
					var areaCode=$('tbTelephone').value.substring(0,index);					
					var phoneNumber=$('tbTelephone').value.substr(index+1,total-index);					
					phoneNumber=showinfo(2,2,phoneNumber);					
					$('tbTelephone').value=areaCode+"-"+phoneNumber;					
					$('tbTelephone').readOnly=true;					
					ShowContrlStyle('span_tbTelephone','2');	
					InnerText($('span_tbTelephone'),'');				
					
				}
				
				$('ahChangeinfo').style.display="inline";				
				
				
			}
			else 
			{
				
				$('ahChangeinfo').style.display="none";				
				
			}
			
			
		}
		
	});	
	
	
}



//按*号方式显示信息
function showinfo(intForeText,intLastText,strText)
{
	
	var returnStr="";	
	var total=strText.length;	
	var strForeText=strText.substring(0,intForeText);	
	
	var strLastText=strText.substr(total-intLastText,intLastText);	
	
	var intCenterText=total-intForeText-intLastText;	
	var strCenterText="";	
	for(i=0;i<intCenterText;i++)
	{
		
		strCenterText+="*";		
		
	}
	if(total<=intForeText+intLastText)
	{
		
		returnStr=strText;		
		
	}
	else 
	{
		
		returnStr=strForeText+strCenterText+strLastText;		
		
	}
	return returnStr;	
	
}

//显示激活验证层
function ShowActivationDiv()
{
	
	selectCol('hidden');
	InnerText($('span_Mobile'),$('tbMobile').value);
	ShowActivationHtmlAlert('ActivationDiv');	
	scroll(0,0);	
	
}

//显示遮罩激活层					
function ShowActivationHtmlAlert(DivId)
{
    var iWidth=document.documentElement.clientWidth;	
	var iHeight=document.documentElement.clientHeight;	
	var h=350;
	var w=560;
	
	$(DivId).style.top=(iHeight-h)/2 + getScrollTop() +"px";
	$(DivId).style.left=(iWidth-w)/2+"px";
    $(DivId).style.display='block';	
    DisplayBg(true);
   
}
function ShowHtmlAlertByHW(DivId,h,w)
{
    var iWidth=document.documentElement.clientWidth;	
	var iHeight=document.documentElement.clientHeight;	
	
	$(DivId).style.top=(iHeight-h)/2 + getScrollTop() +"px";
	$(DivId).style.left=(iWidth-w)/2+"px";
    $(DivId).style.display='block';	
    DisplayBg(true);

}
var bgObj=document.createElement("div");
function DisplayBg(flag)
{
	var iWidth=document.documentElement.clientWidth;	
	var iHeight=document.documentElement.clientHeight;	
	
	bgObj.id='AlertHtml';	
	bgObj.style.cssText="position:absolute;left:0px;top:0px;width:"+iWidth+"px;height:"+Math.max(document.body.clientHeight,iHeight)+"px;filter:Alpha(Opacity=30);opacity:0.3;background-color:#000000;z-index:101;";	
	if(flag==true)
    {
	document.body.appendChild(bgObj);
	}
	else
	{
 
	document.body.removeChild(bgObj);	
	}

}

// 关闭遮罩激活层
function CloseActivationHtmlAlert(DivId)
{
   
	selectCol('visible');	
	$(DivId).style.display='none';
	if($('AlertHtml'))
	{
	    $('AlertHtml').style.display='none';
	}
	DisplayBg(false);
	
	
}
function HideAlertHtml(flag)
{
	
	if(flag)
	{
		
		$('AlertHtml').style.display='none';		
		$('divHtmlAlertContainer2').style.display='none';		
		
	}
	else 
	{
		
		$('AlertHtml').style.display='block';		
		$('divHtmlAlertContainer2').style.display='block';		
		
	}
	
	
}
//通过输入校验码激活
function ActivationByMobile()
{
	
	
	var paras='methodName=BINDMOBILEBYVALIDATECODE&validcode='+$('tbcode').value;	
	var _ajax=new Ajax.Request(ajaxPage,
	{
		
		method:'get',parameters:paras,onComplete:function onComplete(request)
		{
			
			
			if(request.responseText=='true')
			{
				
				
				NeedActivation();				
				
			}
			else 
			{
				
				alert('激活码错误，请确认后再输入！');		
				var txt = document.getElementById("tbcode");
				if(txt)
				{
				    txt.value = "";
				}
			}
			
		}
		
	});	
	
	
}


function clickddd()
{
     document.getElementById("IDPublish1_btnPub").click();
}


//取得当前用户是否需要显示激活相关模块
function NeedActivation()
{
	
	var paras='methodName=NEEDACTIVATION';	
	var _ajax=new Ajax.Request(ajaxPage,
	{
		
		method:'get',parameters:paras,onComplete:function onComplete(request)
		{
			
			//var activation=request.responseText;			
			//var myArray=activation.split("&");			
			//var needActivation=myArray[0];
	 
			var needActivation=request.responseText;	
			if(needActivation=='true')
			{
				
				alert('您的5173帐号未被激活，请确认后再点击！');				
				
			}
			else if(needActivation=='false')
			{
				
				alert('您的5173帐号激活成功！');				
				CloseActivationHtmlAlert('ActivationDiv');	
				clickddd();				
				
			}
			else
			{
			    alert(needActivation);
			}
			
		}
		
	});	
	
	
}

//发送校验码
function SendValidateCode()
{
	
	var paras='methodName=SENDVALIDATECODE';	
	var _ajax=new Ajax.Request(ajaxPage,
	{
		
		method:'get',parameters:paras,onComplete:function onComplete(request)
		{
			
			if(request.responseText!='true' && request.responseText!='unable')
			{
				alert(request.responseText); 
			}
			
		}
		
	});	
	
	
}

//重新发送校验码倒计时脚本
var wait=300;
//设置秒数(单位秒) 
var secs=0;

var flag="0";
function run()
{
	
	$('btnResent').disabled=true;

	if(flag=="0")
	{
	    for(var i=1;i<=wait;i++)
	    {
		    window.setTimeout("sTimer("+i+")",i*1000);
	    }
	}
}
function sTimer(num)
{
	
	if(num==wait)
	{
		
		$('spanResent').innerHTML="重新发送校验码";
		
		$('btnResent').disabled=false;
		flag = "0";
 
		SendValidateCode();		
		
	}
	else 
	{
		secs=wait-num;
		$('spanResent').innerHTML="重新发送校验码("+secs+")";
	    flag = "1";
	}
	
}


function selectCol(isHidden)
{
	
	var colSelects=document.getElementsByTagName('SELECT');	
	for(var i=0;i<colSelects.length;i++)
	{
		
		colSelects[i].style.visibility=isHidden;		
		
	}
	
	
}
//判断游戏是否必须选择阵营
function clickited(contrID) 
{
    var bl = true;
    var i=0;
    var objCount =$(hiddenGameRaceCountClientID);
    var count=objCount.value;
    var cou=0;
    for(i=0;i<count;i++)
    {
            var dom=document.getElementById(contrID + "_" + i);
            
            if(dom != null && bl)
            {
                if (dom.checked)
                {
                    break;
                } 
                else
                {
                    cou+=1;
                }        
            }
    }
    if(cou==count)
    {
        return "true"
    }

}

/*function isGameRaceMust()
{
    var objValue =$(hiddenGameRaceMustClientID);
    if(objValue!=null)
    {
        var objCount =$(hiddenGameRaceCountClientID);
        var count=objCount.value;
        var isMust =objValue.value;
        if($('IDPublishID1_IdGameRace1_rdGameRace') != null&&isMust=="2"&&count!="0"&&count!="")
        {
	        var value =clickited('IDPublishID1_IdGameRace1_rdGameRace');
	        if(value=="true")
            {
                $('span_rdGameRace').innerText="请选择游戏阵营！";
                ShowContrlStyle('span_rdGameRace','3');
                return false;
            }
            else{return true;}	
	    }
	    else{ return true;}
	}
	else{ return true;}
}*/



/*function ActivationSubDataCheckByIdCard()
{
 
    var flag=SubDataCheck();
	if(flag && $(hfHasIdClientID))
	{
	    if($(hfHasIdClientID).value=='false')
	    {
	        ShowIdCardDiv();
	        scroll(0,0);	
	        $(hfHasIdClientID).value="true";
	    }
	    else
	    {
	      ActivationSubDataCheck();
	    
	    }
	}

}*/

function ActivationSubDataCheckByIdCard()
{
    var flag=SubDataCheck();
	if(flag)
	{
	   ActivationSubDataCheck();
	}

}

//确认发布单信息并提交
function ActivationSubDataCheck()
{
	var flag=SubDataCheck();
	
	//检查阵营是否必选 Add by tuxia
	/*var isGameRaceCheck = isGameRaceMust();*/
	if(flag)
	{
	 
		var paras='methodName=NEEDACTIVATION';		
		var _ajax=new Ajax.Request(ajaxPage,
		{
			
			method:'get',parameters:paras,onComplete:function onComplete(request)
			{
				
				var activation=request.responseText;				
				var myArray=activation.split("&");				
				var needActivation=myArray[0];				
				var isActivation=myArray[1];				
				
				
				if(needActivation=='false')
				{
					
					if($(hfNeedAuditingCommisionClientID).value=="true")
					{
 
						IsPublish();						
						
					}
					else 
					{
 
				        clickddd();		
					}
					
				}
				else 
				{
 
					GetUserInfo();	
					SelectShow(false);					
					CloseActivationHtmlAlert('ActivationDiv');					
							
				}
				
				
			}
			
		});		
		
		
		
		
	}
	
	
}


/*屏蔽输入'<'>'*/
  function replaceErroWord(obj)
{
        if(obj.value.indexOf('<')>-1 || obj.value.indexOf('>')>-1)
       { 
        var str= obj.value.replace(/</g,'').replace(/>/g,'');
        obj.value =str;
       } 
}


/********************
 * 取窗口滚动条高度 
 ******************/
function getScrollTop()
{
    var scrollTop=0;
    if(document.documentElement&&document.documentElement.scrollTop)
    {
        scrollTop=document.documentElement.scrollTop;
    }
    else if(document.body)
    {
        scrollTop=document.body.scrollTop;
    }
    return scrollTop;
}


/********************
 * 取窗口可视范围的高度 
 *******************/
function getClientHeight()
{
    var clientHeight=0;
    if(document.body.clientHeight&&document.documentElement.clientHeight)
    {
        var clientHeight = (document.body.clientHeight<document.documentElement.clientHeight)?document.body.clientHeight:document.documentElement.clientHeight;        
    }
    else
    {
        var clientHeight = (document.body.clientHeight>document.documentElement.clientHeight)?document.body.clientHeight:document.documentElement.clientHeight;    
    }
    return clientHeight;
}

/********************
 * 取文档内容实际高度 
 *******************/
function getScrollHeight()
{
    return Math.max(document.body.scrollHeight,document.documentElement.scrollHeight);
}