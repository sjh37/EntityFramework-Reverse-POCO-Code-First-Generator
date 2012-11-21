@echo off
copy "EntityFramework Reverse POCO Generator\*.tt*" ItemTemplate\
cd ItemTemplate
del *.zip
"C:\Program Files\7-Zip\7z.exe" a "EntityFramework Reverse POCO Generator 1.0.0.zip"