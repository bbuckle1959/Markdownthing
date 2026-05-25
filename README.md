# MarkdownThing

Free Windows app for **viewing and editing** Markdown (`.md`) with a live preview. Export to **PDF**, **PNG**, **HTML**, **Word**, or plain **text**.

![Windows](https://img.shields.io/badge/Platform-Windows-0078D6?style=flat&logo=windows)
![License](https://img.shields.io/badge/License-MIT-green.svg)

## Download

From **[Releases](https://github.com/bbuckle1959/MarkdownThing/releases)**, download:

| File | Notes |
|------|--------|
| `MarkdownThing_Setup_1.2.0.zip` | Extract and run setup (~55 MB). Optional `.md` file association. |
| `MarkdownThing_Portable_1.2.0.zip` | Unzip and run `MarkdownThing.exe` — no install (~77 MB). |

**Requirements:** Windows 10 or 11 (64-bit). [WebView2](https://developer.microsoft.com/microsoft-edge/webview2/) for the preview (usually already installed). The first PDF or PNG export downloads a one-time component (~150 MB) so output matches the preview.

**SmartScreen:** The installer is not code-signed; Windows may warn about an unknown publisher. Use *More info* → *Run anyway*, or the portable zip.

## Features

- Live **edit + preview**; themes and optional dark mode
- **Export** PDF, PNG, HTML, Word (`.docx`), or text (PDF/PNG match the preview)
- PDF page setup, **batch folder** export, drag-and-drop, recent files
- Tables, task lists, code blocks, footnotes, and other common Markdown (via [Markdig](https://github.com/xoofx/markdig))

Word export covers headings, lists, emphasis, quotes, code, and links; some complex elements (e.g. tables) may not transfer fully to Word.

## Shortcuts

| Action | Shortcut |
|--------|----------|
| Open / Save / Save As | Ctrl+O / Ctrl+S / Ctrl+Shift+S |
| Edit mode | Ctrl+E |
| Find / Go to line | Ctrl+F / Ctrl+G |
| Reload from disk | F5 |
| Export PDF / PNG / HTML / Word / Text | Ctrl+P / Ctrl+Shift+P / Ctrl+H / Ctrl+W / Ctrl+T |
| Preview zoom | Ctrl++ / Ctrl+- / Ctrl+0 |

Run `MarkdownThing.exe --help` for command-line conversion (`--pdf`, `--png`, `--batch`, etc.).

## Building From Source
This project is built using .NET and WinForms. To clone and build the project locally, please see the step-by-step instructions in the [Setup/README.md](Setup/README.md) file.

## Acknowledgments

Built with [Markdig](https://github.com/xoofx/markdig), [WebView2](https://developer.microsoft.com/microsoft-edge/webview2/), [PuppeteerSharp](https://github.com/hardkoded/puppeteer-sharp), and [Open XML SDK](https://github.com/dotnet/Open-XML-SDK). Without these projects, this app would not have been possible.

License details: [LICENSE](LICENSE) · [THIRD-PARTY-NOTICES.txt](THIRD-PARTY-NOTICES.txt). To build from source: [Setup/README.md](Setup/README.md).
