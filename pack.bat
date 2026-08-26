@echo on

REM The efrpg dotnet tool is no longer built here. Its source and its NuGet package both come
REM from the separate Efrpg repository; this script packages the VSIX item template only.

copy LICENSE ItemTemplate\license.txt
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