<script>
window.attachEvent("onload", init);
function init(){

var editplandat = document.getElementById('_Datafoll_buywill').innerHTML.toString();
var pricetype  = document.getElementById('_Captfoll_result');

pricetype.parentNode.style.display = "none";
if (editplandat.indexOf('Œﬁ“‚œÚ') != -1)
{
	pricetype.parentNode.style.display = "";
}
var typevalue = document.getElementById('foll_buywill').value;
if(typevalue == "2") {	
	pricetype.parentNode.style.display = "";
}else {
	pricetype.parentNode.style.display = "none";
}

}
</script>