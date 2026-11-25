param([string]$Configuration = "Release")

$ErrorActionPreference = "Stop"
$root = $PSScriptRoot

if (-not $env:PEAK_GAME_DIR) {
    throw "请先设置环境变量 PEAK_GAME_DIR（PEAK 游戏根目录）"
}

$manifest = Get-Content (Join-Path $root "manifest.json") -Raw -Encoding UTF8 | ConvertFrom-Json
$version = $manifest.version_number
$name = $manifest.name

Write-Host "Building $name $version ..."
dotnet build $root -c $Configuration | Out-Host
if ($LASTEXITCODE -ne 0) { throw "编译失败" }

$dll = Join-Path $root "bin/$Configuration/$name.dll"
if (-not (Test-Path $dll)) { throw "找不到编译产物: $dll" }

$stage = Join-Path $root "dist/stage"
if (Test-Path $stage) { Remove-Item $stage -Recurse -Force }
$pluginDir = Join-Path $stage "BepInEx/plugins/$name"
New-Item -ItemType Directory -Force -Path $pluginDir | Out-Null

# 只放运行需要的 dll，不带 pdb（pdb 里含有本机源码路径）
Copy-Item $dll (Join-Path $pluginDir "$name.dll")
foreach ($f in @("manifest.json", "CHANGELOG.md", "LICENSE", "icon.png")) {
    $src = Join-Path $root $f
    if (Test-Path $src) { Copy-Item $src (Join-Path $stage $f) }
}

# 雷霆商店的介绍页正文取自包内 README.md：
# 有 DESCRIPTION.md 就用它（中文在前 + 英文），否则退回仓库 README.md
$storeReadme = Join-Path $root "DESCRIPTION.md"
if (-not (Test-Path $storeReadme)) { $storeReadme = Join-Path $root "README.md" }
if (Test-Path $storeReadme) { Copy-Item $storeReadme (Join-Path $stage "README.md") }

$zip = Join-Path $root "dist/$name-$version.zip"
if (Test-Path $zip) { Remove-Item $zip -Force }
Compress-Archive -Path (Join-Path $stage "*") -DestinationPath $zip -CompressionLevel Optimal
Write-Host "打包完成: $zip"