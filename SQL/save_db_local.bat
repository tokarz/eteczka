::DZIENNY ZRZUT BAZY DANYCH

::"C:/Program Files/PostgreSQL/9.5/bin\pg_dump.exe" --host localhost --port 5432 --username "postgres" --no-password  --format custom --blobs --file "%EAD_DIR%\backup\ead_db_%date%.backup" "E-Agropin-EAD"


    @echo off
   for /f "tokens=1-4 delims=/ " %%i in ("%date%") do (
     set dow=%%i
     set month=%%j
     set day=%%k
     set year=%%l
   )

    
   set BACKUP_FILE=%EAD_DIR%\backup\ead_db_%date%.backup
   echo backup file name is %BACKUP_FILE%
   SET PGPASSWORD=admin
   echo on
   "C:/Program Files/PostgreSQL/9.5/bin\pg_dump.exe" -h localhost -p 5432 -U postgres -F c -b -v -f %BACKUP_FILE% E-Agropin-EAD