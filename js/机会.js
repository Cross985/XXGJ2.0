<script> 
window.attachEvent("onload", init);
function init(){
var Captoppo_totalquote =  document.getElementById("_Captoppo_totalquotes");
    if(Captoppo_totalquote!=null){                    
Captoppo_totalquote.parentNode.parentNode.parentNode.parentNode.parentNode.style.display='none';
    }  
    var arrElements = document.getElementsByTagName('td');
    var strClassName='PANEREPEAT';
    var oElement;
    for(var i=0; i < arrElements.length; i++){
       oElement = arrElements[i];
       if(oElement.innerHTML=='机会总计'){
           oElement.parentNode.style.display='none';
       }
    }
	hidefrom();
	
}
function hidefrom(){

var editplandat = document.getElementById('_Dataoppo_source').innerHTML.toString();

var pricetype  = document.getElementById('_Captoppo_fromzh');
var comp  = document.getElementById('_Captoppo_rescompanyid');
pricetype.parentNode.style.display = "none";
comp.parentNode.style.display = "none";
//alert(editplandat);
if (editplandat.indexOf('展会') != -1)
{
	pricetype.parentNode.style.display = "";
}else if(editplandat.indexOf('客户介绍') != -1) 
{
	comp.parentNode.style.display = "";
}
var typevalue = document.getElementById('oppo_source').value;
if(typevalue == "Tradeshow") {	
	pricetype.parentNode.style.display = "";
	comp.parentNode.style.display = "none";
}else if (typevalue == "Referral") {
pricetype.parentNode.style.display = "none";
	comp.parentNode.style.display = "";
}else {
	pricetype.parentNode.style.display = "none";
	comp.parentNode.style.display = "none";
}

}
</script>