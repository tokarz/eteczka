IF EXIST "c:\inetpub\wwwroot\EAD" (
    rmdir "c:\inetpub\wwwroot\EAD" /s /q
)


mkdir "c:\inetpub\wwwroot\EAD"

xcopy "../Eteczka/Eteczka/app" "c:/inetpub/wwwroot/EAD/app" /E
xcopy "../Eteczka/Eteczka/App_Start" "c:/inetpub/wwwroot/EAD/App_Start" /E
xcopy "../Eteczka/Eteczka/bin" "c:/inetpub/wwwroot/EAD/bin" /E
xcopy "../Eteczka/Eteczka/Content" "c:/inetpub/wwwroot/EAD/Content" /E
xcopy "../Eteczka/Eteczka/fonts" "c:/inetpub/wwwroot/EAD/fonts" /E
xcopy "../Eteczka/Eteczka/node_modules" "c:/inetpub/wwwroot/EAD/node_modules" /E
xcopy "../Eteczka/Eteczka/Properties" "c:/inetpub/wwwroot/EAD/Properties" /E
xcopy "../Eteczka/Eteczka/Providers" "c:/inetpub/wwwroot/EAD/Providers" /E
xcopy "../Eteczka/Eteczka/Scripts" "c:/inetpub/wwwroot/EAD/Scripts" /E

xcopy "../Eteczka/Eteczka/web.nlog" "c:/inetpub/wwwroot/EAD/web.nlog" /E
xcopy "../Eteczka/Eteczka/Web.config" "c:/inetpub/wwwroot/EAD/Web.config" /E
xcopy "../Eteczka/Eteczka/index.html" "c:/inetpub/wwwroot/EAD/index.html" /E
xcopy "../Eteczka/Eteczka/Global.asax" "c:/inetpub/wwwroot/EAD/Global.asax" /E


