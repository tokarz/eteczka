--Lewy panel pracownicy widok 5.2
--Widok zawsze na dzieñ, opcja 'pozostali'
--Lista firm brana z KatLoginy
--

--select numeread,nazwisko,imie from "KatPracownicy" where numeread not in  
--(select numeread from "MiejscePracy"   
--where firma in ('TFW','TFG')  and  '2017-07-30' between "MiejscePracy".datapocz and "MiejscePracy".datakoniec)
--and numeread in 
--(select numeread from "MiejscePracy"   
--where firma in ('TFW','TFG') )
--order by nazwisko,imie ; 

select {views} from "KatPracownicy" where numeread not in  
(select numeread from "MiejscePracy"   
where firma in ({listafirm})  and  {datadzisiaj} between "MiejscePracy".datapocz and "MiejscePracy".datakoniec)
and numeread in 
(select numeread from "MiejscePracy"   
where firma in ({listafirm}) )
order by nazwisko,imie ; 

