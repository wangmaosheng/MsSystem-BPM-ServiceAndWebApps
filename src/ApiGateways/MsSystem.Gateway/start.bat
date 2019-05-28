@echo off

title MsSystem.Gateway

set ASPNETCORE_ENVIRONMENT=Development
cd /d %~dp0
dotnet watch run -c Debug --urls http://*:5000