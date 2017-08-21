--Lewy panel pracownicy widok 5.2
--Widok zawsze na dzieñ, opcja 'ALL'
--Lista firm brana z KatLoginy
--

--select numeread,nazwisko,imie from "KatPracownicy" where numeread in  
--(select numeread from "MiejscePracy"   
--where firma in ('TFG','TFW') )
--order by nazwisko,imie ; 


select {views} from "KatPracownicy" where numeread in  
(select numeread from "MiejscePracy"   
where firma in ({listafirm}) )
order by nazwisko,imie ; 

