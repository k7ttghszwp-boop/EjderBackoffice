param(
    [switch] $OnlyCoreApps
)

$ErrorActionPreference = "Stop"

$solutionRoot = Split-Path -Parent $MyInvocation.MyCommand.Path

# Çalıştırılacak projeler (gerekirse burayı düzenleyebilirsin)
$projects = @(
    "src\Ejder.Api",
    "src\EjderBackoffice.Web",
    "src\Ejder.Web.Public",
    "src\Ejder.Employee.Web",
    "src\Ejder.CallCenter.Web"
)

if ($OnlyCoreApps) {
    # Sadece API + Backoffice
    $projects = @(
        "src\Ejder.Api",
        "src\EjderBackoffice.Web"
    )
}

Write-Host "Solution klasörü: $solutionRoot" -ForegroundColor Cyan
Write-Host "Aşağıdaki projeler ayrı PowerShell pencerelerinde başlatılacak:" -ForegroundColor Cyan
$projects | ForEach-Object { Write-Host " - $_" }

foreach ($project in $projects) {
    $fullPath = Join-Path $solutionRoot $project

    if (-not (Test-Path $fullPath)) {
        Write-Warning "Klasör bulunamadı, atlanıyor: $fullPath"
        continue
    }

    Write-Host "Başlatılıyor: $fullPath" -ForegroundColor Green

    # API için sabit port (http://localhost:5185) ayarla
    if ($project -eq "src\Ejder.Api") {
        Start-Process powershell -ArgumentList @(
            "-NoExit",
            "-Command",
            "cd `"$fullPath`"; $env:ASPNETCORE_URLS='http://localhost:5185'; dotnet run"
        ) | Out-Null
    }
    else {
        # Diğer projeler için normal dotnet run
        Start-Process powershell -ArgumentList @(
            "-NoExit",
            "-Command",
            "cd `"$fullPath`"; dotnet run"
        ) | Out-Null
    }
}

Write-Host "Tüm seçili projeler için dotnet run komutları gönderildi." -ForegroundColor Cyan

