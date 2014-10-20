<script>
window.attachEvent("onload", init);
function init(){

var editplandat = document.getElementById('_Dataquta_type').innerHTML.toString();
var pricetype  = document.getElementById('_Captquta_pricetpye');
var prodinfo = document.getElementById('_Captquta_productinfo');
var wrapinfo = document.getElementById('_Captquta_wrapinfo');
//alert(editplandat);
// alert("="+editplandat.innerText+"=")

if (editplandat.indexOf('国内') != -1)
{
	
	pricetype.parentNode.style.display = "none";
	prodinfo.parentNode.style.display = "none";
	wrapinfo.parentNode.style.display = "none";
	
}else if (editplandat.indexOf('国外') != -1)
{
	
	pricetype.parentNode.style.display = "";
	prodinfo.parentNode.style.display = "";
	wrapinfo.parentNode.style.display = "";
}
var typevalue = document.getElementById('quta_type').value;
if(typevalue == "2102") {
	
	pricetype.parentNode.style.display = "";
	prodinfo.parentNode.style.display = "";
	wrapinfo.parentNode.style.display = "";
}else {
	
	pricetype.parentNode.style.display = "none";
	prodinfo.parentNode.style.display = "none";
	wrapinfo.parentNode.style.display = "none";
}

}
</script>