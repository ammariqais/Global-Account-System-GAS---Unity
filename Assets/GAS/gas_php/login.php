<?php
/////////////////////////////////////////
//    Powered By : WindForce Studios   // 
/////////////////////////////////////////


include('config.php');
include('secureFunction.php');
include("getip.php");

include("Key.php");
if(!empty($_GET['username']) and !empty($_GET['password']) and !empty($_GET['secureid'])){
if($_GET['secureid'] != $SecureKey){
die("Secured! File");
}

$checkmail = mysql_query("SELECT * FROM users WHERE username='".make_safe($_GET['username'])."' AND password='".md5(make_safe($_GET['password']))."'");

$getInfo = mysql_fetch_array($checkmail);
$getNumAccount = mysql_num_rows($checkmail);

if($getNumAccount > 0 ){
	$update_user_ip = mysql_query("UPDATE users SET ip='".make_safe($real_ip_adress)."' WHERE id='".make_safe($getInfo['id'])."'");
	if($update_user_ip){
if($getInfo["active"] == 1){
echo "1,".$getInfo['id'].','.$getInfo['firstname'].','.$getInfo['lastname'].','.$getInfo['country'].','.$getInfo['username'].','.$getInfo['age'].','.$getInfo['email'];
}else{
	echo "2";
}
	}
}else{
echo 'Wrong username or password' ;
}

}
else{
die("Please Fill All Field!");
}




?>