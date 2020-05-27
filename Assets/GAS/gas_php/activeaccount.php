<?php
/////////////////////////////////////////
//    Powered By : WindForce Studios   // 
/////////////////////////////////////////


include('config.php');
include('secureFunction.php');

include("Key.php");
if($_GET['secure'] != $SecureKey){
die("Secured! File");
}
if(!empty($_GET['code'])){

$checkCode = mysqli_query($connect, "SELECT * FROM activate WHERE serial='".make_safe($_GET['code'])."'");
        if (!$checkCode) {
            printf("Error: %s\n", mysqli_error($connect));
            exit();
        }
$getInfo = mysqli_fetch_array($checkCode);
$checkCodeExist = mysqli_num_rows($checkCode);

if($checkCodeExist > 0 ){
		$ActiveAccount = mysqli_query($connect, "UPDATE users SET active=1 WHERE id='".make_safe($getInfo["userid"])."'");
        if (!$ActiveAccount) {
            printf("Error: %s\n", mysqli_error($connect));
            exit();
        }

if($ActiveAccount){
	$deleteCode = mysqli_query($connect, "DELETE FROM activate WHERE serial='".make_safe($_GET['code'])."'");
        if (!$deleteCode) {
            printf("Error: %s\n", mysqli_error($connect));
            exit();
        }
	if($deleteCode){
	echo "1";
	}
	else{
		
		echo "Unkown Problem ;(";
	}
}else{
	echo "Unkown Problem ;( Please Try Again Later";
}
		
		}else{
echo 'Wrong Code Activate' ;
}

}
else{
die("Please Fill All Field!");
}




?>