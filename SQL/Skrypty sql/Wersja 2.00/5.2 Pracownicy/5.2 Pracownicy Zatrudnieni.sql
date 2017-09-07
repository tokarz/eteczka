--Lewy panel pracownicy widok 5.2
--Widok zawsze na dzieñ, opcja 'zatrudnieni'


--select numeread,nazwisko,imie from "KatPracownicy" 
--where confidential <= 8
--and numeread in  
--(select numeread from "MiejscePracy"   
--where firma in ('TFG')  and  '2017-07-30' between "MiejscePracy".datapocz and "MiejscePracy".datakoniec)
--order by nazwisko,imie ; 

select {views} from "KatPracownicy" 
where confidential <= {firmacombo}.confidential 
and numeread in  
(select numeread from "MiejscePracy"   
where firma in ({firmacombo}.firma)  and  {datadzisiaj} between "MiejscePracy".datapocz and "MiejscePracy".datakoniec)
order by nazwisko,imie ; 


