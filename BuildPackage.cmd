@ECHO OFF

RMDIR /S /Y Package
MKDIR Package

COPY /Y Main\bin\Release\SpotlightToDesktop.exe Package\
COPY /Y Main\bin\Release\*.dll Package\

COPY /Y Silent\bin\Release\SpotlightToDesktopSilent.exe Package\
REM COPY /Y Silent\bin\Release\*.dll Package\