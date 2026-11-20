#requires -version 5.1
<#
.SYNOPSIS
  Apply or inspect Windows Defender Firewall application-level inbound/outbound blocking.
.EXAMPLE
  .\scripts\Block-Network.ps1 -Action Install
#>
[CmdletBinding()]
param(
    [ValidateSet('Install', 'Status', 'Remove')]
    [string]$Action = 'Install',
    [string]$ExecutablePath = ''
)

$ErrorActionPreference = 'Stop'
$ruleIds = @(
    'RecoveryTechCoLtd.RetroBarOffline.Outbound',
    'RecoveryTechCoLtd.RetroBarOffline.Inbound'
)

if ($Action -ne 'Status') {
    $identity = [Security.Principal.WindowsIdentity]::GetCurrent()
    $principal = New-Object Security.Principal.WindowsPrincipal($identity)
    if (-not $principal.IsInRole([Security.Principal.WindowsBuiltInRole]::Administrator)) {
        throw 'Administrator PowerShell required to add/remove firewall rules.'
    }
}

if ($Action -eq 'Remove') {
    foreach ($ruleId in $ruleIds) {
        Get-NetFirewallRule -Name $ruleId -ErrorAction SilentlyContinue |
            Remove-NetFirewallRule -ErrorAction Stop
    }
    Write-Host 'Removed only RetroBarOffline firewall rules.'
    return
}

if ($Action -eq 'Install') {
    if (-not $ExecutablePath) {
        $ExecutablePath = Join-Path $PSScriptRoot '..\out\win-x64\RetroBarOffline.exe'
    }
    if (-not (Test-Path -LiteralPath $ExecutablePath -PathType Leaf)) {
        throw "Cannot find the published EXE: $ExecutablePath"
    }
    $ExecutablePath = (Resolve-Path -LiteralPath $ExecutablePath).Path
    # Replace only our own rules; this also updates the path after moving the EXE.
    foreach ($ruleId in $ruleIds) {
        Get-NetFirewallRule -Name $ruleId -ErrorAction SilentlyContinue |
            Remove-NetFirewallRule -ErrorAction Stop
    }
    New-NetFirewallRule -Name $ruleIds[0] `
        -DisplayName 'RetroBarOffline BLOCK outbound' `
        -Group 'RecoveryTechCoLtd RetroBarOffline' `
        -Program $ExecutablePath -Direction Outbound `
        -Action Block -Enabled True -Profile Any -PolicyStore PersistentStore | Out-Null
    New-NetFirewallRule -Name $ruleIds[1] `
        -DisplayName 'RetroBarOffline BLOCK inbound' `
        -Group 'RecoveryTechCoLtd RetroBarOffline' `
        -Program $ExecutablePath -Direction Inbound `
        -Action Block -Enabled True -Profile Any -PolicyStore PersistentStore | Out-Null
    Write-Host "Firewall now blocks inbound and outbound traffic for: $ExecutablePath" -ForegroundColor Green
}

foreach ($ruleId in $ruleIds) {
    Get-NetFirewallRule -Name $ruleId -ErrorAction SilentlyContinue |
        ForEach-Object {
            $rule = $_
            $app = $rule | Get-NetFirewallApplicationFilter
            [pscustomobject]@{
                Name = $rule.Name
                Direction = $rule.Direction
                Action = $rule.Action
                Enabled = $rule.Enabled
                Profiles = $rule.Profile
                Program = $app.Program
            }
        } | Format-List
}
