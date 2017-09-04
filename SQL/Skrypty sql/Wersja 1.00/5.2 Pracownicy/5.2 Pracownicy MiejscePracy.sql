--Widok 5.2 wyúwietlamy sekcjÍ Miejsce Pracy
--Widok pracownikÛw zosta≥ utworzony przez kwerendÍ z pliku  5.2 Pracownicy

--select {views} from "KatPracownicy" where numeread in
--(select numeread from "MiejscePracy"
--where firma in ({listafirm})  and {datadzisiaj} between "MiejscePracy".datapocz and "MiejscePracy".datakoniec)
--order by nazwisko,imie ; 

--ten widok w vfp realizuje za pomocπ AfterRowColChange lub BeforRowColChange w obiekcie Grid
--moøe to byÊ Click

--select datapocz, datakoniec, "MiejscePracy".firma, "KatRejony".nazwa as rejon, "KatWydzial".nazwa as wydzial, "KatPodWydzial".nazwa as podwydzial, 
--konto5 from "MiejscePracy"
--left outer join "KatRejony" on "MiejscePracy".rejon = "KatRejony".rejon and "MiejscePracy".firma = "KatRejony".firma
--left outer join "KatWydzial" on "MiejscePracy".wydzial = "KatWydzial".wydzial and "MiejscePracy".firma = "KatWydzial".firma
--left outer join "KatPodWydzial" on "MiejscePracy".podwydzial = "KatPodWydzial".podwydzial and "MiejscePracy".firma = "KatPodWydzial".firma
--and "MiejscePracy".wydzial = "KatPodWydzial".wydzial
--where numeread like '%WOèLEC%' and "MiejscePracy".firma in ('TFW','JAG','TFPOZ');

select datapocz, datakoniec, "MiejscePracy".firma, "KatRejony".nazwa as rejon, "KatWydzial".nazwa as wydzial, "KatPodWydzial".nazwa as podwydzial, 
konto5 from "MiejscePracy"
left outer join "KatRejony" on "MiejscePracy".rejon = "KatRejony".rejon and "MiejscePracy".firma = "KatRejony".firma
left outer join "KatWydzial" on "MiejscePracy".wydzial = "KatWydzial".wydzial and "MiejscePracy".firma = "KatWydzial".firma
left outer join "KatPodWydzial" on "MiejscePracy".podwydzial = "KatPodWydzial".podwydzial and "MiejscePracy".firma = "KatPodWydzial".firma
and "MiejscePracy".wydzial = "KatPodWydzial".wydzial
where numeread like {numeread} and "MiejscePracy".firma in ({listafirm});

