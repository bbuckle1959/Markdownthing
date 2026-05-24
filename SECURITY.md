# Security Policy

## Supported versions

Security fixes are applied to the latest release on the default branch (`main`).

| Version | Supported |
| ------- | --------- |
| Latest  | Yes       |
| Older   | Best effort |

## Reporting a vulnerability

Please **do not** open a public GitHub issue for security-sensitive reports.

Instead, use [GitHub private vulnerability reporting](https://github.com/bbuckle1959/MarkdownThing/security/advisories/new) if available, or open a minimal issue asking for a private contact channel.

Include:

- Affected version or commit
- Steps to reproduce
- Impact (data loss, code execution, etc.)
- Any suggested fix, if you have one

## Scope notes

MarkdownThing renders user-supplied Markdown locally. PDF export uses headless Chromium with JavaScript disabled. Treat untrusted Markdown like untrusted HTML when opening files from unknown sources.
