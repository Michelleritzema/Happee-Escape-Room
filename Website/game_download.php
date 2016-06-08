<?php
	if(isset($_GET["json"])) {
		$json = $_GET["json"];
		$fp = fopen('download/settings.json', 'w');
		fwrite($fp, $json);
		fclose($fp);
	}
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
  </head>
  <body>
	<?php include 'header.php';?>
	<div class="container">
		<?php include 'menu.php';?>
		<div class="build_page">
			<h2 class="build_page_title center_text">
				<span class="green_line">Wachten op download</span>
			</h2>
			<div id="info" class="center_text med_text">
				Uw escape room game wordt nu in elkaar gezet en ingepakt in een zip bestand.</br>
				Dit kan tot tien minuten duren.
			</div>
			<img id="loader" class="center_block" src="img/loader.gif" />
			<div id="info" class="center_text med_text">
				<span class="underlined">Let op</span></br>De zip file kan geblokkeerd worden door de 
				browser.</br>In dat geval moet u de download handmatig toestaan.
			</div>
		</div>
		<div class="copyright center_text">
			<span class="copyright_text green_line">&copy; Happee, 2016</span>
		</div>
	</div>
	<script>
		window.open("game_download_complete.php","_self");
	</script>
  </body>
</html>