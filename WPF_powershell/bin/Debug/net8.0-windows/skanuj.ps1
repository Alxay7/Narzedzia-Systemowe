param (
    [string]$apps  # Oczekujemy formatu "min,max,sciezka"
)

# Sciezka do logu w katalogu aplikacji
$logPath = Join-Path $PSScriptRoot "logi_scan.txt"

# Parsowanie parametrow
$parts = $apps -split ","
if ($parts.Length -ne 3) {
    Write-Host "Niepoprawny format parametrow. Oczekiwano: min,max,sciezka"
    exit
}

$minMB = [double]$parts[0]
$maxMB = [double]$parts[1]
$folder = $parts[2].Trim()

# Sprawdzenie czy folder istnieje
if (-not (Test-Path $folder)) {
    Write-Host "Podana sciezka nie istnieje: $folder"
    exit
}

# Konwersja MB na bajty
$minBytes = $minMB * 1MB
$maxBytes = $maxMB * 1MB

# Czyscimy poprzedni log
if (Test-Path $logPath) { 
    Write-Host "Usuwam stary log..."
    Remove-Item $logPath 
}

Write-Host "Rozpoczynam skanowanie folderu $folder..."

# Lista do przechowywania wynikow
$lines = New-Object System.Collections.Generic.List[System.String]

$count = 0
$found = 0
Get-ChildItem -Path $folder -Recurse -File -ErrorAction SilentlyContinue | ForEach-Object {
    $size = $_.Length
    if ($size -ge $minBytes -and $size -le $maxBytes) {
        $line = "$($_.FullName) | $([math]::Round($size / 1MB,2)) MB"
        $lines.Add($line)
        $found++
        Write-Host "Dodano do logu: $line"
    }
    $count++
    if ($count % 100 -eq 0) { Write-Host "$count plikow sprawdzonych..." }
}

Write-Host "Skanowanie zakonczone. Znaleziono $found plikow w zadanym zakresie."

# Zapis wszystkich wynikow jednorazowo
$lines | Out-File -FilePath $logPath -Encoding UTF8 -Force

Write-Host "Wyniki zapisano w $logPath"
