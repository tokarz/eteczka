--Widok 5.2 wy�wietlamy sekcj� Miejsce Pracy
--Widok pracownik�w zosta� utworzony przez kwerend� z pliku  5.2 Pracownicy bez wzgl�du na opcje 

--select datapocz, datakoniec, 
--"MiejscePracy".firma, 
--"MiejscePracy".rejon,"MiejscePracy".wydzial,"MiejscePracy".podwydzial,
--"KatRejony".nazwa as rejonnazwa, "KatWydzial".nazwa as wydzialnazwa, "KatPodWydzial".nazwa as podwydzialnazwa, 
--konto5 from "MiejscePracy"
--left outer join "KatRejony" 
--on "MiejscePracy".rejon = "KatRejony".rejon and "MiejscePracy".firma = "KatRejony".firma 
--left outer join "KatWydzial" 
--on "MiejscePracy".wydzial = "KatWydzial".wydzial and "MiejscePracy".firma = "KatWydzial".firma
--left outer join "KatPodWydzial" 
--on "MiejscePracy".podwydzial = "KatPodWydzial".podwydzial and "MiejscePracy".firma = "KatPodWydzial".firma
--and "MiejscePracy".wydzial = "KatPodWydzial".wydzial 
--where not "MiejscePracy".usuniety and numeread like '%WO�LEC%' and "MiejscePracy".firma in ('TFW','JAGROL','TF Pozna�','TFG');

select datapocz, datakoniec, 
"MiejscePracy".firma,
"MiejscePracy".rejon,"MiejscePracy".wydzial,"MiejscePracy".podwydzial,
"KatRejony".nazwa as rejonnazwa, "KatWydzial".nazwa as wydzialnazwa, "KatPodWydzial".nazwa as podwydzialnazwa, 
konto5 from "MiejscePracy"
left outer join "KatRejony" 
on "MiejscePracy".rejon = "KatRejony".rejon and "MiejscePracy".firma = "KatRejony".firma 
left outer join "KatWydzial" 
on "MiejscePracy".wydzial = "KatWydzial".wydzial and "MiejscePracy".firma = "KatWydzial".firma 
left outer join "KatPodWydzial" 
on "MiejscePracy".podwydzial = "KatPodWydzial".podwydzial and "MiejscePracy".firma = "KatPodWydzial".firma 
and "MiejscePracy".wydzial = "KatPodWydzial".wydzial 
where not "MiejscePracy".usuniety and numeread like {listaprac}.numeread and "MiejscePracy".firma in ({firmacombo}.firma);

