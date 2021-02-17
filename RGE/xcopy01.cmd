@echo off
rem cmd /c xcopy01.cmd 12345 \\t90\tmp\source d:\tmp\target 1> xcopy01.cmd.12345.tmp 2> xcopy01.cmd.12345.tmp
set Result=1
set DateTimeStamp=%1
set SourceFolder=%2
set TargetFolder=%3

set ScriptFile=%0
set ScriptFolder=%~dp0
set ScriptName=%~n0
set FileLog=%ScriptFile%.%DateTimeStamp%.log
echo **************************************************************************
echo LOG   
echo **************************************************************************
echo Start  %date% %time%
chcp 65001
echo Host %COMPUTERNAME%
echo User %USERNAME%
echo Log %FileLog%
echo SourceFolder %SourceFolder%
echo TargetFolder %TargetFolder%

dir "%SourceFolder%" 
rem net  use  \\deploy2\tmp
dir \\fs1-oduyu\distrib


if "%TargetFolder%"=="" echo No required parameters&&GOTO :END

if not exist "%SourceFolder%" echo No access to "%SourceFolder%" &GOTO :END
rem dir "%SourceFolder%"
tree "%SourceFolder%" /f
if not exist "%TargetFolder%" md "%TargetFolder%"
if not exist "%TargetFolder%" md "%TargetFolder%" echo Target folder "%TargetFolder%" not available&GOTO :END
echo XCOPY
xcopy "%SourceFolder%" "%TargetFolder%" /E /C /I /F /Y
echo %ERRORLEVEL%
set Result=0

:END
echo Finish %date% %time% ERRORLEVL=%Result%
rem if exist "%FileLog%" start cmd.exe /c REN "%FileLog%" "%ScriptFile%.%DateTimeStamp%.log"
rem if exist "%FileLog%" start timeout /t 10 & cmd.exe /c copy /y "%FileLog%" "%ScriptFile%.%DateTimeStamp%.log"
echo **************************************************************************
echo SCRIPT %ScriptFile%
echo **************************************************************************
type %ScriptFile%
set
exit %Result%