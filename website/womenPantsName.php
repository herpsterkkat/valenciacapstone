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

$sql = 'SELECT id, type, category, size, name FROM womenPants WHERE name="' . $_GET["name"] . '"';
$result = $conn->query($sql);

if ($result->num_rows > 0) {
  // The page will be built here.
  echo '
<!DOCTYPE html>
<html>
  <div class="content">
    <head>
      <title>CLOBUNDANCE - WOMENS - PANTS</title>
      <link rel ="stylesheet" href="stylesheet.css">
      <link href="https://fonts.googleapis.com/css?family=Montserrat" rel="stylesheet">
      <link href="https://fonts.googleapis.com/css?family=Roboto" rel="stylesheet">

      <a href="index.html"><h1>CLOBUNDANCE</h1></a>

    </head>

    <body>
    <div class="categoryrow">
      <div class="itemimageplacement">
        <img src="images/womenPants.jpg">
      </div>
      <div class="itemfontplacement">
        <div class="itempagetitle">' . $_GET["name"] . '<br></div>
        59.99
        <br>
        <br>
        <b>Available sizes:</b>
        <br>
        <br>
';

        // Generate a unique list of available items.

        while($row = $result->fetch_assoc()) {
            echo '<a href="womenPantsItem.php?id=' . $row["id"] . '">' . $row["size"] . "</a><br>\r\n";
        }

  echo '
      </div>
    </div>
  </div>
</html>';
}
$conn->close();
?> 