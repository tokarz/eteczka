--Trzeci panel no�nik koszt�w (konto5) widok 5.7
--Uwaga : Och�dzka mo�e dodawa� i usuwa� konta5
--dotyczy �rodkowej listy Menu firmy wybrano konta5

--select firma,konto5,kontoskr, nazwa from "KatKonta5" 
--where not "KatKonta5".usuniety and "KatKonta5".firma in ('TFG')
--order by firma ; 

select {views} from "KatKonta5" 
where not "KatKonta5".usuniety and "KatKonta5".firma in ({jednafirma})
order by firma ; 


