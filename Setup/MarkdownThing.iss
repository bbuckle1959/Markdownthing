#define MyAppName "MarkdownThing"
#define MyAppVersion "1.2.0"
#define MyAppPublisher "Barry Buckle"
#define MyAppURL "https://github.com/bbuckle1959/MarkdownThing"
#define MyAppExeName "MarkdownThing.exe"

[Setup]
AppId={{A1B2C3D4-E5F6-7890-ABCD-EF1234567890}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}/issues
AppUpdatesURL={#MyAppURL}/releases
DefaultDirName={autopf}\{#MyAppName}
DefaultGroupName={#MyAppName}
OutputDir=Output
OutputBaseFilename=MarkdownThing_Setup_{#MyAppVersion}
Compression=lzma2
SolidCompression=yes
ArchitecturesInstallIn64BitMode=x64
PrivilegesRequired=lowest
WizardStyle=modern
SetupIconFile=..\milcot.ico
UninstallDisplayIcon={app}\milcot.ico

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"
Name: "associatemd"; Description: "Associate .md files with MarkdownThing"; GroupDescription: "File associations:"

[Files]
Source: "..\bin\Release\net8.0-windows\win-x64\publish\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs

[Icons]
Name: "{group}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; IconFilename: "{app}\milcot.ico"
Name: "{group}\{cm:UninstallProgram,{#MyAppName}}"; Filename: "{uninstallexe}"
Name: "{autodesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; IconFilename: "{app}\milcot.ico"; Tasks: desktopicon

[Registry]
Root: HKCU; Subkey: "Software\Classes\.md"; ValueType: string; ValueName: ""; ValueData: "MarkdownThing.md"; Flags: uninsdeletevalue; Tasks: associatemd
Root: HKCU; Subkey: "Software\Classes\MarkdownThing.md"; ValueType: string; ValueName: ""; ValueData: "Markdown Document"; Flags: uninsdeletekey; Tasks: associatemd
Root: HKCU; Subkey: "Software\Classes\MarkdownThing.md\DefaultIcon"; ValueType: string; ValueName: ""; ValueData: "{app}\milcot.ico"; Tasks: associatemd
Root: HKCU; Subkey: "Software\Classes\MarkdownThing.md\shell\open\command"; ValueType: string; ValueName: ""; ValueData: """{app}\{#MyAppExeName}"" ""%1"""; Tasks: associatemd

[Run]
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#MyAppName}}"; Flags: nowait postinstall skipifsilent
