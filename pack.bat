@echo off
copy "EntityFramework.Reverse.POCO.Generator\*.ttinclude" ItemTemplate\ItemTemplates\
copy "EntityFramework.Reverse.POCO.Generator\Database.tt" ItemTemplate\ItemTemplates\
copy "ItemTemplate\*.png" ItemTemplate\ItemTemplates\
copy "ItemTemplate\*.ico" ItemTemplate\ItemTemplates\
copy "ItemTemplate\*.vstemplate" ItemTemplate\ItemTemplates\
cd ItemTemplate\ItemTemplates\
del *.zip /s
"C:\Program Files\7-Zip\7z.exe" a efrpoco.zip
del *.p* *.t* *.i* *.v*
copy *.zip "..\..\EntityFramework Reverse POCO Generator\ItemTemplates\"
copy *.zip "..\..\EntityFramework Reverse POCO Generator\ItemTemplates\CSharp\Data\1033\"
copy *.zip "..\..\EntityFramework Reverse POCO Generator\ItemTemplates\CSharp\1033\"
cd..\..
pause