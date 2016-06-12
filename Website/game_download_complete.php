<!-- Created by Michelle Ritzema -->

<?php
	ini_set('max_execution_time', 600);
	ini_set('memory_limit','1024M');

	$GLOBALS['location_settings'] = "download/settings.json";
	$GLOBALS['location_game'] = "C:/Users/0881495/Desktop/HPLab_Project/Modules/Init.exe";
	$GLOBALS['location_game_data'] = "C:/Users/0881495/Desktop/HPLab_Project/Modules/Init_Data";
	$GLOBALS['location_player_win1'] = "C:/Users/0881495/Desktop/HPLab_Project/Modules/player_win_x86.pdb";
	$GLOBALS['location_player_win2'] = "C:/Users/0881495/Desktop/HPLab_Project/Modules/player_win_x86_s.pdb";
	$GLOBALS['readme'] = "C:/Users/0881495/Desktop/HPLab_Project/Modules/README.txt";
	
	//Original from: https://davidwalsh.name/create-zip-php
	/*
		Creates a zip file containing the game exe file, settings json file, 
		Init_Data directory and a README text file.
	*/
	function createZip($destination = '', $overwrite = true) {
		if(file_exists($destination) && !$overwrite)
			return false;
		$zip = new ZipArchive();
		if($zip->open($destination,$overwrite ? ZIPARCHIVE::OVERWRITE : ZIPARCHIVE::CREATE) !== true)
			return false;
		addToZip($zip, $GLOBALS['location_settings'], "settings.json");
		addToZip($zip, $GLOBALS['location_game'], "Init.exe");
		addToZip($zip, $GLOBALS['location_player_win1'], "player_win_x86.pdb");
		addToZip($zip, $GLOBALS['location_player_win2'], "player_win_x86_s.pdb");
		addToZip($zip, $GLOBALS['readme'], "README.txt");
		addFolderToZip($GLOBALS['location_game_data'], $zip, 1);
		$zip->close();
		return file_exists($destination);
	}
	
	/*
		Adds the supplied file to the zip
	*/
	function addToZip($zip, $location, $destination) {
		$zip->addFile($location, $destination);
	}
	
	/*
		Adds a directory recursively to a zip, by visiting all the files inside.
		If the found file is a directory, it is added to an array and will be traversed later.
	*/
	function addFolderToZip($dir, $zip, $layer) {
		$folders = array();
		if(is_dir($dir)) {
			$files = glob($dir."/*");
			$index = 0;
			foreach($files as $file) {
				$path = getPath($file, $layer);
				$file_name_remove = preg_replace("/[^\/]+$/", '', $file);
				$file_name = str_replace($file_name_remove, "", $file);
				if(is_file($file))
					addToZip($zip, $file, $path);
				else {
					if(($path !== ".") && ($path !== ".."))
						$folders[count($folders)] = $dir."/".$file_name;
				}
				++$index;
			}
			goIntoFolders($zip, $folders, $layer);
		}
	}
	
	/*
		Iterates over the folders array and adds the folder to the zip file.
	*/
	function goIntoFolders($zip, $folders, $layer) {
		if(count($folders) > 0) {
			$new_layer = ++$layer;
			foreach($folders as $directory)
				addFolderToZip($directory, $zip, $new_layer);
		}
	}
	
	/*
		Fetches the file path that is used in the zip file with regex.
		The shape of the regex is determined by the layer of depth.
	*/
	function getPath($file, $layer) {
		$preg_insert = "[^\/]+\/";
		$preg_layer = "";
		for($i = 0; $i < $layer; $i++)
			$preg_layer .= $preg_insert;
		$preg_match = "/".$preg_layer."[^\/]+$/";
		$to_remove = preg_replace($preg_match, '', $file);
		$path = str_replace($to_remove, "", $file);
		return $path;
	}
	
	createZip("download/escape_room.zip");
?>

<!DOCTYPE html>
<html>
  <head>
	<title>InIT Escape Room - Happee</title>
	<link rel="stylesheet" type="text/css" href="css/happee.css">
	<link rel="stylesheet" type="text/css" href="css/bootstrap.min.css">
	<link rel="stylesheet" href="css/bootstrap-theme.min.css">
	<script src="js/jquery-2.2.4.min.js"></script>
	<script src="js/bootstrap.min.js"></script>
	<script src="js/jquery.serialize-object.js"></script>
  </head>
  <body>
	<?php include 'header.php';?>
	<div class="container">
		<?php include 'menu.php';?>
		<div class="build_page">
			<h2 class="build_page_title center_text">
				<span class="green_line">Download compleet</span>
			</h2>
			<div id="download_zip" class="center_text med_text">
				Als er nu geen bestand gedownload is, kan het zijn dat er een pop-up geblokeerd wordt.</br>
				Sta de website in dit geval toe en probeer het opnieuw.</br>
				<a class="hyperlink" href="">Download opnieuw</a>
			</div>
			<div class="center_text med_text">
				Binnen het gedownloade zip bestand bevindt zich een README bestand.</br>
				Volg deze stappen om het spel op te zetten op uw computer systeem.</br></br>
				<a class="hyperlink" href="index.php">Terug naar de homepage</a>.
			</div>
		</div>
		<div class="copyright center_text">
			<span class="copyright_text green_line">&copy; Happee, 2016</span>
		</div>
	</div>
	<script>
		window.open('download/escape_room.zip','_blank');
		$(document).ready(function() {
			$("#download_zip a").click(function() {
				window.open('download/escape_room.zip','_blank');
			});
		});
	</script>
  </body>
</html>