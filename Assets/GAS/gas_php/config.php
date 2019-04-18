<?php
/////////////////////////////////////////
//    Powered By : WindForce Studios   // 
/////////////////////////////////////////
$db_host        =    'localhost';
$db_user         =     'root';
$db_password     =     '';
$db_name        =    '';  

$connect     =     mysql_connect($db_host,$db_user,$db_password) or die(mysql_error());
$select     =     mysql_select_db($db_name) or die(mysql_error());  
mysql_query("set character_set_server='utf8'");
mysql_query("set names 'utf8'");
?>