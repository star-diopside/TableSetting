@echo off

%SystemRoot%\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe TableSetting.sln /p:Configuration=Release /p:Platform="Any CPU" /t:Rebuild

pause
