# Contributing

Thanks for considering a contribution to MarkdownThing.

## Getting started

```bash
git clone https://github.com/bbuckle1959/MarkdownThing.git
cd MarkdownThing
dotnet build MarkdownThing.sln -c Release
dotnet run --project MarkdownThing.csproj -c Release
```

Requirements: .NET 8 SDK, Windows, WebView2 runtime.

## Code layout

| Path | Purpose |
|------|---------|
| `Form1*.cs` | Main window, menus, scroll sync |
| `MDConvertLib/` | Markdown conversion, export, CLI |
| `AppSettings.cs` / `AppDataPaths.cs` | User settings under `%AppData%\MarkdownThing` |
| `Setup/` | Inno Setup installer scripts |

The conversion library lives in `MDConvertLib/` (folder name only; types use the `MDConvertLib` namespace).

## Pull requests

1. Fork and create a feature branch from the repository default branch (`main`).
2. Keep changes focused — one logical fix or feature per PR.
3. Build locally before opening the PR: `dotnet build MarkdownThing.sln -c Release`.
4. Describe what changed and how you tested it.

## Style

- Match existing naming and formatting in the file you edit.
- Avoid drive-by refactors unrelated to your change.
- Prefer clear code over comments that restate the obvious.

## Issues

Bug reports and feature requests are welcome via [GitHub Issues](https://github.com/bbuckle1959/MarkdownThing/issues). Include Windows version, app version, and steps to reproduce when reporting bugs.
