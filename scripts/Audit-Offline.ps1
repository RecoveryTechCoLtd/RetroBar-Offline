#requires -version 5.1
<# Static first-party source and bundled XAML integrity audit. #>
[CmdletBinding()]
param()

$ErrorActionPreference = 'Stop'
$root = (Resolve-Path (Join-Path $PSScriptRoot '..')).Path
$app = Join-Path $root 'RetroBar'
$errorsFound = New-Object System.Collections.Generic.List[string]
$badPatterns = @(
    '\bHttpClient\b', '\bWebClient\b', '\bWebRequest\b',
    '\bHttpWebRequest\b', '\bHttpListener\b', '\bWebSocket\b',
    '\bTcpClient\b', '\bUdpClient\b', '\bSocket\s*\(',
    '\bSystem\.Net\b', '\bDns\.', '\bDownloadFile\b',
    '\bDownloadData\b', '\bDownloadString\b',
    '\bCheckForUpdates\b', '\bUpdater\b',
    '\bProcess\s*\.\s*Start\s*\(',
    '\bShellHelper\s*\.\s*ExecuteProcess\s*\(',
    '\bProcessStartInfo\b',
    '\bDllImport\s*\(\s*"(?:wininet|winhttp|ws2_32|urlmon)',
    '\bAssembly\s*\.\s*Load(?:File|From)?\s*\(',
    '\bXamlReader\b',
    '(?<![a-z])(?:https?|ftp|ws|wss)://'
)
$csFiles = @(Get-ChildItem -LiteralPath $app -Recurse -File -Filter '*.cs')
foreach ($file in $csFiles) {
    $s = [System.IO.File]::ReadAllText($file.FullName)
    $s = [regex]::Replace($s, '/\*.*?\*/', '', [System.Text.RegularExpressions.RegexOptions]::Singleline)
    $s = [regex]::Replace($s, '^\s*//.*$', '', [System.Text.RegularExpressions.RegexOptions]::Multiline)
    foreach ($pattern in $badPatterns) {
        if ([regex]::IsMatch($s, $pattern, [System.Text.RegularExpressions.RegexOptions]::IgnoreCase)) {
            $errorsFound.Add("Forbidden C# pattern: $($file.FullName) -> $pattern")
        }
    }
}

$xamlFiles = @(Get-ChildItem -LiteralPath $app -Recurse -File -Filter '*.xaml')
foreach ($file in $xamlFiles) {
    try {
        [xml]$xml = [System.IO.File]::ReadAllText($file.FullName)
        foreach ($attr in $xml.SelectNodes('//@*')) {
            if ($attr.LocalName -match '(Source|Uri|NavigateUri)' -and
                $attr.Value -match '(?i)(?:https?|ftp|wss?)://') {
                $errorsFound.Add("Remote XAML resource: $($file.FullName): $($attr.Value)")
            }
        }
    } catch {
        $errorsFound.Add("Unparseable XAML: $($file.FullName): $_")
    }
}

$catalogFile = Join-Path $app 'Utilities\TrustedDictionaries.cs'
$catalog = [System.IO.File]::ReadAllText($catalogFile)
$matches = [regex]::Matches($catalog, '\["((?:Themes|Languages)/[^"\r\n]+\.xaml)"\]\s*=\s*"([a-f0-9]{64})"')
$allow = @{}
foreach ($match in $matches) {
    $key = $match.Groups[1].Value
    $expected = $match.Groups[2].Value
    $allow[$key] = $true
    $file = Join-Path $app ($key.Replace('/', '\'))
    if (-not (Test-Path -LiteralPath $file -PathType Leaf)) {
        $errorsFound.Add("Missing trusted XAML: $key")
    } elseif ((Get-FileHash -LiteralPath $file -Algorithm SHA256).Hash.ToLowerInvariant() -ne $expected) {
        $errorsFound.Add("SHA256 mismatch: $key")
    }
}
foreach ($folder in @('Themes', 'Languages')) {
    foreach ($file in (Get-ChildItem -LiteralPath (Join-Path $app $folder) -Filter '*.xaml' -File)) {
        $key = "$folder/$($file.Name)"
        if (-not $allow.ContainsKey($key)) {
            $errorsFound.Add("Unknown XAML in source: $key")
        }
    }
}
if (-not (Test-Path -LiteralPath (Join-Path $root 'LICENSE'))) {
    $errorsFound.Add('Original LICENSE is missing')
}
if (Test-Path -LiteralPath (Join-Path $app 'Utilities\Updater.cs')) {
    $errorsFound.Add('Updater.cs must not be included')
}
if (Test-Path -LiteralPath (Join-Path $root 'installer.iss')) {
    $errorsFound.Add('Networked installer is forbidden')
}
if (Test-Path -LiteralPath (Join-Path $root '.github\workflows')) {
    $errorsFound.Add('Inherited workflows are forbidden')
}
if ($errorsFound.Count -gt 0) {
    foreach ($failure in $errorsFound) { Write-Error $failure }
    throw "Offline security audit failed ($($errorsFound.Count) findings)."
}
Write-Host "PASS: $($csFiles.Count) C# files, $($xamlFiles.Count) XAML files and $($allow.Count) SHA256-pinned theme/language resources." -ForegroundColor Green
Write-Host 'Source scan only: for stronger isolation, install firewall rules and check runtime traffic.'
