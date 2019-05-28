@echo off
:: 变量赋值，使用!name!

setlocal enabledelayedexpansion

set currentPath=%~dp0
set tempModulesPath=%currentPath%\temp
set modulesPath=%currentPath%\src\UI\

set str="publish"

:GOON
for /f "delims=,+, tokens=1,*" %%i in (%str%) do (
    echo --------------------------------------------------------
    echo 【%%i】发布开始
    set path1=%modulesPath%%%i
    set path2=%currentPath%\release\%%i\
    set filnePath=!path2!app_offline.htm
    echo !path1!
    echo 停止【%%i】站点
    if not exist !path2! md !path2!

    cd /d !path1!
    echo 执行发布【!path2!】
    echo 网站维护中>!filnePath!
    call dotnet publish -o !path2!
    call xcopy %tempModulesPath% !path2! /s /e /Q /Y /I
    del !filnePath!
    echo 开启【%%i】站点
    
    echo 【%%i】发布完成
    set str="%%j"
    goto GOON
)

pause