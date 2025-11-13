# Narzędzia Systemowe

Aplikacja desktopowa WPF dla systemu Windows, która pomaga w zarządzaniu i optymalizacji systemu operacyjnego.

## Opis projektu

Narzędzia Systemowe to aplikacja stworzona w technologii WPF (.NET 8.0), która oferuje zestaw użytecznych narzędzi do zarządzania systemem Windows. Aplikacja wykorzystuje skrypty PowerShell do wykonywania operacji systemowych.

## Funkcjonalności

### 🧹 Czyszczenie systemu
- Automatyczne czyszczenie folderów tymczasowych
- Usuwanie plików z Prefetch
- Czyszczenie kosza systemowego
- Szczegółowe logowanie operacji z informacją o zwolnionej przestrzeni

### 🔍 Skanowanie dysku
- Wyszukiwanie plików według zakresu rozmiarów (w MB)
- Możliwość określenia własnej ścieżki do skanowania
- Szczegółowy raport ze znalezionymi plikami i ich rozmiarami
- Logowanie postępu skanowania

### 📦 Instalacja aplikacji
Automatyczna instalacja popularnych aplikacji za pomocą package managera:
- Brave Browser
- Spotify
- Visual Studio Code
- npm
- Git

## Wymagania systemowe

- System operacyjny: Windows 10/11
- .NET Runtime 8.0 lub nowszy
- PowerShell
- Uprawnienia administratora (dla niektórych operacji)

## Instalacja

1. Sklonuj repozytorium:
```bash
git clone https://github.com/Alxay7/Narzedzia-Systemowe.git
```

2. Otwórz rozwiązanie w Visual Studio:
```
WPF_powershell.sln
```

3. Zbuduj projekt (Build → Build Solution)

4. Uruchom aplikację z poziomu Visual Studio lub z folderu `bin/Debug/net8.0-windows/`

## Użytkowanie

### Czyszczenie systemu
1. Kliknij przycisk "Wyczyść śmieci"
2. Aplikacja automatycznie wyczyści foldery tymczasowe
3. Szczegóły operacji pojawią się w sekcji "Logi"

### Skanowanie dysku
1. Wprowadź zakres rozmiarów plików (Od - Do w MB)
2. Podaj ścieżkę do skanowania (domyślnie: C:\Users\User\Downloads)
3. Kliknij "Skanuj dysk"
4. Lista znalezionych plików pojawi się w sekcji "Logi"

### Instalacja aplikacji
1. Zaznacz aplikacje do pobrania z listy (możliwość wielokrotnego wyboru)
2. Kliknij "Pobierz"
3. Aplikacje zostaną automatycznie zainstalowane
4. Status instalacji pojawi się w sekcji "Logi"

## Struktura projektu

```
Narzedzia-Systemowe/
├── WPF_powershell/
│   ├── MainWindow.xaml          # Interfejs użytkownika
│   ├── MainWindow.xaml.cs       # Logika aplikacji
│   ├── App.xaml                 # Konfiguracja aplikacji
│   ├── App.xaml.cs
│   ├── AssemblyInfo.cs
│   └── bin/Debug/net8.0-windows/
│       ├── czysc.ps1            # Skrypt czyszczenia
│       ├── skanuj.ps1           # Skrypt skanowania
│       └── pobierz.ps1          # Skrypt instalacji
└── WPF_powershell.sln
```

## Skrypty PowerShell

### czysc.ps1
Automatycznie czyści:
- `%USERPROFILE%\AppData\Local\Temp`
- `%SystemRoot%\Temp`
- `%SystemRoot%\Prefetch`
- `$Recycle.Bin`

### skanuj.ps1
Skanuje określony folder w poszukiwaniu plików o określonym rozmiarze.

Parametry:
- `min` - minimalny rozmiar w MB
- `max` - maksymalny rozmiar w MB
- `sciezka` - ścieżka do skanowania

### pobierz.ps1
Instaluje wybrane aplikacje przy użyciu odpowiedniego package managera.

## Bezpieczeństwo

⚠️ **Uwaga**: Niektóre operacje wymagają uprawnień administratora. Aplikacja wykonuje nieodwracalne operacje na systemie plików (usuwanie plików). Używaj z rozwagą.

## Technologie

- C# 12
- WPF (Windows Presentation Foundation)
- .NET 8.0
- PowerShell
- XAML

## Rozwój

Aby przyczynić się do rozwoju projektu:

1. Utwórz fork repozytorium
2. Stwórz branch dla swojej funkcjonalności (`git checkout -b feature/nowa-funkcjonalnosc`)
3. Zacommituj zmiany (`git commit -am 'Dodano nową funkcjonalność'`)
4. Wypchnij branch (`git push origin feature/nowa-funkcjonalnosc`)
5. Otwórz Pull Request

## Licencja

Projekt jest dostępny na zasadach określonych przez autora repozytorium.

## Autor

[Alxay7](https://github.com/Alxay7)

## Kontakt

W razie pytań lub problemów, proszę o otwarcie Issue w repozytorium GitHub.
