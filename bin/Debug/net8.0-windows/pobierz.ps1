param (
    [string[]]$apps
)

$logPath = Join-Path $PSScriptRoot "logi_pobierz.txt"
if (Test-Path $logPath) { Remove-Item $logPath }

function Log($msg) { $msg | Tee-Object -FilePath $logPath -Append }

# Mapowanie nazw ListBox -> winget ID
$packageMap = @{
    "brave" = "Brave.Brave"
    "spotify" = "Spotify.Spotify"
    "visual studio code" = "Microsoft.VisualStudioCode"
    "npm" = "OpenJS.NodeJS.LTS"
    "git" = "Git.Git"
}

# Przygotowanie listy aplikacji
$appsList = @()
foreach ($app in $apps) {
    if ($app -is [string]) { $appsList += ($app -split ",") }
    elseif ($app -is [System.Array]) { $appsList += $app }
}
$appsList = $appsList | ForEach-Object { $_.ToLower().Trim() } | Where-Object { $_ -ne "" }

Log "Rozpoczynam instalacje aplikacji..."
foreach ($app in $appsList) {
    if (-not $packageMap.ContainsKey($app)) {
        Log "Nieznany pakiet: $app"
        continue
    }

    $id = $packageMap[$app]

    # Sprawdz, czy juz zainstalowany
    $installed = winget list --id $id -q 2>$null
    if ($installed) {
        Log "Juz zainstalowane: $app ($id)"
        continue
    }

    Log "Instalacja: $app ($id)"
    try {
        & winget install --id $id --scope user --silent --accept-package-agreements --accept-source-agreements
        if ($LASTEXITCODE -eq 0) { Log "Pomyślnie zainstalowano: $app" }
        else { Log "Blad instalacji: $app" }
    } catch { Log "Wyjatek: $($_.Exception.Message)" }
}

Log "Instalacja zakonczona."
