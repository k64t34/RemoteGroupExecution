rem cmd /c xcopy01.cmd 12345 \\t90\tmp\source d:\tmp\target 1> xcopy01.cmd.12345.tmp 2> xcopy01.cmd.12345.tmp
@echo off
set Result=1
set DateTimeStamp=%1
set SourceFolder=%2
set TargetFolder=%3

set ScriptFile=%0
set ScriptFolder=%~dp0
set ScriptName=%~n0
set FileLog=%ScriptFolder%%ScriptFile%.%DateTimeStamp%.tmp
echo **************************************************************************
echo %COMPUTERNAME% %USERNAME% %ScriptFolder%%0
echo **************************************************************************
echo Start  %date% %time%

if "%TargetFolder%"=="" echo No required parameters&&GOTO :END

if not exist "%SourceFolder%" echo Source folder "%SourceFolder%" not exist&GOTO :END
if not exist "%TargetFolder%" md "%TargetFolder%"
if not exist "%TargetFolder%" md "%TargetFolder%" echo Target folder "%TargetFolder%" not available&GOTO :END
echo XCOPY
xcopy "%SourceFolder%" "%TargetFolder%" /E /C /I /F /Y
echo %ERRORLEVEL%

:END
set Result=0
echo Finish %date% %time% ERRORLEVL=%Result%
rem if exist "%FileLog%" start cmd.exe /c REN "%FileLog%" "%ScriptFile%.%DateTimeStamp%.log"
if exist "%FileLog%" start timeout /t 10 & cmd.exe /c copy /y "%FileLog%" "%ScriptFile%.%DateTimeStamp%.log"
exit %Result%