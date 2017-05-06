set EAD_DIR=%EAD_DIR%
if not defined EAD_DIR (if exist d: (set EAD_DIR=D:\eteczka.main) else (set EAD_DIR=C:\eteczka.main))
setx EAD_DIR %EAD_DIR% -m

if not exist %EAD_DIR%\pliki (mkdir %EAD_DIR%\pliki)
if not exist %EAD_DIR%\archiwum (mkdir %EAD_DIR%\archiwum)
if not exist %EAD_DIR%\logs (mkdir %EAD_DIR%\logs)
if not exist %EAD_DIR%\strukturaFirmy (mkdir %EAD_DIR%\strukturaFirmy)

