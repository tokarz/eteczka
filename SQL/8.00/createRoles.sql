-- Database: "E-Agropin-EAD"
-- DROP DATABASE IF EXISTS "E-Agropin-EAD"; 

  -- TWORZYMY UZYTKOWNIKOW DLA SYSTEMU
REASSIGN OWNED BY ead TO postgres;
DROP OWNED BY ead;
-- repeat the above commands in each database of the cluster
DROP ROLE ead;








