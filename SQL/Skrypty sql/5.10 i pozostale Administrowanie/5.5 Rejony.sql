--Trzeci panel rejony widok 5.5
--Lista firm brana z KatRejony
--Uwaga : Och�dzka mo�e dodawa� i usuwa� rejony
--dotyczy �rodkowej listy Menu firmy wybrano rejony

--select firma,rejon, nazwa from "KatRejony" 
--where "KatRejony".firma in ('TFG')
--order by firma ; 

select {views} from "KatRejony" 
where "KatRejony".firma in ({jednafirma})
order by firma ; 


