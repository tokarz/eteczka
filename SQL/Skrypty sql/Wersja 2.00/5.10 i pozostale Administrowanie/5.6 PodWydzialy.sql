--Trzeci panel podwydzia³y widok 5.6
--Uwaga : Ochódzka mo¿e dodawaæ i usuwaæ podwydzia³y 
--podwydzia³y s¹ powi¹zane z wydzia³ami ale nie wszystkie wydzia³y maj¹ podwydzia³y
--dotyczy œrodkowej listy Menu firmy wybrano podwydzia³y

--select firma,wydzial,podwydzial, nazwa from "KatPodWydzial" 
--where "KatPodWydzial".firma in ('TFW')
--and "KatPodWydzial".wydzial in ('02')
--order by firma,wydzial ; 

select {views} from "KatPodWydzial" 
where "KatPodWydzial".firma in ({jednafirma})
and "KatPodWydzial".wydzial in ({jedenwydzial})
order by firma, wydzial ; 


