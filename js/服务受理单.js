	<script>
	window.attachEvent("onload", init);
	function init(){
		var type = document.getElementById('_Dataseac_type').innerHTML;
		
		var s1 = document.getElementById('_Captseac_product');
		var s2 = document.getElementById('_Captseac_spec');
		var s3 = document.getElementById('_Captseac_qty');
		var s4 = document.getElementById('_Captseac_reason');
		
		if(type.toString().indexOf("·þÎñ") != -1)
		{
			s1.parentNode.style.display = "none";
			s2.parentNode.style.display = "none";
			s3.parentNode.style.display = "none";
			s4.parentNode.style.display = "";
		}
		
		var typevalue = document.getElementById('seac_type').value; 
		if(typevalue == "product")
		{
			s1.parentNode.style.display = "";	
			s2.parentNode.style.display = "";
			s3.parentNode.style.display = "";
			s4.parentNode.style.display = "";
			//alert();
		}
		else {
			s1.parentNode.style.display = "none";
			s2.parentNode.style.display = "none";
			s3.parentNode.style.display = "none";
			s4.parentNode.style.display = "";
		}
	}
	function updateStatus(){
		var seac_anareason = document.getElementById('seac_anareason').value;
		var seac_status =document.getElementById('seac_status').value;
		if((seac_anareason != null || seac_anareason != "" || seac_anareason != "undefined") && (seac_status == "0" || seac_status == ""))
		{
			document.getElementById('seac_status').value = "1";
		}
	}
	</script>