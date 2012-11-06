@echo off
md VS.Extension\ItemTemplates\Data\EFRPOCO
copy "EntityFramework Reverse POCO Generator\*.tt*" VS.Extension\ItemTemplates\Data\EFRPOCO\
cd VS.Extension\ItemTemplates\Data
"C:\Program Files\7-Zip\7z.exe" u EFRPOCO.zip
del EFRPOCO /q
rd EFRPOCO
cd..\..
"C:\Program Files\7-Zip\7z.exe" a EF.Reverse.POCO.zip