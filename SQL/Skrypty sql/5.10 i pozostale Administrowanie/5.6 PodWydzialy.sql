--Trzeci panel podwydzia�y widok 5.6
--Uwaga : Och�dzka mo�e dodawa� i usuwa� podwydzia�y 
--podwydzia�y s� powi�zane z wydzia�ami ale nie wszystkie wydzia�y maj� podwydzia�y
--dotyczy �rodkowej listy Menu firmy wybrano podwydzia�y

--select firma,wydzial,podwydzial, nazwa from "KatPodWydzial" 
--where "KatPodWydzial".firma in ('TFW')
--and "KatPodWydzial".wydzial in ('02')
--order by firma,wydzial ; 

select {views} from "KatPodWydzial" 
where "KatPodWydzial".firma in ({jednafirma})
and "KatPodWydzial".wydzial in ({jedenwydzial})
order by firma, wydzial ; 


