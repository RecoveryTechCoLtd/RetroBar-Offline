# Changes from uploaded RetroBar-master.zip

Source snapshot: user-provided `RetroBar-master.zip` (source archive dated 2026-11-16).

- Removed `RetroBar/Utilities/Updater.cs` entirely.
- Removed references to `Updater` in `App.xaml.cs`, `Taskbar.xaml.cs`, `WindowManager.cs`.
- Removed update context menu and settings toggle.
- Removed online installer `installer.iss`, upstream `.github` workflows and dependabot config, versioning build asset.
- Changed AssemblyName to `RetroBarOffline` and target framework to `net10.0-windows`.
- Isolated mutex and `%LOCALAPPDATA%` settings from official RetroBar.
- Removed automatic startup registration UI and outbound website navigation.
- Restricted XAML theme and language loading to 66 hashed built-in files, and checks the full nested XAML import chain; removed user-supplied XAML auto-discovery and monitoring.
- Included builds for Windows x86 / x64 / ARM64 as .NET self-contained folder publish, local audit, firewall scripts, and docs.
- Preserved themes, languages, graphics, app taskbar functionality and original upstream license.

This is a full derived project (not a patching toolkit) but was not successfully compiler-tested on Windows in the current environment.
