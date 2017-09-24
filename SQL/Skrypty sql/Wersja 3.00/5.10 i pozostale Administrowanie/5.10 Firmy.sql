--Lewy panel firmy widok 5.10
--Lista firm brana z KatFirmy
--Uwaga : tylko administrator ma pe³n¹ listê firm i tylko on mo¿e dodawaæ i edytowaæ
--dotyczy œrodkowej listy Menu firmy wybrano firma

--select firma, nazwa ,nazwa2 from "KatFirmy" 
--where not "KatFirmy".usuniety and "KatFirmy".firma in ('TFG','TFW')
--order by firma ; 

select {views} from "KatFirmy" 
where not "KatFirmy".usuniety and "KatFirmy".firma in ({listafirm})
order by firma ; 


