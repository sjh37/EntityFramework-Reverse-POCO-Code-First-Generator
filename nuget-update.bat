@echo off

pushd "%~dp0"
for /f "tokens=1* delims=*" %%p in ('dir /s /b packages.config') do call :UPDATE "%%p"
popd

pause
goto :EOF

:UPDATE
echo Installing - "%~dpnx1"
NuGet.exe install "%~dpnx1" -outputdirectory Packages -source "https://nuget.org/api/v2/"
goto :EOF

:EOF