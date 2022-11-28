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

$sql = "SELECT type, category, size, name FROM womenPants WHERE id=" . $_GET["id"];
$result = $conn->query($sql);

if ($result->num_rows > 0) {
  $row = $result->fetch_assoc();

  // The page will be built here.

  echo '
<!DOCTYPE html>
<html>
  <head>
    <div class="content">
      <title>CLOBUNDANCE ' . $row["name"] . '</title>
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
      <div class="itempagetitle">' . $row["name"] . '<br></div>
      59.99
      <br>
      <br>
      <table border="1">
        <tr>
          <td>Name:</td>
          <td>' . $row["name"] . '</td>
        </tr>
        <tr>
          <td>Type:</td>
          <td>' . $row["type"] . '</td>
        </tr>
        <tr>
          <td>Category:</td>
          <td>' . $row["category"] . '</td>
        </tr>
        <tr>
          <td>Size:</td>
          <td>' . $row["size"] . '</td>
        </tr>
      </table>
  </body>
</html>';
}
$conn->close();
?> 