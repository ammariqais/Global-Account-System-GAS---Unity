<?php

/////////////////////////////////////////
//    Powered By : WindForce Studios   // 
/////////////////////////////////////////


include("config.php");
include('secureFunction.php');
include("Key.php");

if($SecureKey == @$_GET['secure']){
if(@$_GET["code"] && @$_GET["password"]){
	$CheckCode = mysql_query("SELECT userid FROM forget_passwords WHERE code='".make_safe($_GET["code"])."'");
	$isExist = mysql_num_rows($CheckCode);
	if($isExist > 0){
		$getAccountInformation = mysql_fetch_array($CheckCode);
		$updatePassword = mysql_query("UPDATE users SET password='".md5(make_safe($_GET["password"]))."' WHERE id='".make_safe($getAccountInformation["userid"])."'");
		if($updatePassword){
					$deleteRecord = mysql_query("DELETE FROM forget_passwords WHERE userid='".$getAccountInformation["userid"]."'");

			if($deleteRecord){
		
		echo "1";
		}else{
			echo "Try Again Later ! Error in DB";
		}
		}else{
			echo "Try Again Later ! Error in DB";
		}
	}else{
		
		die("Invalid Code!");
	}
	
	
}else{
	die("Please Fill All Fields");
}
}else{
	echo "Invalid SecureKey";
}
?>