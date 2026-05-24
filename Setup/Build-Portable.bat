@echo off
set CONFIGURATION=Release
set RUNTIME=win-x64
cd /d "%~dp0.."

echo Publishing portable build...
dotnet publish MarkdownThing.csproj -c %CONFIGURATION% -r %RUNTIME% --self-contained true -p:PublishSingleFile=false
if errorlevel 1 exit /b 1

set OUT=Setup\Output\MarkdownThing_Portable
if exist "%OUT%" rmdir /s /q "%OUT%"
mkdir "%OUT%"
xcopy /e /i /y "bin\%CONFIGURATION%\net8.0-windows\%RUNTIME%\publish\*" "%OUT%\"

powershell -NoProfile -Command "Compress-Archive -Path '%OUT%\*' -DestinationPath 'Setup\Output\MarkdownThing_Portable_1.2.0.zip' -Force"
echo Created Setup\Output\MarkdownThing_Portable_1.2.0.zip
