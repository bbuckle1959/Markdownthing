# Installer and portable builds

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Inno Setup 6](https://jrsoftware.org/isinfo.php) — required to build the `.exe` installer

## Installer

From the repository root:

```text
Setup\Build-Installer.bat
```

Or step by step:

```text
dotnet publish MarkdownThing.csproj -c Release -r win-x64 --self-contained true -p:PublishReadyToRun=true
cd Setup
"C:\Program Files (x86)\Inno Setup 6\ISCC.exe" MarkdownThing.iss
```

**Output:** `Setup\Output\MarkdownThing_Setup_1.2.0.exe`

The installer packages a **self-contained** 64-bit Windows build (~150 MB) so end users do not need the .NET runtime installed separately.

`MarkdownThing.iss` installs the published files, shows the MIT license, and offers optional tasks:

- Desktop shortcut
- `.md` file association (per-user registry; no admin rights)

The Inno `AppId` matches the original **MD Convert** installer so an in-place upgrade over MD Convert still works.

## Portable zip

```text
Setup\Build-Portable.bat
```

**Output:** `Setup\Output\MarkdownThing_Portable_1.2.0.zip` — unzip anywhere and run `MarkdownThing.exe`.

## Version numbers

Keep these aligned when releasing:

1. `MarkdownThing.csproj` — `<Version>`
2. `Setup\MarkdownThing.iss` — `#define MyAppVersion`
3. `Setup\Build-Portable.bat` — zip file name
4. `manifests/.../bbuckle1959.MarkdownThing.yaml` — `PackageVersion` and installer URL

## WebView2

The app needs the [WebView2 runtime](https://developer.microsoft.com/microsoft-edge/webview2/). Windows 11 and current Windows 10 builds usually include it.
