# Sciezka do pliku logow w tym samym folderze co skrypt
$logFile = Join-Path -Path $PSScriptRoot -ChildPath "logi.txt"

# Funkcja czyszczenia folderu
function Clear-Folder($folderPath) {
    if (Test-Path $folderPath) {
        $items = Get-ChildItem -Path $folderPath -Recurse -Force -ErrorAction SilentlyContinue
        foreach ($item in $items) {
            try {
                $size = 0
                if ($item.PSIsContainer) {
                    $size = (Get-ChildItem -Path $item.FullName -Recurse -Force -ErrorAction SilentlyContinue | Measure-Object -Property Length -Sum).Sum
                    Remove-Item -Path $item.FullName -Recurse -Force -ErrorAction SilentlyContinue
                } else {
                    $size = $item.Length
                    Remove-Item -Path $item.FullName -Force -ErrorAction SilentlyContinue
                }

                $sizeMB = [math]::Round($size / 1MB, 2)
                $logEntry = "$(Get-Date -Format 'yyyy-MM-dd HH:mm:ss') | Usunieto: $($item.FullName) | Rozmiar: $sizeMB MB"
                Add-Content -Path $logFile -Value $logEntry
            } catch {
                # Ignorujemy bledy
            }
        }
    }
}

# Foldery do czyszczenia
$tempUser = "$env:USERPROFILE\AppData\Local\Temp"
$tempSystem = "$env:SystemRoot\Temp"
$prefetch = "$env:SystemRoot\Prefetch"
$recycleBin = [IO.Path]::Combine($env:SystemDrive, '$Recycle.Bin')

# Czyszczenie folderow
Clear-Folder $tempUser
Clear-Folder $tempSystem
Clear-Folder $prefetch
Clear-Folder $recycleBin

Write-Host "Czyszczenie zakonczone. Szczegoly w logi.txt"
