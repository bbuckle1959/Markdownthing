; MarkdownThing installer (Inno Setup 6)
;
; Build the publish folder first:
;   Setup\Build-Installer.bat
; or manually:
;   dotnet publish ..\MarkdownThing.csproj -c Release -r win-x64 --self-contained true
; Then compile this script (ISCC.exe MarkdownThing.iss).

#define MyAppName "MarkdownThing"
#define MyAppVersion "1.2.0"
#define MyAppPublisher "Barry Buckle"
#define MyAppURL "https://github.com/bbuckle1959/MarkdownThing"
#define MyAppExeName "MarkdownThing.exe"
#define PublishDir "..\bin\Release\net8.0-windows\win-x64\publish"

[Setup]
; Same AppId as the original MD Convert installer — in-place upgrades keep working.
AppId={{A1B2C3D4-E5F6-7890-ABCD-EF1234567890}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
AppVerName={#MyAppName} {#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}/issues
AppUpdatesURL={#MyAppURL}/releases
AppCopyright=Copyright (C) 2024-2026 {#MyAppPublisher}
DefaultDirName={autopf}\{#MyAppName}
DefaultGroupName={#MyAppName}
DisableProgramGroupPage=yes
OutputDir=Output
OutputBaseFilename=MarkdownThing_Setup_{#MyAppVersion}
Compression=lzma2
SolidCompression=yes
WizardStyle=modern
PrivilegesRequired=lowest
ArchitecturesAllowed=x64
ArchitecturesInstallIn64BitMode=x64
MinVersion=10.0.17763
SetupIconFile=..\milcot.ico
UninstallDisplayIcon={app}\milcot.ico
LicenseFile=..\LICENSE
VersionInfoVersion={#MyAppVersion}.0
VersionInfoCompany={#MyAppPublisher}
VersionInfoDescription={#MyAppName} Setup
VersionInfoProductName={#MyAppName}
VersionInfoProductVersion={#MyAppVersion}

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked
Name: "associatemd"; Description: "Open .md files with MarkdownThing (recommended)"; GroupDescription: "File associations:"; Flags: unchecked

[Files]
Source: "{#PublishDir}\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs

[Icons]
Name: "{group}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; IconFilename: "{app}\milcot.ico"
Name: "{group}\{cm:UninstallProgram,{#MyAppName}}"; Filename: "{uninstallexe}"
Name: "{autodesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; IconFilename: "{app}\milcot.ico"; Tasks: desktopicon

[Registry]
; Per-user .md association (no administrator rights required).
Root: HKCU; Subkey: "Software\Classes\.md"; ValueType: string; ValueName: ""; ValueData: "MarkdownThing.md"; Flags: uninsdeletevalue; Tasks: associatemd
Root: HKCU; Subkey: "Software\Classes\MarkdownThing.md"; ValueType: string; ValueName: ""; ValueData: "Markdown Document"; Flags: uninsdeletekey; Tasks: associatemd
Root: HKCU; Subkey: "Software\Classes\MarkdownThing.md\DefaultIcon"; ValueType: string; ValueName: ""; ValueData: "{app}\milcot.ico"; Tasks: associatemd
Root: HKCU; Subkey: "Software\Classes\MarkdownThing.md\shell\open\command"; ValueType: string; ValueName: ""; ValueData: """{app}\{#MyAppExeName}"" ""%1"""; Tasks: associatemd

[Run]
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#MyAppName}}"; Flags: nowait postinstall skipifsilent
