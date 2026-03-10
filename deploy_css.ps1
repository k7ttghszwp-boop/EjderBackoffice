$projects = @("Ejder.Web.Public", "EjderBackoffice.Web", "Ejder.CallCenter.Web", "Ejder.Employee.Web", "Ejder.Marketing.Web")
foreach ($p in $projects) {
    $dest = "src/$p/wwwroot/css"
    if (Test-Path $dest) {
        Copy-Item "src/SharedAssets/css/ejder-ui.css" -Destination "$dest/ejder-ui.css" -Force
        Write-Host "Copied to $dest"
    } else {
        Write-Host "Skipped ${p} - Path not found"
    }
}
