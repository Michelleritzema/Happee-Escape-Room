<?php

require 'database.php';
$TeamName = $_POST["teamName"];
$FinishTime = $_POST["endTime"];
$Module1Tijd = $_POST["module1Time"];    
$Module2Tijd = $_POST["module2Time"];    
$Module3Tijd = $_POST["module3Time"];    
$Module4Tijd = $_POST["module4Time"];    
if (!$TeamName || !$FinishTime ) {
        echo "Zorg voor minimaal de team naam en de eind tijd";
    }
    else
    {
        $stmt = $dbh->query('SELECT * FROM game_stats where team_naam ="'.$TeamName.'"');
        $row_count = $stmt->rowCount();
        if($row_count == 0)
        {
            $stmt = $dbh->prepare("INSERT INTO accounts (team_naam, totale_tijd, test_module_medical_tijd, test_module_1_tijd, test_module_2_tijd, test_module_3_tijd) 
			VALUES (:TeamName, :FinishTime, :Module1Tijd, :Module2Tijd, :Module3Tijd, :Module4Tijd)");
            $stmt->bindParam(':TeamName', $TeamName);
			$stmt->bindParam(':FinishTime', $FinishTime);
            $stmt->bindParam(':Module1Tijd', $Module1Tijd);
			$stmt->bindParam(':Module2Tijd', $Module2Tijd);
			$stmt->bindParam(':Module3Tijd', $Module3Tijd);
			$stmt->bindParam(':Module4Tijd', $Module4Tijd);
            $stmt->execute();
            echo "Registratie successful";
        }
        else
        {
            echo "Gekozen naam is al in gebruik";

		}
	}