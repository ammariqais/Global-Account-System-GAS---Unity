<?php
/////////////////////////////////////////
//    Powered By : WindForce Studios   // 
/////////////////////////////////////////
include("Key.php");
include("getip.php");
if(@$_GET["secure"] == $SecureKey ){
include('config.php');
include('secureFunction.php');
if(! empty($_GET['username']) and !empty($_GET['email']) and !empty($_GET['password'])){
if(strlen($_GET['password']) < 1){
die("The password most be bigger than 0 characters"); 
}if(strlen($_GET['username']) < 3){
die("The nickname most be bigger than 2 characters"); 
}if (!filter_var($_GET['email'], FILTER_VALIDATE_EMAIL)) {
    die( "This email not vaild!");
}
$checkmail = mysql_query("SELECT * FROM users WHERE email='".make_safe($_GET['email'])."'");
$checknickname = mysql_query("SELECT * FROM users WHERE username='".make_safe($_GET['username'])."'");

$getNumEmail = mysql_fetch_array($checkmail);
$getNumNickname = mysql_fetch_array($checknickname);

if($getNumEmail > 0 ){
die("This Email Is Already used");
}if($getNumNickname > 0 ){
die("This NickName Is Already used");
}if(ctype_space($getNumNickname)){
die("This NickName have WhiteSpace");
}
    if (!is_numeric($_GET["age"]) || $_GET["age"] > 80) {
		die("Wrong Age!!");
	}
if(@$_GET["activate"]){
$register = mysql_query("INSERT INTO users(id,username,email,password,country,age,firstname,lastname,macAddresses,ip) VALUES('NULL','".make_safe($_GET['username'])."','".make_safe($_GET['email'])."','".md5(make_safe($_GET['password']))."','".make_safe($_GET["country"])."','".make_safe($_GET["age"])."','".make_safe($_GET["firstname"])."','".make_safe($_GET["lastname"])."','".make_safe($_GET["mac"])."','".make_safe($real_ip_adress)."')");
if($register){
$getId = mysql_query("SELECT * from users where username='".make_safe($_GET['username'])."'");
$fetchID = mysql_fetch_array($getId);
$CreateSerial = rand(0000000000000000,9999999999999999);
$insertIntoActivate = mysql_query("INSERT INTO activate(userid,serial) values('".make_safe($fetchID["id"])."','".make_safe($CreateSerial)."')");
if($insertIntoActivate){
		$sendMail = mail($fetchID["email"], 'Active Acount', $CreateSerial);
		if($sendMail){
			echo "1";
		}else{
			
			echo "Error Mail Server Please Try Again Later";

			}

	
}

}else{
echo 'There is a problem in the database, please try again later!' ;
}
}else{
	$register = mysql_query("INSERT INTO users(id,username,email,password,country,age,firstname,lastname,active,macAddresses,ip) VALUES('NULL','".make_safe($_GET['username'])."','".make_safe($_GET['email'])."','".md5(make_safe($_GET['password']))."','".make_safe($_GET["country"])."','".make_safe($_GET["age"])."','".make_safe($_GET["firstname"])."','".make_safe($_GET["lastname"])."',1,'".make_safe($_GET["mac"])."','".make_safe($real_ip_adress)."')");
	

if($register){
echo "1" ;
}else{
echo 'There is a problem in the database, please try again later!' ;
}
}


}else{
die("Please Fill All required Fields");
}
}else{
	die("Can't Access!");
}




?>