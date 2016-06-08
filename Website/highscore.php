<!DOCTYPE html>
<html>
  <head>
	<title>InIT Escape Room - Happee</title>
	<link rel="stylesheet" type="text/css" href="css/happee.css">
	<link rel="stylesheet" type="text/css" href="css/bootstrap.min.css">
	<link rel="stylesheet" href="css/bootstrap-theme.min.css">
	<script src="js/jquery-2.2.4.min.js"></script>
	<script src="js/bootstrap.min.js"></script>
	<script src="https://cdn.plot.ly/plotly-latest.min.js"></script>
  </head>
  <body>
	<?php include 'header.php';?>
	<div class="container">
		<?php include 'menu.php';?>
		<div class="build_page">
			<h2 class="build_page_title">
				<span class="green_line">Totale tijd in minuten per school</span>
			</h2>
			<div id="plotly-div">
				<script src="js/plotlybar.js"></script>
			</div>
		</div>
		<div class="copyright center_text">
			<span class="copyright_text green_line">&copy; Happee, 2016</span>
		</div>
	</div>
  </body>
</html>