--Zatrudnieni 

SELECT * FROM "KatPracownicy" where 
LOWER (nazwisko) || ' ' || LOWER (imie) || ' ' || pesel LIKE '%woüniak l%' 
and not usuniety and confidential < 8
and numeread in 
(select numeread from "MiejscePracy" where firma IN ('AFM','JAGROL','TFG')
and  
'2017-09-28'  between "MiejscePracy".datapocz and "MiejscePracy".datakoniec) 
ORDER BY nazwisko,imie;

-- Pozostali

SELECT * FROM "KatPracownicy" where 
LOWER (nazwisko) || ' ' || LOWER (imie) || ' ' || pesel LIKE '%610%' 
and not usuniety and confidential < 8
and numeread not in 
(select numeread from "MiejscePracy" where firma IN ('AFM','JAGROL','TFG')
and not usuniety and  
'2017-09-28'  between "MiejscePracy".datapocz and "MiejscePracy".datakoniec) 
and numeread in 
(select numeread from "MiejscePracy" where firma IN ('AFM','JAGROL','TFG')) 
ORDER BY nazwisko,imie;


--ALL 

SELECT * FROM "KatPracownicy" where 
LOWER (nazwisko) || ' ' || LOWER (imie) || ' ' || pesel LIKE '%610%' 
and not usuniety and confidential < 8
and numeread in 
(select numeread from "MiejscePracy" where firma IN ('AFM','JAGROL','TFG')) 
ORDER BY nazwisko,imie;

