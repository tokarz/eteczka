--Lewy panel pracownicy widok 5.2
--Widok zawsze na dzieñ, opcja 'zatrudnieni'
--Lista firm brana z KatLoginy
--
--select numeread,nazwisko,imie from "KatPracownicy" where numeread in  
--(select numeread from "MiejscePracy"   
--where firma in ('AFM','TFG')  and  '2017-07-30' between "MiejscePracy".datapocz and "MiejscePracy".datakoniec)
--order by nazwisko,imie ; 


select {views} from "KatPracownicy" where numeread in
(select numeread from "MiejscePracy"
where firma in ({listafirm})  and {datadzisaj} between "MiejscePracy".datapocz and "MiejscePracy".datakoniec)
order by nazwisko,imie ; 
