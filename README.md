# MarkdownThing

> **Archived — no longer maintained.** This project is archived. You can still download the app from [Releases](https://github.com/bbuckle1959/MarkdownThing/releases), but there will be no new versions, fixes, or support.

**MarkdownThing** is a free Windows app for opening Markdown files (`.md`). View your document with a live preview, edit when you need to, and save or export as **PDF**, **PNG**, **HTML**, **Word**, or plain **text**.

![Windows](https://img.shields.io/badge/Platform-Windows-0078D6?style=flat&logo=windows)
![License](https://img.shields.io/badge/License-MIT-green.svg)

## Download and install

1. Open **[Releases](https://github.com/bbuckle1959/MarkdownThing/releases)** on GitHub.
2. Download the latest **`MarkdownThing_Setup_….exe`** (installer) **or** **`MarkdownThing_Portable_….zip`** (no installer — unzip and run `MarkdownThing.exe`).

| Download | What you get |
|----------|----------------|
| **Installer** (~150 MB) | Installs the app and can register `.md` files to open with MarkdownThing. Includes everything needed to run. |
| **Portable zip** (~150 MB) | Same app without installing — useful on a USB stick or if you cannot install software. |

**Windows:** Windows 10 or 11 (64-bit).

**WebView2:** The preview pane needs [Microsoft WebView2](https://developer.microsoft.com/microsoft-edge/webview2/). Most PCs already have it; if the app asks you to install it, use the link in the message.

**First PDF or PNG export:** The first time you export to PDF or PNG, the app downloads a one-time browser component (~150 MB) so the file matches what you see in the preview. Later exports do not download again.

**Windows SmartScreen:** The installer is not code-signed. Windows may show an “unknown publisher” warning — choose *More info* → *Run anyway* if you trust this download, or use the portable zip.

**Upgrading from MD Convert:** If you used the older *MD Convert* app, your settings are moved automatically from the old folder to `%AppData%\MarkdownThing\` the first time you run MarkdownThing. Installing over MD Convert should keep your setup.

### Install with winget (optional)

If the package is available on your PC:

```text
winget install bbuckle1959.MarkdownThing
```

## What you can do

- Open `.md` files by double-clicking (after using the installer’s file association) or via **File → Open**
- **Edit** with a live **preview** that updates as you type
- Choose **preview themes** (Default, GitHub-style, Print-friendly) and optional dark preview or dark editor
- **Export** to PDF, PNG, HTML, Word (`.docx`), or plain text — PDF and PNG match the preview
- Set **PDF page size and margins** (A4, Letter, etc.) before exporting
- **Export a whole folder** of Markdown files at once (File menu)
- **Drag and drop** files onto the window; **recent files** list; **Check for updates** under Help (while releases exist)

**Word export** supports headings, lists, bold/italic, quotes, code blocks, and links. Complex layouts (e.g. some tables) appear in the preview and in PDF/HTML/PNG but may not transfer fully into Word.

## Quick start

1. Run **MarkdownThing** and open a `.md` file (**Ctrl+O** or drag onto the window).
2. Press **Ctrl+E** to turn **edit mode** on or off (split view: editor + preview).
3. Use **File → Export** (or the shortcuts below) to create PDF, PNG, HTML, Word, or text files.

Use the **View** menu for preview theme, dark mode, word wrap, and zoom in the preview (**Ctrl++**, **Ctrl+-**, **Ctrl+0**).

## Keyboard shortcuts

| Action | Shortcut |
|--------|----------|
| New | Ctrl+N |
| Open | Ctrl+O |
| Save / Save As | Ctrl+S / Ctrl+Shift+S |
| Edit mode on/off | Ctrl+E |
| Find | Ctrl+F |
| Go to line | Ctrl+G |
| Reload file from disk | F5 |
| Insert date/time | Ctrl+Shift+D |
| Copy preview as HTML | Ctrl+Shift+C |
| Preview zoom in / out / reset | Ctrl++ / Ctrl+- / Ctrl+0 |
| Heading 1–6 (in editor) | Ctrl+1 … Ctrl+6 |
| Export PDF / PNG / HTML / Word / Text | Ctrl+P / Ctrl+Shift+P / Ctrl+H / Ctrl+W / Ctrl+T |

**Tips:** **File → Reload from disk** (F5) discards unsaved edits and reloads the saved file. **File → Export → PDF page setup** sets paper size before PDF export. **File → Export → Batch folder** converts every `.md` file in a folder. The toolbar image button inserts a picture from your PC when nothing is selected in the editor.

## Command line (advanced)

You can convert files without opening the full window:

```text
MarkdownThing.exe notes.md --pdf report.pdf
MarkdownThing.exe notes.md --png slide.png --html page.html --docx report.docx
MarkdownThing.exe --batch C:\Docs\Notes --format pdf
```

Run `MarkdownThing.exe --help` for all options. Double-clicking a `.md` file still opens the normal app.

## How it compares to other tools

| | MarkdownThing | Typical alternative |
|---|---------------|---------------------|
| **Pandoc** | Point-and-click + preview; PDF looks like the screen | Powerful command-line tool; more setup |
| **VS Code** | Just for Markdown; export built in | General code editor; extensions needed for some exports |
| **Obsidian** | Simple “open a file and export” | Notes vault; heavier if you only need one file |
| **Typora** | Free app; edit and export | Paid editor; different workflow |

## Markdown features

The app understands common Markdown, including **tables**, **task lists** (`- [ ]`), **fenced code blocks**, **footnotes**, and more.

## Open-source libraries

MarkdownThing was built with help from these projects — without them, this app would not exist. Thank you to their authors.

| Library | License |
|---------|---------|
| [Markdig](https://github.com/xoofx/markdig) | BSD-2-Clause |
| [Microsoft WebView2](https://developer.microsoft.com/microsoft-edge/webview2/) | BSD-3-Clause |
| [PuppeteerSharp](https://github.com/hardkoded/puppeteer-sharp) | MIT |
| [DocumentFormat.OpenXml](https://github.com/dotnet/Open-XML-SDK) | MIT |

Full license texts: [THIRD-PARTY-NOTICES.txt](THIRD-PARTY-NOTICES.txt).

## License

This project is [MIT licensed](LICENSE). You may use, modify, and share the code under those terms.

## Source code

The full source is in this repository for reference only. The repo is **archived** — pull requests and issues are not accepted. Build notes for developers are in [Setup/README.md](Setup/README.md).
