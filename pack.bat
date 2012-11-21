@echo off
copy "EntityFramework.Reverse.POCO.Generator\*.tt*" ItemTemplate\ItemTemplates\
copy "ItemTemplate\*.png" ItemTemplate\ItemTemplates\
copy "ItemTemplate\*.ico" ItemTemplate\ItemTemplates\
copy "ItemTemplate\*.vstemplate" ItemTemplate\ItemTemplates\
copy "EntityFramework.Reverse.POCO.Generator\license.txt" ItemTemplate\
cd ItemTemplate\ItemTemplates\
"C:\Program Files\7-Zip\7z.exe" a efrpoco.zip
del *.p* *.t* *.i* *.v*
cd..\..
