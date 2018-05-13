SET DB_HOST=localhost
SET DB_USER=postgres


psql -h %DB_HOST% -p 5432 -U %DB_USER% -f %1/insert_db.sql