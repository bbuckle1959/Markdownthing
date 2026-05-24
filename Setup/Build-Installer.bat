@echo off
REM MarkdownThing build and package script

echo ========================================
echo MarkdownThing Build and Package Script
echo ========================================
echo.

set CONFIGURATION=Release
set FRAMEWORK=net8.0-windows
set RUNTIME=win-x64

cd /d "%~dp0.."

echo [1/4] Cleaning previous builds...
dotnet clean MarkdownThing.sln -c %CONFIGURATION% >nul 2>&1

echo [2/4] Building application...
dotnet build MarkdownThing.sln -c %CONFIGURATION%
if errorlevel 1 (
    echo ERROR: Build failed!
    pause
    exit /b 1
)

echo [3/4] Publishing application (self-contained)...
dotnet publish MarkdownThing.csproj -c %CONFIGURATION% -r %RUNTIME% --self-contained true -p:PublishSingleFile=false -p:PublishReadyToRun=true
if errorlevel 1 (
    echo ERROR: Publish failed!
    pause
    exit /b 1
)

echo [4/4] Creating installer...
set ISCC_PATH=
if exist "C:\Program Files (x86)\Inno Setup 6\ISCC.exe" (
    set ISCC_PATH=C:\Program Files (x86)\Inno Setup 6\ISCC.exe
) else if exist "C:\Program Files\Inno Setup 6\ISCC.exe" (
    set ISCC_PATH=C:\Program Files\Inno Setup 6\ISCC.exe
)

if "%ISCC_PATH%"=="" (
    echo WARNING: Inno Setup not found. Install Inno Setup 6 from https://jrsoftware.org/isinfo.php
    echo.
    echo Published to: bin\%CONFIGURATION%\%FRAMEWORK%\%RUNTIME%\publish
    echo Compile Setup\MarkdownThing.iss manually to create the installer.
    pause
    exit /b 0
)

cd Setup
"%ISCC_PATH%" MarkdownThing.iss
if errorlevel 1 (
    echo ERROR: Installer creation failed!
    pause
    exit /b 1
)

echo.
echo ========================================
echo Build completed successfully!
echo ========================================
echo.
echo Installer: Setup\Output\MarkdownThing_Setup_1.2.0.exe
echo.
pause
