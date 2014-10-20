</script><script>
window.attachEvent("onload", init);
function init(){
	var all = [];
	all = document.getElementsByTagName('a');
	var i ;
	for( i=0 ; i < all.length; i++)
	{
		var link = all[i].href;
		//alert(link.toString());
		if(link.toString().indexOf("&Act=130") != -1)
		{
			all[i].parentNode.parentNode.style.display = "none";
			//alert();
		}
	}
	hidefrom();
}
function hidefrom(){

var editplandat = document.getElementById('_Datacomp_resource').innerHTML.toString();

var pricetype  = document.getElementById('_Captcomp_fromzh');
var comp  = document.getElementById('_Captcomp_reccompanyid');
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
var typevalue = document.getElementById('comp_resource').value;
if(typevalue == "1") {	
	pricetype.parentNode.style.display = "";
	comp.parentNode.style.display = "none";
}else if (typevalue == "4") {
pricetype.parentNode.style.display = "none";
	comp.parentNode.style.display = "";
}else {
	pricetype.parentNode.style.display = "none";
	comp.parentNode.style.display = "none";
}

}
</script>