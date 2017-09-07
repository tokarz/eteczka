--Trzeci panel wydzia³y widok 5.4
--Uwaga : Ochódzka mo¿e dodawaæ i usuwaæ wydzia³y
--dotyczy œrodkowej listy Menu firmy wybrano wydzia³y

--select firma,wydzial, nazwa from "KatWydzial" 
--where "KatWydzial".firma in ('TFG')
--order by firma ; 

select {views} from "KatWydzial" 
where "KatWydzial".firma in ({jednafirma})
order by firma ; 


