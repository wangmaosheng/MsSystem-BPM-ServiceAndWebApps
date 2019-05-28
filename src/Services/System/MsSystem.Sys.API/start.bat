@echo off

title MsSystem.Sys.API

set ASPNETCORE_ENVIRONMENT=Development
cd /d %~dp0
dotnet watch run -c Debug --urls http://*:5002