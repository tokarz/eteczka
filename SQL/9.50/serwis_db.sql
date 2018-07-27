-- Database: "E-Agropin-EAD"
-- Zmiana pola z numeric na character; 


  \connect E-Agropin-EAD


ALTER TABLE "Pliki" ADD symboleadtmp character(25) ;

UPDATE "Pliki" SET symboleadtmp = symbolead ;

UPDATE "Pliki" SET symbolead = symbol 
WHERE 
NOT ( substring(symbol from 1 for length(trim(symbol))) = substring(symbolead from 1 for length(trim(symbol))) ) 
AND NOT symbol LIKE '%BadLekOkr%' 
AND NOT symbol LIKE '%PotPosUpr%' 
AND NOT symbolead LIKE '%_ead%' ;

UPDATE "Pliki" SET symbolead = symbol 
WHERE 
NOT ( substring(symbol from 1 for length(trim(symbol))) = substring(symbolead from 1 for length(trim(symbol))) ) 
AND NOT symbol LIKE '%BadLekOkr%' 
AND NOT symbol LIKE '%PotPosUpr%' 
AND symbolead LIKE '%_ead%' ;

UPDATE "Pliki" SET symbol = 'BadLekOkr77' 
WHERE 
substring(symbol from 1 for length(trim(symbol))) = 'BadLekOkr' ; 

UPDATE "Pliki" SET symbolead = 'BadLekOkr' 
WHERE 
substring(symbol from 1 for length(trim(symbol))) LIKE '%BadLekOkr77%'  
AND NOT symbolead LIKE '%BadLekOkr%' ;

UPDATE "KatRejony" SET firma = 'Jagrol_old', usuniety = 'True'  
WHERE firma = 'Jagrol' ;

UPDATE "KatWydzial" SET firma = 'Jagrol_old', usuniety = 'True'  
WHERE firma = 'Jagrol' ;

UPDATE "KatKonta5" SET firma = 'Jagrol_old', usuniety = 'True'  
WHERE firma = 'Jagrol' ;


