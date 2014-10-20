
<script>
window.attachEvent("onload", init);
function init(){
//修改取消按钮跳转链接
	var all = new Array();
	all = document.getElementsByTagName('a');
	var i ;
	for( i=0 ; i < all.length; i++)
	{
		var link = all[i].href;
		//alert(link.toString());
		if(link.toString().indexOf("&Act=184") != -1)
		{
			var link = all[i].getAttribute('href');
			var str = new Array();
			str = link.split("Act=184");
			link = str[0] + "Act=432&Mode=1&CLk=T&dotnetdll=SalesMenu&dotnetfunc=RunOpportunity&J=Opportunity&T=SalesManagement";
			all[i].setAttribute('href',link); 
			//alert(str[0]);
		}
	}
	
}
</script>