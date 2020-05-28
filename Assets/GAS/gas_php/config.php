<?php
/////////////////////////////////////////
//    Powered By : WindForce Studios   // 
/////////////////////////////////////////
$db_host        =    'mysql-tom60chat.alwaysdata.net';
$db_user         =     'tom60chat';
$db_password     =     '&Topq654';
$db_name        =    'tom60chat_katycorp';  

$connect     =     mysqli_connect($db_host,$db_user,$db_password) or die(mysqli_error());
$select     =     mysqli_select_db($connect, $db_name) or die(mysqli_error());  
mysqli_query($connect, "set character_set_server='utf8'");
mysqli_query($connect, "set names 'utf8'");

// All erreor in a file :)
error_reporting(E_ALL); // Error engine - always TRUE!
ini_set('ignore_repeated_errors', TRUE); // always TRUE
ini_set('display_errors', TRUE); // Error display - FALSE only in production environment or real server
ini_set('log_errors', TRUE); // Error logging engine
ini_set('error_log', './php-error.log');
?>