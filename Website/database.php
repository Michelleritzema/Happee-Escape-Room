<?php

$hostname = "127.0.0.1:3306";
$DBName = "escape_room";
$user = "HPLab";
$PasswordDB ="happee";
try {

    $dbh = new PDO('mysql:host='. $hostname .';dbname='. $DBName, $user, $PasswordDB);
    echo "Database connection is successful  ";
}
catch(PDOException $e)
{
    echo '<h1>An error has ocurred.</h1><pre>', $e->getMessage(), '</pre>';
}