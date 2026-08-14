[CmdletBinding()]
param()

$ErrorActionPreference = 'Stop'
$projectRoot = Split-Path -Parent $PSScriptRoot

$requiredFiles = @(
    'About/About.xml',
    'About/Preview.png',
    'About/thumb.png',
    'ControlsPlus.csproj',
    'README.md',
    'LICENSE',
    'src/ControlsPlusMod.cs'
)

foreach ($relativePath in $requiredFiles) {
    $path = Join-Path $projectRoot $relativePath
    if (-not (Test-Path -LiteralPath $path -PathType Leaf)) {
        throw "Missing required file: $relativePath"
    }
}

$about = [xml](Get-Content -LiteralPath (Join-Path $projectRoot 'About/About.xml') -Raw -Encoding UTF8)
$project = [xml](Get-Content -LiteralPath (Join-Path $projectRoot 'ControlsPlus.csproj') -Raw -Encoding UTF8)
$source = Get-Content -LiteralPath (Join-Path $projectRoot 'src/ControlsPlusMod.cs') -Raw -Encoding UTF8

$expectedId = 'com.james.controlsplus'
$expectedAssembly = 'ControlsPlus'
$version = [string]$project.Project.PropertyGroup.Version

if ([string]$about.ModMetadata.ModID -ne $expectedId) {
    throw "About.xml ModID does not equal $expectedId."
}

if ([string]$project.Project.PropertyGroup.AssemblyName -ne $expectedAssembly) {
    throw "AssemblyName does not equal $expectedAssembly."
}

if ([string]$about.ModMetadata.Version -ne $version) {
    throw 'About.xml and project versions do not match.'
}

if ($source -notmatch [regex]::Escape("public const string ModId = `"$expectedId`";")) {
    throw 'ControlsPlusMod.ModId does not match About.xml.'
}

if ($source -notmatch [regex]::Escape("public const string Version = `"$version`";")) {
    throw 'ControlsPlusMod.Version does not match the project version.'
}

foreach ($label in 'Toggle Sensor Lenses', 'Toggle Tablet') {
    if ($source -notmatch [regex]::Escape("`"$label`"")) {
        throw "Missing control label: $label"
    }
}

$mojibakeMarkers = @(
    (-join @([char]0x00e2, [char]0x20ac, [char]0x201d)),
    (-join @([char]0x00e2, [char]0x20ac, [char]0x201c)),
    (-join @([char]0x00ef, [char]0x00bb, [char]0x00bf)),
    [string][char]0xfffd
)
$textFiles = Get-ChildItem -LiteralPath $projectRoot -Recurse -File |
    Where-Object {
        $_.FullName -ne $PSCommandPath -and
        $_.FullName -notmatch '[\\/](bin|obj|artifacts)[\\/]' -and
        $_.Extension -in '.cs', '.csproj', '.md', '.ps1', '.xml', '.yml'
    }
foreach ($file in $textFiles) {
    $content = Get-Content -LiteralPath $file.FullName -Raw -Encoding UTF8
    foreach ($marker in $mojibakeMarkers) {
        if ($content.Contains($marker)) {
            throw "Possible encoding corruption '$marker' in $($file.FullName)."
        }
    }
}

Write-Host "Repository metadata and LaunchPad structure are valid for version $version."
