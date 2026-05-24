# Installer and portable builds

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Inno Setup 6](https://jrsoftware.org/isinfo.php) — only for the `.exe` installer

## Installer (recommended for most users)

```text
cd Setup
Build-Installer.bat
```

Output: `Setup\Output\MarkdownThing_Setup_1.2.0.exe`

The script publishes a **self-contained** `win-x64` app (~150 MB) so users do not need the .NET runtime installed separately.

The Inno `AppId` is unchanged from the original MD Convert installer so upgrades in place still work.

## Portable zip

```text
cd Setup
Build-Portable.bat
```

Output: `Setup\Output\MarkdownThing_Portable_1.2.0.zip` — unzip anywhere and run `MarkdownThing.exe`.

## Smaller framework-dependent build

For developers who already have the desktop runtime:

```text
dotnet publish MarkdownThing.csproj -c Release -r win-x64 --self-contained false
```

Publish output is under `bin\Release\net8.0-windows\win-x64\publish\` (~10 MB plus dependencies).

## Version numbers

Keep these in sync when releasing:

1. `MarkdownThing.csproj` — `<Version>`
2. `Setup\MarkdownThing.iss` — `#define MyAppVersion`
3. `manifests/.../bbuckle1959.MarkdownThing.yaml` — `PackageVersion` and download URL
4. `Setup\Build-Portable.bat` zip filename (optional)

## Inno Setup script

`MarkdownThing.iss` installs the publish folder, optional desktop icon, and optional `.md` file association (per-user, no admin required).

If Inno Setup is missing, `Build-Installer.bat` still publishes the app and tells you where the files are.

## WebView2

The app needs the WebView2 runtime. Windows 11 and current Windows 10 builds usually have it. Otherwise: https://developer.microsoft.com/microsoft-edge/webview2/
