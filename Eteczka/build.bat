set SRC_DIR=%~dp0\Eteczka
set SERVER_CONFIGURATION=Release
set PUBLISH_PROFILE = "%SRC_DIR%\Properites\PublishProfiles\ETeczka.pubxml"
@REM ------------------------------

"C:\Program Files (x86)\MSBuild\12.0\bin\msbuild.exe" "%SRC_DIR%\Eteczka.csproj" /p:Configuration=%SERVER_CONFIGURATION% /p:Platform=AnyCPU /clp:ErrorsOnly /t:Clean,Rebuild

@REM ---- Testy BE-----

@REM ---- Testy FE-----


@REM ---- Raport Pokrycia BE-----


@REM ---- Raport Pokrycia FE-----


@REM Koniec  2