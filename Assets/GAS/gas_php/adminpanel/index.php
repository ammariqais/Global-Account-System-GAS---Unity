<?php
/////////////////////////////////////////
//    Powered By : WindForce Studios   // 
/////////////////////////////////////////
@session_start();

include('../config.php');
include('../secureFunction.php');


if(@$_SESSION["loginGASADMIN"]){
	if(@$_GET["banned"]){
		$getuser = mysql_query("SELECT * from users where id='".$_GET["banned"]."'");
		$fetchUSER = mysql_fetch_array($getuser);
		$getAllusersinDevice = mysql_query("SELECT * FROM USERS WHERE macAddresses='".make_safe($fetchUSER["macAddresses"])."'");
		while($bannedUser = mysql_fetch_array($getAllusersinDevice)){
		$banUser = mysql_query("UPDATE users SET banned=1 WHERE id='".make_safe($bannedUser["id"])."'");
		}
			
 echo "<meta http-equiv=\"refresh\" content=\"0;url=index.php\"></p>";

		
	}else if(@$_GET["unbanned"]){
				$getuser = mysql_query("SELECT * from users where id='".$_GET["unbanned"]."'");
		$fetchUSER = mysql_fetch_array($getuser);
		$getAllusersinDevice = mysql_query("SELECT * FROM USERS WHERE macAddresses='".make_safe($fetchUSER["macAddresses"])."'");
				while($unbannedUser = mysql_fetch_array($getAllusersinDevice)){

		$unbanUser = mysql_query("UPDATE users SET banned=0 WHERE id='".make_safe($unbannedUser["id"])."'");
		}
			
 echo "<meta http-equiv=\"refresh\" content=\"0;url=index.php\"></p>";

		
	
	
	
	}elseif(@$_GET["BannedFromIP"]){
				$getuser = mysql_query("SELECT * from users where id='".$_GET["BannedFromIP"]."'");
		$fetchUSER = mysql_fetch_array($getuser);
		$getAllusersinDevice = mysql_query("SELECT * FROM USERS WHERE ip='".make_safe($fetchUSER["ip"])."'");
		while($bannedUser = mysql_fetch_array($getAllusersinDevice)){
		$banUser = mysql_query("UPDATE users SET banned=1 WHERE id='".make_safe($bannedUser["id"])."'");
		}
			
 echo "<meta http-equiv=\"refresh\" content=\"0;url=index.php\"></p>";
		
		
	}else if(@$_GET["unBannedFromIP"]){
				$getuser = mysql_query("SELECT * from users where id='".$_GET["unBannedFromIP"]."'");
		$fetchUSER = mysql_fetch_array($getuser);
		$getAllusersinDevice = mysql_query("SELECT * FROM USERS WHERE ip='".make_safe($fetchUSER["ip"])."'");
				while($unbannedUser = mysql_fetch_array($getAllusersinDevice)){

		$unbanUser = mysql_query("UPDATE users SET banned=0 WHERE id='".make_safe($unbannedUser["id"])."'");
		}
			
 echo "<meta http-equiv=\"refresh\" content=\"0;url=index.php\"></p>";

		
	
	
	
	}
	
	elseif(@$_GET["delete"]){
		$DeleteUser = mysql_query("DELETE from users WHERE id='".make_safe($_GET["delete"])."'");
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
	$getRecords = mysql_query("SELECT * FROM users WHERE id");
		include('style/main.html');
}

}else{
 echo "<meta http-equiv=\"refresh\" content=\"0;url=login.php\"></p>";

}






?>