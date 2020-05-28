<?php
/////////////////////////////////////////
//    Powered By : WindForce Studios   // 
/////////////////////////////////////////


include('config.php');
include('secureFunction.php');
include("Key.php");

if(@$_GET['secure'] != $SecureKey){
die("Secured! File");
}
if($_GET['username']){
$checkAccount = mysqli_query($connect, "SELECT * FROM users WHERE username='".make_safe($_GET['username'])."'");
if (!$checkAccount) {
            printf("Error: %s\n", mysqli_error($connect));
            exit();
        }

$getInfo = mysqli_fetch_array($checkAccount);
$checkCodeExist = mysqli_num_rows($checkAccount);
if($checkCodeExist >0){
	$getCode = mysqli_query($connect, "SELECT * FROM activate WHERE userid='".make_safe($getInfo['id'])."'");
if (!$getCode) {
            printf("Error: %s\n", mysqli_error($connect));
            exit();
        }
	if($getCode){
		//mail('tomlapin.com@gmail.com', 'My Subject', 'test');
		$fetchCode = mysqli_fetch_array($getCode);
		$sendMail = mail($getInfo["email"], 'Active Acount', $fetchCode["serial"]);
if($sendMail){
	
	echo "1,".$getInfo["email"];
}else{
	
	echo "Error in Mail Server";
}
	}

	
}
}else{
	echo "Please Try Again Later!";
}
	







?>