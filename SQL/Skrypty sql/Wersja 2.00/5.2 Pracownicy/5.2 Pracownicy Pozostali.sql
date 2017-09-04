--Lewy panel pracownicy widok 5.2
--Widok zawsze na dzieñ, opcja 'pozostali'

--select numeread,nazwisko,imie from "KatPracownicy" 
--where confidential <= 8
--and numeread not in  
--(select numeread from "MiejscePracy"   
--where firma in ('TFG')  and  '2017-07-30' between "MiejscePracy".datapocz and "MiejscePracy".datakoniec)
--and numeread in 
--(select numeread from "MiejscePracy"   
--where firma in ('TFG') )
--order by nazwisko,imie ; 

select {views} from "KatPracownicy" 
where confidential <= {firmacombo}.confidential 
and numeread not in  
(select numeread from "MiejscePracy"   
where firma in ({firmacombo}.firma)  and  {datadzisiaj} between "MiejscePracy".datapocz and "MiejscePracy".datakoniec)
and numeread in  
(select numeread from "MiejscePracy"   
where firma in ({firmacombo}.firma) )
order by nazwisko,imie ; 

