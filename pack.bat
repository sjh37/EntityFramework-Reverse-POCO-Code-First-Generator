@echo off
copy "EntityFramework Reverse POCO Generator\*.tt*" ItemTemplates\Data\
copy "EntityFramework Reverse POCO Generator\*.txt" ItemTemplates\Data
cd ItemTemplates\Data
del *.zip
"C:\Program Files\7-Zip\7z.exe" a "efrpoco.zip"
del *.t*
cd..\..
"C:\Program Files\7-Zip\7z.exe" a "EntityFramework Reverse POCO Generator 1.0.0.zip"
rem ren "EntityFramework Reverse POCO Generator 1.0.0.zip" "EntityFramework Reverse POCO Generator 1.0.0.vsix"