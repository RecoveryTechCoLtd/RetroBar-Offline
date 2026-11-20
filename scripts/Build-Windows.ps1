#requires -version 5.1
<#
.SYNOPSIS
  Produce a Windows 11 self-contained Release folder without an installer.
.DESCRIPTION
  First build: dotnet restore contacts nuget.org for official dependencies.
  Future builds: use -NoRestore after packages and RID-specific assets are cached.
  No publish step executes the resulting application or downloads runtime at app launch.
#>
[CmdletBinding()]
param(
    [ValidateSet('win-x64', 'win-x86', 'win-arm64')]
    [string]$Runtime = 'win-x64',
    [switch]$NoRestore
)

$ErrorActionPreference = 'Stop'
$env:DOTNET_CLI_TELEMETRY_OPTOUT = '1'
$env:DOTNET_NOLOGO = '1'
$projectRoot = (Resolve-Path (Join-Path $PSScriptRoot '..')).Path
$project = Join-Path $projectRoot 'RetroBar\RetroBar.csproj'
$publishDir = Join-Path $projectRoot "out\$Runtime"

if (-not (Get-Command dotnet -ErrorAction SilentlyContinue)) {
    throw 'Missing .NET 10 SDK. Install it from Microsoft before building.'
}
$installedSdk = (& dotnet --list-sdks)
if ($LASTEXITCODE -ne 0 -or -not ($installedSdk -match '(?m)^10\.')) {
    throw 'Please install the .NET 10 SDK (not just the Desktop Runtime).'
}

# Use the bundled PowerShell audit; Python is NOT required to build.
& (Join-Path $PSScriptRoot 'Audit-Offline.ps1')

if (-not $NoRestore) {
    Write-Host 'Build-time NuGet restore: network may be used here, never at runtime.'
    & dotnet restore $project --runtime $Runtime --configfile (Join-Path $projectRoot 'NuGet.Config')
    if ($LASTEXITCODE -ne 0) { throw 'NuGet restore failed.' }
}

& dotnet publish $project `
    --configuration Release `
    --framework net10.0-windows `
    --runtime $Runtime `
    --self-contained true `
    --no-restore `
    -p:PublishSingleFile=false `
    -p:PublishTrimmed=false `
    --output $publishDir
if ($LASTEXITCODE -ne 0) { throw 'Publish failed.' }

$exe = Join-Path $publishDir 'RetroBarOffline.exe'
if (-not (Test-Path -LiteralPath $exe -PathType Leaf)) { throw "EXE missing: $exe" }

Write-Host "Output: $publishDir" -ForegroundColor Green
Write-Host 'Run scripts\Block-Network.ps1 -Action Install -ExecutablePath "<path-to-EXE>" as administrator BEFORE launch.'
