set SRC_DIR=%~dp0\Eteczka
set SERVER_CONFIGURATION=Debug
set VS_VERSION=%1
set PUBLISH_PROFILE = "%SRC_DIR%\Properites\PublishProfiles\ETeczka.pubxml"
@REM ------------------------------

"C:\Windows\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe "%SRC_DIR%\Eteczka\Eteczka.csproj" /p:Configuration=%SERVER_CONFIGURATION% /p:Platform=AnyCPU /clp:ErrorsOnly /t:Clean,Rebuild

@REM ---- Testy BE-----
REM "C:\Windows\Microsoft.NET\Framework\v4.0.30319\ "%SRC_DIR%\Eteczka.Be.Tests\Eteczka.Be.Tests.csproj" /p:Configuration=%SERVER_CONFIGURATION% /p:Platform=AnyCPU /clp:ErrorsOnly /t:Clean,Rebuild

@REM ---- Testy FE-----


@REM ---- Raport Pokrycia BE-----


@REM ---- Raport Pokrycia FE-----


@REM Koniec  2