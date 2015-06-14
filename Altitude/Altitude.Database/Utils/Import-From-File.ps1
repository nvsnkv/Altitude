param($file)
Add-Type -Path ..\Altitude.Database.dll

$importer = New-Object Altitude.Database.Import.FileImporter
$importer.Import.Invoke($file)