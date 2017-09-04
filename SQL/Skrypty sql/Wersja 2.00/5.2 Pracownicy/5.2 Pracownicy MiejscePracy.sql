--Widok 5.2 wy�wietlamy sekcj� Miejsce Pracy
--Widok pracownik�w zosta� utworzony przez kwerend� z pliku  5.2 Pracownicy bez wzgl�du na opcje 

--select datapocz, datakoniec, "MiejscePracy".firma, "KatRejony".nazwa as rejon, "KatWydzial".nazwa as wydzial, "KatPodWydzial".nazwa as podwydzial, 
--konto5 from "MiejscePracy"
--left outer join "KatRejony" on "MiejscePracy".rejon = "KatRejony".rejon and "MiejscePracy".firma = "KatRejony".firma
--left outer join "KatWydzial" on "MiejscePracy".wydzial = "KatWydzial".wydzial and "MiejscePracy".firma = "KatWydzial".firma
--left outer join "KatPodWydzial" on "MiejscePracy".podwydzial = "KatPodWydzial".podwydzial and "MiejscePracy".firma = "KatPodWydzial".firma
--and "MiejscePracy".wydzial = "KatPodWydzial".wydzial
--where numeread like '%WO�LEC%' and "MiejscePracy".firma in ('TFW','JAGROL','TF Pozna�','TFG');

select datapocz, datakoniec, "MiejscePracy".firma, "KatRejony".nazwa as rejon, "KatWydzial".nazwa as wydzial, "KatPodWydzial".nazwa as podwydzial, 
konto5 from "MiejscePracy"
left outer join "KatRejony" on "MiejscePracy".rejon = "KatRejony".rejon and "MiejscePracy".firma = "KatRejony".firma
left outer join "KatWydzial" on "MiejscePracy".wydzial = "KatWydzial".wydzial and "MiejscePracy".firma = "KatWydzial".firma
left outer join "KatPodWydzial" on "MiejscePracy".podwydzial = "KatPodWydzial".podwydzial and "MiejscePracy".firma = "KatPodWydzial".firma
and "MiejscePracy".wydzial = "KatPodWydzial".wydzial
where numeread like {listaprac}.numeread and "MiejscePracy".firma in ({firmacombo}.firma);

