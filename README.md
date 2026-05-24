# MarkdownThing

Open a `.md` file on Windows, edit it with a live preview, and export to HTML, PDF, PNG, Word or Text.

![.NET 8](https://img.shields.io/badge/.NET-8.0-512BD4?style=flat&logo=dotnet)
![Windows](https://img.shields.io/badge/Platform-Windows-0078D6?style=flat&logo=windows)
![License](https://img.shields.io/badge/License-MIT-green.svg)

## Who it's for

- You write notes or docs in Markdown and need to hand someone a **PDF**, **PNG**, **Word**, **HTML** or **Text** file.
- You double-click `README.md` and want a proper viewer, not Notepad.
- You have a folder of `.md` files and want them converted in one go (GUI batch or CLI).

## Download

**[Releases](https://github.com/bbuckle1959/MarkdownThing/releases)** — pick `MarkdownThing_Setup_x.x.x.exe` (installer) or `MarkdownThing_Portable_x.x.x.zip` (unzip and run, no install).

| Option | Size (approx.) | Notes |
|--------|----------------|-------|
| Installer (self-contained) | ~150 MB | Includes .NET runtime; optional `.md` association |
| Portable zip | Same payload | Handy for USB or locked-down PCs |
| Framework-dependent build | ~10 MB | Build yourself; needs [.NET 8 Desktop Runtime](https://dotnet.microsoft.com/download/dotnet/8.0) |

**Also needs:** [WebView2](https://developer.microsoft.com/microsoft-edge/webview2/) (already on most Windows 11 machines and recent Windows 10).

**First PDF or PNG export:** the app downloads Chromium once (~150 MB) via PuppeteerSharp so output matches the preview. Later exports skip that step.

**SmartScreen:** the installer is not code-signed yet. If Windows warns you, that's expected for unsigned hobby releases — build from source if you prefer.

Upgrading from **MD Convert**: settings in `%AppData%\MDConvert\` are moved to `%AppData%\MarkdownThing\` on first launch. The installer keeps the same upgrade ID, so an in-place install over MD Convert should work.

### winget

A manifest lives in [`manifests/`](manifests/) for submission to [winget-pkgs](https://github.com/microsoft/winget-pkgs). After a release is published:

```text
winget install bbuckle1959.MarkdownThing
```

(Works only after the package is accepted upstream and the installer URL + hash in the manifest match the release.)

## Features

- Split **edit + preview** (preview updates as you type)
- **Preview themes:** Default, GitHub-style, Print-friendly; optional dark preview and dark editor
- **Export:** PDF, PNG, HTML, Word (.docx), plain text — PDF and PNG use the same HTML as the preview
- **PDF page setup:** paper size and margins (A4, Letter, etc.)
- **Batch folder export** from the File menu
- **Command-line** conversion for scripts
- Recent files, drag-and-drop, optional `.md` file association (installer)
- **Help → Check for updates** (GitHub releases)

## Usage

### GUI

| Action | Shortcut |
|--------|----------|
| New | Ctrl+N |
| Open | Ctrl+O |
| Save / Save As | Ctrl+S / Ctrl+Shift+S |
| Toggle edit mode | Ctrl+E |
| Find | Ctrl+F |
| Go to line | Ctrl+G |
| Insert date/time | Ctrl+Shift+D |
| Copy HTML preview | Ctrl+Shift+C |
| Reload from disk | F5 |
| Preview zoom in / out / reset | Ctrl++ / Ctrl+- / Ctrl+0 |
| Heading 1–6 (in editor) | Ctrl+1 … Ctrl+6 |
| Export PDF / PNG / HTML / Word / Text | Ctrl+P / Ctrl+Shift+P / Ctrl+H / Ctrl+W / Ctrl+T |

**View** menu: preview theme, dark preview, dark editor, word wrap, preview zoom.

**File → Reload from disk** (F5) reloads the saved file. **Open containing folder** appears when the document is saved to disk.

**Edit → Find** (Ctrl+F), **Go to line** (Ctrl+G), **Copy HTML preview**, and **Insert date/time**. The image toolbar button opens a file picker when nothing is selected.

**File → Export → PDF page setup** before printing to PDF.

**File → Export → Batch folder** — all `.md` files under a directory, same format.

### Command line

```text
MarkdownThing.exe notes.md --pdf report.pdf
MarkdownThing.exe notes.md --png preview.png --html out.html --docx out.docx
MarkdownThing.exe --batch C:\Docs\Notes --format png
MarkdownThing.exe notes.md --pdf out.pdf --theme GitHub --dark --paper Letter
```

Run `MarkdownThing.exe --help` for the full list. Opening a file by itself (e.g. double-click) still launches the GUI.

## How it compares

| Tool | MarkdownThing | Typical alternative |
|------|---------------|---------------------|
| Pandoc | GUI + preview; PDF matches on-screen layout | CLI; steeper learning curve |
| VS Code | Focused on `.md` only; exports without an editor install | Needs extensions/setup for Word/PDF |
| Obsidian | Lighter install; offline export to office formats | Vault model; heavier than “open one file” |
| Typora | MIT license; similar “see and export” idea | Paid WYSIWYG editor (different workflow) |

Word export handles headings, lists, emphasis, quotes, code blocks, and links. Tables and other advanced constructs are preview/HTML/PDF only for now — not a full layout engine for complex documents.

## Build from source

```bash
git clone https://github.com/bbuckle1959/MarkdownThing.git
cd MarkdownThing
dotnet build MarkdownThing.sln -c Release
dotnet run --project MarkdownThing.csproj -c Release
```

**Installer:** `Setup\Build-Installer.bat` (needs [Inno Setup 6](https://jrsoftware.org/isinfo.php)).

**Portable zip:** `Setup\Build-Portable.bat`.

See [Setup/README.md](Setup/README.md) for publish options (self-contained vs framework-dependent).

## Markdown support

Rendering uses [Markdig](https://github.com/xoofx/markdig) with advanced extensions: tables, task lists, fenced code, footnotes, etc.

## Third-party licenses

| Library | License |
|---------|---------|
| [Markdig](https://github.com/xoofx/markdig) | BSD-2-Clause |
| [Microsoft.Web.WebView2](https://www.nuget.org/packages/Microsoft.Web.WebView2) | BSD-3-Clause |
| [PuppeteerSharp](https://github.com/hardkoded/puppeteer-sharp) | MIT |
| [DocumentFormat.OpenXml](https://github.com/dotnet/Open-XML-SDK) | MIT |

See [THIRD-PARTY-NOTICES.txt](THIRD-PARTY-NOTICES.txt) for full license texts.

## Project structure

| Area | Location |
|------|----------|
| WinForms UI | `Form1*.cs`, dialogs |
| Conversion / export | `MDConvertLib/` |
| Settings | `%AppData%\MarkdownThing\` (migrated from legacy `%AppData%\MDConvert\` on first launch) |
| Installer | `Setup/` |

## Contributing

See [CONTRIBUTING.md](CONTRIBUTING.md). Pull requests are welcome — fork, branch, build, PR.

Security reports: see [SECURITY.md](SECURITY.md).

## License

MIT — see [LICENSE](LICENSE).
