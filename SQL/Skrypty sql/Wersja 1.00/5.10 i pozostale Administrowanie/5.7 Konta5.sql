--Trzeci panel noœnik kosztów (konto5) widok 5.7
--Uwaga : Ochódzka mo¿e dodawaæ i usuwaæ konta5
--dotyczy œrodkowej listy Menu firmy wybrano konta5

--select firma,konto5,kontoskr, nazwa from "KatKonta5" 
--where "KatKonta5".firma in ('TFG')
--order by firma ; 

select {views} from "KatKonta5" 
where "KatKonta5".firma in ({jednafirma})
order by firma ; 


