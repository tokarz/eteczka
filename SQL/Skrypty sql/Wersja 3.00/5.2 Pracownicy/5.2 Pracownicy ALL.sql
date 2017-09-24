--Lewy panel pracownicy widok 5.2
--Widok zawsze na dzieñ, opcja 'wszyscy'

--select numeread,nazwisko,imie from "KatPracownicy" 
--where not usuniety 
--and confidential <= 8 
--and numeread in  
--(select numeread from "MiejscePracy"   
--where not usuniety and firma in ('TFG') )
--order by nazwisko,imie ; 

select {views} from "KatPracownicy" 
where not usuniety 
and confidential <= {firmacombo}.confidential 
and numeread in  
(select numeread from "MiejscePracy"   
where not usuniety and firma in ({firmacombo}.firma) )
order by nazwisko,imie ; 

