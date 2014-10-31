<script>
window.attachEvent("onload", init);
function init(){
//
var editplandat = document.getElementById('_Datafoll_buywill').innerHTML.toString();
var pricetype  = document.getElementById('_Captfoll_result');

pricetype.parentNode.style.display = "none";
if (editplandat.indexOf('”–“‚œÚ') != -1)
{
	pricetype.parentNode.style.display = "none";
}
var typevalue = document.getElementById('foll_buywill').value;
if(typevalue == "2") {	
	pricetype.parentNode.style.display = "";
}else {
	pricetype.parentNode.style.display = "none";
}
hidescreen();
hidefrom();
}


function hidescreen(){

var pricetype  = document.getElementById('_Captoppo_code');

var typevalue = document.getElementById('foll_buywill').value;
//alert(typevalue);
if(typevalue == "1") {	
	pricetype.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.style.display = "block";
	
}else if (typevalue == "2") {
pricetype.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.style.display = "none";
	
}else {
	pricetype.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.style.display = "none";
}

}
function hidefrom(){

var pricetype  = document.getElementById('_Captoppo_fromzh');
var comp  = document.getElementById('_Captoppo_rescompanyid');
pricetype.parentNode.style.display = "none";
comp.parentNode.style.display = "none";
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

// var pricetype2  = document.getElementById('_Captoppo_qutaprice');
var comp2  = document.getElementById('_Captoppo_forecast');
// pricetype2.parentNode.style.display = "none";
comp2.parentNode.style.display = "none";
var typevalue2 = document.getElementById('oppo_countryin').value;
if(typevalue2 == "in") {	
	// pricetype2.parentNode.style.display = "";
	comp2.parentNode.style.display = "";
	
}else if (typevalue2 == "out") {
// pricetype2.parentNode.style.display = "none";
comp2.parentNode.style.display = "none";
}else {
	// pricetype2.parentNode.style.display = "none";
	comp2.parentNode.style.display = "none";
}

}
</script>