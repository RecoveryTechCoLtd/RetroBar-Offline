#!/usr/bin/env python3
"""Fail-closed checks for forbidden network functionality and bundled theme integrity.

This is a SOURCE-level audit, not proof about native DLLs or NuGet binaries.
Run: python security/audit_offline.py
"""
import hashlib
import re
import sys
from pathlib import Path
import xml.etree.ElementTree as ET

ROOT = Path(__file__).resolve().parents[1]
APP = ROOT / 'RetroBar'
FORBIDDEN = [
    r'\bHttpClient\b', r'\bWebClient\b', r'\bWebRequest\b',
    r'\bHttpWebRequest\b', r'\bHttpListener\b', r'\bWebSocket\b',
    r'\bTcpClient\b', r'\bUdpClient\b', r'\bSocket\s*\(',
    r'\bSystem\.Net\b', r'\bDns\.', r'\bDownloadFile\b',
    r'\bDownloadData\b', r'\bDownloadString\b',
    r'\bCheckForUpdates\b', r'\bUpdater\b',
    r'\bProcess\s*\.\s*Start\s*\(',
    r'\bShellHelper\s*\.\s*ExecuteProcess\s*\(',
    r'\bProcessStartInfo\b',
    r'\bDllImport\s*\(\s*"(?:wininet|winhttp|ws2_32|urlmon)',
    r'\bAssembly\s*\.\s*Load(?:File|From)?\s*\(',
    r'\bXamlReader\b',
    r'(?i)(?<![a-z])(?:https?|ftp|ws|wss)://',
]
# Comment-only citations to upstream issues are not executable.

def code_without_comments(text):
    # Conservative for this source tree; no raw/verbatim block comments here
    text = re.sub(r'/\*.*?\*/', '', text, flags=re.DOTALL)
    text = re.sub(r'^\s*//.*$', '', text, flags=re.MULTILINE)
    # Keep inline // that might be in strings; accept false positives in that case.
    return text

def check_tree():
    errors = []
    cs = list(APP.rglob('*.cs'))
    xamls = list(APP.rglob('*.xaml'))
    for path in cs:
        content = code_without_comments(path.read_text(encoding='utf-8-sig'))
        for regex in FORBIDDEN:
            match = re.search(regex, content, flags=re.IGNORECASE)
            if match:
                errors.append(f'{path.relative_to(ROOT)}: unexpected code: {match.group(0)}')

    for path in xamls:
        try:
            tree = ET.parse(path)
        except ET.ParseError as ex:
            errors.append(f'{path.relative_to(ROOT)}: invalid XML: {ex}')
            continue
        for elem in tree.iter():
            for attr, value in elem.attrib.items():
                if any(k in attr for k in ('Source', 'Uri', 'NavigateUri')) and re.search(r'\b(?:https?|ftp|wss?)://', value, re.I):
                    errors.append(f'{path.relative_to(ROOT)}: network-bound XAML: {value}')
    trusted = APP / 'Utilities' / 'TrustedDictionaries.cs'
    text = trusted.read_text(encoding='utf-8')
    pairs = dict(re.findall(r'\["((?:Themes|Languages)/[^"\r\n]+\.xaml)"\]\s*=\s*"([a-f0-9]{64})"', text))
    actual = []
    for name, h in pairs.items():
        file = APP / name
        if not file.is_file():
            errors.append(f'Bundled dictionary is missing: {name}')
            continue
        if hashlib.sha256(file.read_bytes()).hexdigest() != h:
            errors.append(f'Bundled dictionary has unexpected hash: {name}')
        actual.append(name)
    paths = {str(p.relative_to(APP)).replace('\\','/') for folder in ('Themes', 'Languages') for p in (APP / folder).glob('*.xaml')}
    if paths != set(pairs):
        errors.append(f'Allowlist mismatch; missing={sorted(paths-set(pairs))}, extra={sorted(set(pairs)-paths)}')

    if (APP/'Utilities'/'Updater.cs').exists():
        errors.append('Updater.cs must not exist')
    if (ROOT/'installer.iss').exists():
        errors.append('Networked installer must not exist')
    if (ROOT/'.github'/'workflows').exists():
        errors.append('Inherited build/deployment workflows are forbidden')
    if not (ROOT/'LICENSE').is_file():
        errors.append('Original Apache 2.0 LICENSE missing')
    if not (ROOT/'RetroBar.sln').is_file():
        errors.append('Solution file missing')
    return cs, xamls, pairs, errors

if __name__ == '__main__':
    cs, xamls, dictionaries, errors = check_tree()
    print(f'Audited {len(cs)} C# files, {len(xamls)} XAML files, {len(dictionaries)} SHA256-pinned resources.')
    if errors:
        print('FAIL:')
        for err in errors:
            print(' - ' + err)
        sys.exit(1)
    print('PASS: no known network APIs, remote XAML resources or updater entry points in first-party source.')
    print('NOTE: This does not establish zero network traffic; enable application firewall rules and runtime-test on Windows.')
