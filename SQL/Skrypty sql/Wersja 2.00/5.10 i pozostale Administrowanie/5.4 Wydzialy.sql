--Trzeci panel wydzia�y widok 5.4
--Uwaga : Och�dzka mo�e dodawa� i usuwa� wydzia�y
--dotyczy �rodkowej listy Menu firmy wybrano wydzia�y

--select firma,wydzial, nazwa from "KatWydzial" 
--where "KatWydzial".firma in ('TFG')
--order by firma ; 

select {views} from "KatWydzial" 
where "KatWydzial".firma in ({jednafirma})
order by firma ; 


