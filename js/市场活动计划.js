<script>
window.attachEvent("onload", init);
function init(){

var type = document.getElementById('_Datamapl_type').innerHTML.toString();
//
var s1  = document.getElementById('_Captmapl_zhplace');
var s2 = document.getElementById('_Captmapl_zhfee');
var s3 = document.getElementById('_Captmapl_zharea');
var s4 = document.getElementById('_Captmapl_decorate');
var s5 = document.getElementById('_Captmapl_decoratecompid');
var s6 = document.getElementById('_Captmapl_zhbgdate');
var s7 = document.getElementById('_Captmapl_zhdxjfdate');
//
var e1 = document.getElementById('_Captmapl_decoratecompid');
var e2 = document.getElementById('_Captmapl_zhfee');
var e3 = document.getElementById('_Captmapl_zhbgdate'); 
var e4 = document.getElementById('_Captmapl_zhdxjfdate');
//paper
var h1 = document.getElementById('_Captmapl_jourtype');
var h2 = document.getElementById('_Captmapl_journo');
//web
var h3 = document.getElementById('_Captmapl_coproject');
//promote
var h4  = document.getElementById('_Captmapl_promotepolicy');
var h5 = document.getElementById('_Captmapl_promotetargetlimit');
var h6 = document.getElementById('_Captmapl_reallimit');
var h7 = document.getElementById('_Captmapl_promotionplace');
var h8 = document.getElementById('_Captmapl_companyid');


if(type.indexOf("网络推广") != -1 || type.indexOf("户外") != -1)
{
	s1.parentNode.style.display = "none";
	s2.parentNode.style.display = "none";
	s3.parentNode.style.display = "none";
	s4.parentNode.style.display = "none";
	s5.parentNode.style.display = "none";
	s6.parentNode.style.display = "none";
	s7.parentNode.style.display = "none";
	h1.parentNode.style.display = "none";
	h2.parentNode.style.display = "none";
	h3.parentNode.style.display = "";
	h4.parentNode.style.display = "none";
	h5.parentNode.style.display = "none";
	h6.parentNode.style.display = "none";
	h7.parentNode.style.display = "none";
	h8.parentNode.style.display = "none";
	
}else if(type.indexOf("标") != -1 || type.indexOf("特装") != -1 ){
	s1.parentNode.style.display = "";
	s2.parentNode.style.display = "";
	s3.parentNode.style.display = "";
	s4.parentNode.style.display = "";
	s5.parentNode.style.display = "";
	s6.parentNode.style.display = "";
	s7.parentNode.style.display = "";
	h1.parentNode.style.display = "none";
	h2.parentNode.style.display = "none";
	h3.parentNode.style.display = "none";
	h4.parentNode.style.display = "none";
	h5.parentNode.style.display = "none";
	h6.parentNode.style.display = "none";
	h7.parentNode.style.display = "none";
	h8.parentNode.style.display = "none";
	
	if(type.indexOf("标展") != -1 )
	{
		e1.parentNode.style.display = "none";
	e2.parentNode.style.display = "none";
	e3.parentNode.style.display = "none";
	e4.parentNode.style.display = "none"
	}else if(type.indexOf("标改") != -1 )
	{
	e1.parentNode.style.display = "none";
	e2.parentNode.style.display = "none";
	e3.parentNode.style.display = "none";
	}
	
}else if(type.indexOf("促销会") != -1)
{
	s1.parentNode.style.display = "none";
	s2.parentNode.style.display = "none";
	s3.parentNode.style.display = "none";
	s4.parentNode.style.display = "none";
	s5.parentNode.style.display = "none";
	s6.parentNode.style.display = "none";
	s7.parentNode.style.display = "none";
	h3.parentNode.style.display = "none";
	h1.parentNode.style.display = "none";
	h2.parentNode.style.display = "none";
	h4.parentNode.style.display = "";
	h5.parentNode.style.display = "";
	h6.parentNode.style.display = "";
	h7.parentNode.style.display = "";
	h8.parentNode.style.display = "";
}
else if(type.indexOf("平面") != -1)
{
	s1.parentNode.style.display = "none";
	s2.parentNode.style.display = "none";
	s3.parentNode.style.display = "none";
	s4.parentNode.style.display = "none";
	s5.parentNode.style.display = "none";
	s6.parentNode.style.display = "none";
	s7.parentNode.style.display = "none";
	h3.parentNode.style.display = "none";
	h1.parentNode.style.display = "";
	h2.parentNode.style.display = "";
	h4.parentNode.style.display = "none";
	h5.parentNode.style.display = "none";
	h6.parentNode.style.display = "none";
	h7.parentNode.style.display = "none";
	h8.parentNode.style.display = "none";
}
else 
{
	s1.parentNode.style.display = "none";
	s2.parentNode.style.display = "none";
	s3.parentNode.style.display = "none";
	s4.parentNode.style.display = "none";
	s5.parentNode.style.display = "none";
	s6.parentNode.style.display = "none";
	s7.parentNode.style.display = "none";
	h1.parentNode.style.display = "none";
	h2.parentNode.style.display = "none";
	h3.parentNode.style.display = "none";
	h4.parentNode.style.display = "none";
	h5.parentNode.style.display = "none";
	h6.parentNode.style.display = "none";
	h7.parentNode.style.display = "none";
	h8.parentNode.style.display = "none";
}



var typevalue = document.getElementById('mapl_type').value;
//alert(typevalue);
if(typevalue == "1" || typevalue == "a" || typevalue == "b" ) {
	
	s1.parentNode.style.display = "";
	s2.parentNode.style.display = "";
	s3.parentNode.style.display = "";
	s4.parentNode.style.display = "";
	s5.parentNode.style.display = "";
	s6.parentNode.style.display = "";
	s7.parentNode.style.display = "";
	h1.parentNode.style.display = "none";
	h2.parentNode.style.display = "none";
	h3.parentNode.style.display = "none";
	h4.parentNode.style.display = "none";
	h5.parentNode.style.display = "none";
	h6.parentNode.style.display = "none";
	h7.parentNode.style.display = "none";
	h8.parentNode.style.display = "none";
	if(typevalue == "a" )
	{
	e1.parentNode.style.display = "none";
	e2.parentNode.style.display = "none";
	e3.parentNode.style.display = "none";
	e4.parentNode.style.display = "none"
	}else if(typevalue == "b" )
	{
	e1.parentNode.style.display = "none";
	e2.parentNode.style.display = "none";
	e3.parentNode.style.display = "none";
	}
	
	
}else if(typevalue == "3" || typevalue == "4")
{
// alert(ss4.innerText);
	s1.parentNode.style.display = "none";
	s2.parentNode.style.display = "none";
	s3.parentNode.style.display = "none";
	s4.parentNode.style.display = "none";
	s5.parentNode.style.display = "none";
	s6.parentNode.style.display = "none";
	s7.parentNode.style.display = "none";
	h1.parentNode.style.display = "none";
	h2.parentNode.style.display = "none";
	h3.parentNode.style.display = "";
	h4.parentNode.style.display = "none";
	h5.parentNode.style.display = "none";
	h6.parentNode.style.display = "none";
	h7.parentNode.style.display = "none";
	h8.parentNode.style.display = "none";
}
else if (typevalue == "5")
{
	s1.parentNode.style.display = "none";
	s2.parentNode.style.display = "none";
	s3.parentNode.style.display = "none";
	s4.parentNode.style.display = "none";
	s5.parentNode.style.display = "none";
	s6.parentNode.style.display = "none";
	s7.parentNode.style.display = "none";
	h3.parentNode.style.display = "none";
	h1.parentNode.style.display = "";
	h2.parentNode.style.display = "";
	h4.parentNode.style.display = "none";
	h5.parentNode.style.display = "none";
	h6.parentNode.style.display = "none";
	h7.parentNode.style.display = "none";
	h8.parentNode.style.display = "none";
}else if (typevalue == "2")
{
	s1.parentNode.style.display = "none";
	s2.parentNode.style.display = "none";
	s3.parentNode.style.display = "none";
	s4.parentNode.style.display = "none";
	s5.parentNode.style.display = "none";
	s6.parentNode.style.display = "none";
	s7.parentNode.style.display = "none";
	h3.parentNode.style.display = "none";
	h1.parentNode.style.display = "none";
	h2.parentNode.style.display = "none";
	h4.parentNode.style.display = "";
	h5.parentNode.style.display = "";
	h6.parentNode.style.display = "";
	h7.parentNode.style.display = "";
	h8.parentNode.style.display = "";
}
else{
	s1.parentNode.style.display = "none";
	s2.parentNode.style.display = "none";
	s3.parentNode.style.display = "none";
	s4.parentNode.style.display = "none";
	s5.parentNode.style.display = "none";
	s6.parentNode.style.display = "none";
	s7.parentNode.style.display = "none";
	h1.parentNode.style.display = "none";
	h2.parentNode.style.display = "none";
	h3.parentNode.style.display = "none";
	h4.parentNode.style.display = "none";
	h5.parentNode.style.display = "none";
	h6.parentNode.style.display = "none";
	h7.parentNode.style.display = "none";
	h8.parentNode.style.display = "none";
}



}
</script>