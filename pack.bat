@echo off
md VS.Extension\ItemTemplates\Data
copy "EntityFramework Reverse POCO Generator\*.tt*" VS.Extension\ItemTemplates\Data
cd VS.Extension
del *.zip
"C:\Program Files\7-Zip\7z.exe" a EF.Reverse.POCO.zip