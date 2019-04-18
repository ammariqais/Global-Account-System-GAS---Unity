<?php
/////////////////////////////////////////
//    Powered By : WindForce Studios   // 
/////////////////////////////////////////

include("config.php");
include('secureFunction.php');
include("Key.php");

if(@$SecureKey == @$_GET['secure']){
if(@$_GET["email"] ){
if(@$_GET["email"]){
	if (!filter_var($_GET['email'], FILTER_VALIDATE_EMAIL)) {
    die( "This email not vaild!");
}
$checkmail = mysql_query("SELECT * FROM users WHERE email='".$_GET['email']."'");
$checkRecord = mysql_num_rows($checkmail);
if($checkRecord > 0){
	$getuserInformation = mysql_fetch_array($checkmail);
	$checkForgetRecordForUser = mysql_query("SELECT * FROM forget_passwords where userid='".make_safe($getuserInformation["id"])."'");
			$getInformation = mysql_fetch_array($checkForgetRecordForUser);

	$checkRecordForUser = mysql_num_rows($checkForgetRecordForUser);
		$code = rand(0000000000000000,9999999999999999);
	if($checkRecordForUser > 0){
		
	
	$updateCode = mysql_query("UPDATE forget_passwords SET code='".$code."' WHERE userid='".make_safe($getInformation["userid"])."'");


	}else{
		$insertRecord = mysql_query("INSERT INTO forget_passwords VALUES('".make_safe($getuserInformation["id"])."','".make_safe($code)."')");
		
	}
	$sendMail = mail($_GET["email"], 'Reset the Password', $code);
	if($sendMail){
echo "1";

}else{
	echo "Error in Mail Server! Try Again Later";
}
}else{
	echo "This Email is not registerd!!";

}
}
}else{
	echo "Please Fill All Fields";
}
}else{
	echo "Invalid SecureKey";
}
?>