<?php
/////////////////////////////////////////
//    Powered By : WindForce Studios   // 
/////////////////////////////////////////


include("config.php");
include('secureFunction.php');
include("Key.php");

if($SecureKey == @$_GET['secure']){
if(@$_GET["changePassword"]){
	if(@$_GET["id"] && @$_GET["newpassword"]){
		
		$update_password = mysql_query("UPDATE users SET password='".md5(make_safe($_GET["newpassword"]))."' WHERE id='".make_safe($_GET["id"])."'");
		if($update_password){
			echo "1";
		}
	}else{
		echo "Please Fill a password Field";
	}
}else if(@$_GET["changeInformation"]){
		if(@$_GET["id"] && @$_GET["firstname"] && @$_GET["lastname"] && @$_GET["age"] && @$_GET["email"] && @$_GET["country"]){
			if (!filter_var($_GET['email'], FILTER_VALIDATE_EMAIL)) {
    die( "This email not vaild!");
}
		$update_information = mysql_query("UPDATE users SET firstname='".make_safe($_GET["firstname"])."' , lastname='".make_safe($_GET["lastname"])."' , age='".make_safe($_GET["age"])."' , email='".make_safe($_GET["email"])."' , country='".make_safe($_GET["country"])."' WHERE id='".make_safe($_GET["id"])."'");
if($update_information){
			echo "1";
		}
	
}else{
	
			echo "Please Fill all Fields";

}
}
}else{
	echo "Invalid SecureKey";
}
?>