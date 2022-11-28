<?php
$servername = "localhost";
$username = "capstone";
$password = "REDACTED_PASSWORD";
$dbname = "capstone";

// Create connection
$conn = new mysqli($servername, $username, $password, $dbname);
// Check connection
if ($conn->connect_error) {
  die("Connection failed: " . $conn->connect_error);
}

$sql = "SELECT DISTINCT name FROM womenShirt";
$result = $conn->query($sql);

if ($result->num_rows > 0) {
  // The page will be built here.
  echo '
<!DOCTYPE html>
<html>
  <div class="content">
    <head>
      <title>CLOBUNDANCE - WOMENS - SHIRTS</title>
      <link rel ="stylesheet" href="stylesheet.css">
      <link href="https://fonts.googleapis.com/css?family=Montserrat" rel="stylesheet">
      <link href="https://fonts.googleapis.com/css?family=Roboto" rel="stylesheet">

      <a href="index.html"><h1>CLOBUNDANCE</h1></a>

    </head>

    <body>
    <div class="categoryrow">
      <div class="itemimageplacement">
        <img src="images/womenShirt.jpg">
      </div>
      <div class="itemfontplacement">
        <div class="itempagetitle">Shirts<br></div>
        59.99
        <br>
        <br>
';

        // Generate a unique list of item names.

        while($row = $result->fetch_assoc()) {
            echo '<a href="womenShirtName.php?name=' . $row["name"] . '">' . $row["name"] . "</a><br>\r\n";
        }

  echo '
      </div>
    </div>
  </div>
</html>';
}
$conn->close();
?> 