--Lewy panel firmy widok 5.10
--Lista firm brana z KatFirmy
--Uwaga : tylko administrator ma pe�n� list� firm i tylko on mo�e dodawa� i edytowa�
--dotyczy �rodkowej listy Menu firmy wybrano firma

--select firma, nazwa ,nazwa2 from "KatFirmy" 
--where not "KatFirmy".usuniety and "KatFirmy".firma in ('TFG','TFW')
--order by firma ; 

select {views} from "KatFirmy" 
where not "KatFirmy".usuniety and "KatFirmy".firma in ({listafirm})
order by firma ; 


