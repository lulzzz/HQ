@ECHO OFF

REM first time use
git submodule update --init --recursive

REM use .gitconfig for this repository
git config --local include.path /.gitconfig

REM switch all submodules to master
git submodule foreach git checkout master

REM update all submodules to use tips of master branches
git submodule foreach git pull origin master

REM remove all source shadow copies
FOR /D %%p IN ("src\HQ\*.*") DO rmdir "%%p" /s /q
FOR /D %%p IN ("src\HQ.DocumentDb\*.*") DO rmdir "%%p" /s /q
FOR /D %%p IN ("src\HQ.MySql\*.*") DO rmdir "%%p" /s /q
FOR /D %%p IN ("src\HQ.Sqlite\*.*") DO rmdir "%%p" /s /q
FOR /D %%p IN ("src\HQ.SqlServer\*.*") DO rmdir "%%p" /s /q

REM need to move into the directory for MSBuild parameters to work
cd src

REM build once for merge source, and again for compile
START /WAIT dotnet build -c Debug
START /WAIT dotnet build -c Debug

cd..