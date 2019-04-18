<?php
/////////////////////////////////////////
//    Powered By : WindForce Studios   // 
/////////////////////////////////////////

function make_safe($str)
{
    return htmlspecialchars(addslashes(trim($str)));
}  
?>