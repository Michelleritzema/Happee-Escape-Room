<?php
	$image_path = 'img/modules/';
	$handle = opendir(dirname(realpath(__FILE__)).'/'.$image_path);
	$files_array = array();
	$files_array_size = 0;
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
				<span class="green_line">Bouw een escape room</span>
			</h2>
			<form class="json_form" action="" method="post">
				<table>
					<tr>
						<td><span class="module_header">School</span></td>
						<td><input id="schoolName" class="input" type="text" name="schoolName" maxlength="50" size="50"/></td>
					</tr>
					<tr>
						<td>
							<span class="module_header">Escape wachtwoord</span><br/>
							<span class="module_subheader">4 - 10 letters</span>
						</td>
						<td><input id="password" class="input" type="text" name="password" maxlength="10" size="12"/></td>
					</tr>
					<tr>
						<td>
							<span class="module_header">Duur van het spel</span><br/>
							<span class="module_subheader">in minuten</span>
						</td>
						<td><input id="totalMinutes" class="input" type="text" name="totalMinutes" maxlength="3" size="3" onkeypress='return event.charCode >= 48 && event.charCode <= 57'></input></td>
					</tr>
					<tr>
						<td><span class="module_header">Taal</span></td>
						<td>
							<select name="language">
								<option value="dutch">Nederlands</option>
								<option value="english">English</option>
							</select>
						</td>
					</tr>
				</table>
				<h4 class="build_page_subtitle center_text">
					Actieve modules<p>Kies er vier</p>
				</h4>
				<div>
					<?php
						$index = 1;
						while($file = readdir($handle)){
							if($file !== '.' && $file !== '..'){
								echo '<div class="module_div col-lg-3">
									<input type="checkbox" name="modules[]" value="'.basename($file, ".png").'" id="module'.$index.'" />
									<label for="module'.$index.'">
										<h4 class="module_title center_text">'.basename($file, ".png").'</h4>
										<img src="'.$image_path.$file.'" class="module_image" />
									</label>
								</div>';
								$files_array[] = $file;
								++$index;
							}
						}
						$files_array_size = count($files_array);
					?>
				</div>
				<div class="clear_float submit center_block">
					<input type="submit" value="Bouw game"/>
				</div>
			</form>
		</div>
		<div class="copyright center_text">
			<span class="copyright_text green_line">&copy; Happee, 2016</span>
		</div>
	</div>
	<script>
		$(function() {
			$('form').submit(function() {
				var schoolName = document.getElementById("schoolName");
				var password = document.getElementById("password");
				var totalMinutes = document.getElementById("totalMinutes");
				var buildStatus = [0, 0, 0, 0];
				if(schoolName.value.length > 0) {
					buildStatus[0] = 1;
					schoolName.style.border = "1px solid grey";
				} else
					schoolName.style.borderColor = "red";
				if(password.value.length > 3) {
					if(/^[a-zA-Z]+$/.test(password.value)) {
					   buildStatus[1] = 1;
						password.style.border = "1px solid grey";
					} else {
						alert("Het wachtwoord mag alleen uit letters bestaan");
						password.style.borderColor = "red";
						return false;
					}
				} else
					password.style.borderColor = "red";
				if(totalMinutes.value.length > 0) {
					buildStatus[2] = 1;
					totalMinutes.style.border = "1px solid grey";
				} else
					totalMinutes.style.borderColor = "red";
				$module_amount = 0;
				var current_amount = <?php echo $files_array_size; ?>;
				for ($i = 1; $i <= current_amount + 1; $i++) {
					if($('#module' + $i).prop('checked'))
						$module_amount++;
				}
				if($module_amount == 4)
					buildStatus[3] = 1;
				else {
					alert("Selecteer vier modules! Momenteel " + $module_amount + " geselecteerd");
					return false;
				}
				for(var i = 0; i < buildStatus.length; i++) {
					if(buildStatus[i] == 0) {
						alert("Een of meer velden zijn leeg of onjuist ingevuld");
						return false;
					}
				}
				window.open("game_download.php?json=" + JSON.stringify($('form').serializeObject()),"_self")
				return false;
			});
		});
	</script>
  </body>
</html>