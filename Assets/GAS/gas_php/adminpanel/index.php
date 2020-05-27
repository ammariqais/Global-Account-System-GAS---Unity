<?php
/////////////////////////////////////////
//    Powered By : WindForce Studios   // 
/////////////////////////////////////////
@session_start();

include('../config.php');
include('../secureFunction.php');


if(@$_SESSION["loginGASADMIN"]){
	if(@$_GET["banned"]){
		$getuser = mysqli_query($connect, "SELECT * from users where id='".$_GET["banned"]."'");
        if (!$getuser) {
            printf("Error: %s\n", mysqli_error($connect));
            exit();
        }
		$fetchUSER = mysqli_fetch_array($getuser);
		$getAllusersinDevice = mysqli_query($connect, "SELECT * FROM users WHERE macAddresses='".make_safe($fetchUSER["macAddresses"])."'");
        if (!$getAllusersinDevice) {
            printf("Error: %s\n", mysqli_error($connect));
            exit();
        }
		while($bannedUser = mysqli_fetch_array($getAllusersinDevice)){
		$banUser = mysqli_query($connect, "UPDATE users SET banned=1 WHERE id='".make_safe($bannedUser["id"])."'");
        if (!$banUser) {
            printf("Error: %s\n", mysqli_error($connect));
            exit();
        }
		}
			
 echo "<meta http-equiv=\"refresh\" content=\"0;url=index.php\"></p>";

		
	}else if(@$_GET["unbanned"]){
				$getuser = mysqli_query($connect, "SELECT * from users where id='".$_GET["unbanned"]."'");
        if (!$getuser) {
            printf("Error: %s\n", mysqli_error($connect));
            exit();
        }
		$fetchUSER = mysqli_fetch_array($getuser);
		$getAllusersinDevice = mysqli_query($connect, "SELECT * FROM users WHERE macAddresses='".make_safe($fetchUSER["macAddresses"])."'");
        if (!$getAllusersinDevice) {
            printf("Error: %s\n", mysqli_error($connect));
            exit();
        }
				while($unbannedUser = mysqli_fetch_array($getAllusersinDevice)){

		$unbanUser = mysqli_query($connect, "UPDATE users SET banned=0 WHERE id='".make_safe($unbannedUser["id"])."'");
        if (!$unbanUser) {
            printf("Error: %s\n", mysqli_error($connect));
            exit();
        }
		}
			
 echo "<meta http-equiv=\"refresh\" content=\"0;url=index.php\"></p>";

		
	
	
	
	}elseif(@$_GET["BannedFromIP"]){
				$getuser = mysqli_query($connect, "SELECT * from users where id='".$_GET["BannedFromIP"]."'");
        if (!$getuser) {
            printf("Error: %s\n", mysqli_error($connect));
            exit();
        }
		$fetchUSER = mysqli_fetch_array($getuser);
		$getAllusersinDevice = mysqli_query($connect, "SELECT * FROM users WHERE ip='".make_safe($fetchUSER["ip"])."'");
        if (!$getAllusersinDevice) {
            printf("Error: %s\n", mysqli_error($connect));
            exit();
        }
		while($bannedUser = mysqli_fetch_array($getAllusersinDevice)){
		$banUser = mysqli_query($connect, "UPDATE users SET banned=1 WHERE id='".make_safe($bannedUser["id"])."'");
        if (!$banUser) {
            printf("Error: %s\n", mysqli_error($connect));
            exit();
        }
		}
			
 echo "<meta http-equiv=\"refresh\" content=\"0;url=index.php\"></p>";
		
		
	}else if(@$_GET["unBannedFromIP"]){
				$getuser = mysqli_query($connect, "SELECT * from users where id='".$_GET["unBannedFromIP"]."'");
        if (!$getuser) {
            printf("Error: %s\n", mysqli_error($connect));
            exit();
        }
		$fetchUSER = mysqli_fetch_array($getuser);
		$getAllusersinDevice = mysqli_query($connect, "SELECT * FROM users WHERE ip='".make_safe($fetchUSER["ip"])."'");
        if (!$getAllusersinDevice) {
            printf("Error: %s\n", mysqli_error($connect));
            exit();
        }
				while($unbannedUser = mysqli_fetch_array($getAllusersinDevice)){

		$unbanUser = mysqli_query($connect, "UPDATE users SET banned=0 WHERE id='".make_safe($unbannedUser["id"])."'");
        if (!$unbanUser) {
            printf("Error: %s\n", mysqli_error($connect));
            exit();
        }
		}
			
 echo "<meta http-equiv=\"refresh\" content=\"0;url=index.php\"></p>";

		
	
	
	
	}
	
	elseif(@$_GET["delete"]){
		$DeleteUser = mysqli_query($connect, "DELETE from users WHERE id='".make_safe($_GET["delete"])."'");
        if (!$DeleteUser) {
            printf("Error: %s\n", mysqli_error($connect));
            exit();
        }
		if($DeleteUser){
			 echo "<meta http-equiv=\"refresh\" content=\"0;url=index.php\"></p>";
		}
		
	}elseif(@$_GET['logout']){
unset($_SESSION['loginGASADMIN']);

if(!isset($_SESSION['loginGASADMIN'])){
 echo "<meta http-equiv=\"refresh\" content=\"0;url=index.php\"></p>";  
}
}
	
	else{
	$getRecords = mysqli_query($connect, "SELECT * FROM users WHERE id");
        if (!$getRecords) {
            printf("Error: %s\n", mysqli_error($connect));
            exit();
        }
		include('style/main.html');
}

}else{
 echo "<meta http-equiv=\"refresh\" content=\"0;url=login.php\"></p>";
}

?>