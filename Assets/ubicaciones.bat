@echo off
setlocal EnableExtensions

rem === Opciones (editables) ==============================
rem Extensiones a incluir (vacío = todas). Separadas por ; sin espacios.
set "EXT=.cs;.js;.ts;.tsx;.jsx;.json;.yaml;.yml;.xml;.md;.txt;.shader;.unity;.prefab;.mat;.asmdef;.ps1;.bat"
rem Carp. a excluir (subcadenas, case-insensitive). Separadas por ; sin espacios.
set "EXC=\node_modules\;.git\;\Library\;\Temp\;\Logs\;\obj\;\bin\;\Build\;\DerivedData\;\.idea\;\.vs\;\ProjectSettings\;\UserSettings\"
rem Profundidad máxima relativa (0 = ilimitada). Raíz=0, subcarpeta directa=1, etc.
set "MAXDEPTH=0"
rem Salida:
set "OUT=estructura_ultra_min.txt"
rem =======================================================

powershell -NoLogo -NoProfile -Command ^
  "$root = Get-Location; " ^
  "$exts = ($env:EXT -split ';' | Where-Object {$_ -ne ''}) ;" ^
  "$excs = ($env:EXC -split ';' | Where-Object {$_ -ne ''}) ;" ^
  "$maxd = [int]($env:MAXDEPTH) ;" ^
  "$files = Get-ChildItem -Recurse -File -Force | Where-Object { " ^
  "  $p = $_.FullName ;" ^
  "  if($excs.Count -gt 0){ foreach($e in $excs){ if($p -like ('*' + $e + '*')){ return $false } } } " ^
  "  if($exts.Count -gt 0){ if($exts -notcontains $_.Extension){ return $false } } " ^
  "  $rel = Resolve-Path -LiteralPath $_.FullName | ForEach-Object { $_.Path.Substring($root.Path.Length+1) } ;" ^
  "  $depth = ($rel -replace '\\','/').Split('/').Count - 1 ;" ^
  "  if($maxd -gt 0 -and $depth -gt $maxd){ return $false } " ^
  "  return $true " ^
  "} | ForEach-Object { " ^
  "  $rel = $_.FullName.Substring($root.Path.Length+1) -replace '\\','/'; " ^
  "  [PSCustomObject]@{ Dir = (Split-Path $rel -Parent); Name = (Split-Path $rel -Leaf) } " ^
  "} ;" ^
  "$dirs = @{} ; $listDirs = New-Object System.Collections.Generic.List[string]; $i=0 ;" ^
  "foreach($f in $files){ $d=$f.Dir ; if(-not $dirs.ContainsKey($d)){ $dirs[$d]=$i; $listDirs.Add($d); $i++ } } " ^
  "$sb = New-Object System.Text.StringBuilder ;" ^
  "$null = $sb.AppendLine('#D');" ^
  "for($k=0; $k -lt $listDirs.Count; $k++){ $d=$listDirs[$k]; $null = $sb.Append($k).Append('|').AppendLine($d) }" ^
  "$null = $sb.AppendLine('#F');" ^
  "foreach($f in $files){ $id = $dirs[$f.Dir]; $null = $sb.Append($id).Append('|').AppendLine($f.Name) }" ^
  "[IO.File]::WriteAllText($env:OUT, $sb.ToString(), [Text.Encoding]::UTF8)"

echo Generado: "%OUT%"
endlocal
