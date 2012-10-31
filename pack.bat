"C:\Windows\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe" /t:Build  /p:Configuration="Release";Platform="any cpu"
del *.nupkg /Q
nuget pack -properties Configuration=Release
