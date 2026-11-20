# RetroBar Offline — security scope and limitations

## Changes

- Deleted original HTTP updater, settings toggle and release URL handler.
- Deleted the Inno Setup installer (which checked releases and downloaded .NET runtime).
- Removed explicit in-application URL launching; the date-format help is now a local MessageBox.
- Removed autostart registry modification UI.
- Disallowed custom XAML themes/languages; shipped XAML is checked against a fixed SHA-256 allowlist at load time.
- Removed upstream CI and dependency auto-update workflows.
- Added opt-in Windows Defender Firewall in/out rules for exact `RetroBarOffline.exe` path.

## Known boundaries

1. This is FIRST-PARTY source-level review, not full audit of the ManagedShell and Gong binaries or native Windows APIs.
2. `dotnet restore` downloads packages at build time. Packaged app uses self-contained publish and has no first-party update/download code.
3. Windows itself and third-party apps launched through taskbar can use network. App-specific firewall is not system-wide egress isolation.
4. Firewall rules must be installed as administrator before launching, and reapplied if the executable moves.
5. Windows firewall policies can be disabled/overridden by administrator/system policy; this project cannot enforce zero network under all conditions.
6. User-provided XAML is intentionally unsupported; modifying protected theme files causes a SHA-256 mismatch and prevents loading.
7. This environment cannot run Microsoft Windows / .NET SDK; real Windows compilation and packet capture are not yet performed.

## Audit commands

```
python security/audit_offline.py
powershell -File scripts/Block-Network.ps1 -Action Status
```

## Upstream obligations

Original RetroBar license: Apache 2.0. Preserve LICENSE and upstream notices in any redistribution.
