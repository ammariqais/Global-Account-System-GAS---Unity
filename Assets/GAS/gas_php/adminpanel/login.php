<?php
/////////////////////////////////////////
//    Powered By : WindForce Studios   // 
/////////////////////////////////////////
@session_start();

include('../config.php');
include('../secureFunction.php');
$MSG = '' ;

if(@$_GET["check"]){
$checkmail = mysqli_query($connect, "SELECT * FROM admins WHERE username='".make_safe($_POST["username"])."' AND password='".make_safe($_POST["password"])."'");
if (!$checkmail) {
    printf("Error: %s\n", mysqli_error($connect));
    exit();
}

$getInfo = mysqli_fetch_array($checkmail);
$getNumAccount = mysqli_num_rows($checkmail);

if($getNumAccount > 0 ){

	$_SESSION["loginGASADMIN"] = $_POST["username"];
 echo "<meta http-equiv=\"refresh\" content=\"0;url=index.php\"></p>";
}else{
$MSG = 'Wrong username or password' ;
	include('style/loginform.html');

}
}else{
	include('style/loginform.html');

}






?>