<?php
/////////////////////////////////////////
//    Powered By : WindForce Studios   // 
/////////////////////////////////////////


include("config.php");
include("Key.php");

include('secureFunction.php');

if($SecureKey == $_GET['secure']){
if($_GET["id"]){
	$getAccount = mysql_query("SELECT * FROM users WHERE id='".make_safe($_GET["id"])."'");
	
$getInfo = mysql_fetch_array($getAccount);
$getNumAccount = mysql_num_rows($getAccount);

if($getNumAccount > 0 ){
echo "1,".$getInfo['id'].','.$getInfo['firstname'].','.$getInfo['lastname'].','.$getInfo['country'].','.$getInfo['username'].','.$getInfo['age'].','.$getInfo['email'];
}else{
	
	echo "Please Check Account id ".$getInfo['id'];
}
}else{
	echo "Please Check Account id ".$getInfo['id'];

	
}
}else{
	echo "SecureKey Invalid";
}

?>