<?php
/////////////////////////////////////////
//    Powered By : WindForce Studios   // 
/////////////////////////////////////////

include("config.php");

function make_safe($str)
{
    return htmlspecialchars(addslashes(trim($str)));
}  
?>