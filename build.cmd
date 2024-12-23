@echo off
pushd Client
dotnet publish -c Release
popd

pushd Server
dotnet publish -c Release
popd

rmdir /s /q dist
mkdir dist

copy /y fxmanifest.lua dist
copy /y outfits.json dist
copy /y departments.json dist
copy /y identities.json dist
copy /y postals.json dist
copy /y coordinates.json dist
copy /y callout-types.json dist
copy /y jails.json dist
copy /y items.json dist
copy /y arrests.json dist
copy /y users.json dist
copy /y server-settings.json dist

xcopy /y /e /s Client\bin\Release\net452\publish\* dist\Client\
xcopy /y /e /s Server\bin\Release\netstandard2.0\publish\* dist\Server\

xcopy /y /e /s NUI\* dist\NUI\