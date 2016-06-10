<?php

require 'database.php';
$Username = $_POST["Username"];
$Password = $_POST["Password"];
    if (!$Username || !$Password ) {
        echo "Niet alles is in ingevuld";
    }
    else
    {
        $stmt = $dbh->query('SELECT * FROM accounts where username ="'.$Username.'"');
        $row_count = $stmt->rowCount();
        if($row_count == 0)
        {
            $password = encrypt($Password,$key);
            $stmt = $dbh->prepare("INSERT INTO accounts (username, password) VALUES (:username, :password)");
            $stmt->bindParam(':username', $Username);
            $stmt->bindParam(':password', $password);
            $stmt->execute();
            echo "Registratie successful";
        }
        else
        {
            echo "Gekozen naam is al in gebruik";
        }