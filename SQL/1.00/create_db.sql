-- Database: "E-Agropin-EAD"

-- DROP DATABASE "E-Agropin-EAD";

CREATE DATABASE "E-Agropin-EAD"
  WITH OWNER = postgres
       ENCODING = 'UTF8'
       TABLESPACE = pg_default
       LC_COLLATE = 'German_Germany.1252'
       LC_CTYPE = 'German_Germany.1252'
       CONNECTION LIMIT = -1;



  \connect E-Agropin-EAD


       -- Table: public."KatRejony"

-- DROP TABLE public."KatRejony";

CREATE TABLE public."KatRejony"
(
  id numeric(20,0) NOT NULL, -- Numer ID
  symbol character(20), -- Identyfikator firmy : np. TFG, TFNI itp.
  nazwa character(150), -- Nazwa, np. Top Farms G�ubczyce
  nazwaskrocona character(30), -- Nazwa skr�cona firmy u�ywana w systemie P�atnik
  ulica character(50), -- Ulica w adresie firmy
  numerdomu character(10), -- Numer domu w adresie firmy
  numerlokalu character(10), -- Numer lokalu w adresie firmy
  miasto character(25), -- Miejscowo�� w adresie firmy
  kodpocztowy character(6), -- Kod pocztowy w adresie firmy
  poczta character(25), -- Poczta w adresie firmy
  gmina character(25), -- Gmina w adresie firmy
  powiat character(25), -- Powiat w adresie firmy
  wojewodztwo character(25), -- Wojew�dztwo w adresie firmy
  kraj character(25), -- Kraj w kt�rym jest firma, np. PL lub Polska
  nip character(10), -- Numer NIP firmy
  regon character(20), -- Numer regon firmy
  krs character(25), -- Numer KRS firmy
  pesel character(11), -- Numer pesel w�a�ciciela firmy, je�eli firma jest jednoosobowa i jest to osoba fizyczna
  datamodify time with time zone, -- Data modyfikacji
  idoper character(30), -- Idetyfikator osoby dokonuj�cej wpisu
  idakcept character(30), -- Identyfikator osoby akceptuj�cej
  dataakcept time with time zone, -- Data akceptu przez idakcept
  lokalizacjapapier character(20), -- Lokalizacja dokument�w papierowych
  CONSTRAINT "KatRejony_pkey" PRIMARY KEY (id)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE public."KatRejony"
  OWNER TO postgres;
COMMENT ON COLUMN public."KatRejony".symbol IS 'Identyfikator Rejony : np. TFG, TFNI itp.';
COMMENT ON COLUMN public."KatRejony".nazwa IS 'Nazwa, np. Top Farms G�ubczyce';
COMMENT ON COLUMN public."KatRejony".nazwaskrocona IS 'Nazwa skr�cona Rejony u�ywana w systemie P�atnik';
COMMENT ON COLUMN public."KatRejony".ulica IS 'Ulica w adresie Rejony';
COMMENT ON COLUMN public."KatRejony".numerdomu IS 'Numer domu w adresie Rejony';
COMMENT ON COLUMN public."KatRejony".numerlokalu IS 'Numer lokalu w adresie Rejony';
COMMENT ON COLUMN public."KatRejony".miasto IS 'Miejscowo�� w adresie Rejony';
COMMENT ON COLUMN public."KatRejony".kodpocztowy IS 'Kod pocztowy w adresie Rejony';
COMMENT ON COLUMN public."KatRejony".poczta IS 'Poczta w adresie Rejony';
COMMENT ON COLUMN public."KatRejony".gmina IS 'Gmina w adresie Rejony';
COMMENT ON COLUMN public."KatRejony".powiat IS 'Powiat w adresie Rejony';
COMMENT ON COLUMN public."KatRejony".wojewodztwo IS 'Wojew�dztwo w adresie Rejony';
COMMENT ON COLUMN public."KatRejony".kraj IS 'Kraj w kt�rym jest firma, np. PL lub Polska';
COMMENT ON COLUMN public."KatRejony".nip IS 'Numer NIP Rejony';
COMMENT ON COLUMN public."KatRejony".regon IS 'Numer regon Rejony';
COMMENT ON COLUMN public."KatRejony".krs IS 'Numer KRS Rejony';
COMMENT ON COLUMN public."KatRejony".pesel IS 'Numer pesel w�a�ciciela Rejony, je�eli firma jest jednoosobowa i jest to osoba fizyczna';
COMMENT ON COLUMN public."KatRejony".datamodify IS 'Data modyfikacji';
COMMENT ON COLUMN public."KatRejony".idoper IS 'Idetyfikator osoby dokonuj�cej wpisu';
COMMENT ON COLUMN public."KatRejony".idakcept IS 'Identyfikator osoby akceptuj�cej';
COMMENT ON COLUMN public."KatRejony".dataakcept IS 'Data akceptu przez idakcept';
COMMENT ON COLUMN public."KatRejony".id IS 'Numer ID';
COMMENT ON COLUMN public."KatRejony".lokalizacjapapier IS 'Lokalizacja dokument�w papierowych';



-- Table: public."KatPodWydzialy"

-- DROP TABLE public."KatPodWydzialy";

CREATE TABLE public."KatPodWydzialy"
(
  id numeric(20,0) NOT NULL, -- Numer ID
  symbol character(20), -- Identyfikator podwydzia�u : np. KDR (Kadry - p�ace).
  nazwa character(150), -- Nazwa, np. Top Farms G�ubczyce
  symboldzialy character(20), -- Identyfikator dzia�u nadrz�dnego : np. FA (tabela KatDzialy). Podwydzial KDR nale�y do dzia�u FA
  datamodify time with time zone, -- Data modyfikacji
  idoper character(30), -- Idetyfikator osoby dokonuj�cej wpisu
  idakcept character(30), -- Identyfikator osoby akceptuj�cej
  dataakcept time with time zone, -- Data akceptu przez idakcept
  CONSTRAINT "KatPodDzialy_pkey" PRIMARY KEY (id)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE public."KatPodWydzialy"
  OWNER TO postgres;
COMMENT ON COLUMN public."KatPodWydzialy".id IS 'Numer ID';
COMMENT ON COLUMN public."KatPodWydzialy".symbol IS 'Identyfikator podwydzia�u : np. KDR (Kadry - p�ace).';
COMMENT ON COLUMN public."KatPodWydzialy".nazwa IS 'Nazwa, np. Top Farms G�ubczyce';
COMMENT ON COLUMN public."KatPodWydzialy".symboldzialy IS 'Identyfikator dzia�u nadrz�dnego : np. FA (tabela KatDzialy). Podwydzial KDR nale�y do dzia�u FA';
COMMENT ON COLUMN public."KatPodWydzialy".datamodify IS 'Data modyfikacji';
COMMENT ON COLUMN public."KatPodWydzialy".idoper IS 'Idetyfikator osoby dokonuj�cej wpisu';
COMMENT ON COLUMN public."KatPodWydzialy".idakcept IS 'Identyfikator osoby akceptuj�cej';
COMMENT ON COLUMN public."KatPodWydzialy".dataakcept IS 'Data akceptu przez idakcept';



-- Table: public."KatLokalPapier"

-- DROP TABLE public."KatLokalPapier";

CREATE TABLE public."KatLokalPapier"
(
  id numeric(20,0) NOT NULL, -- Numer ID
  symbolfirma character(20), -- Identyfikator firmy : np. TFG, TFNI itp. symbol z KatFirmy
  symbol character(20), -- Symbol archiwum : np. TFG_piwnica
  nazwa character varying(300), -- Nazwa, np. archiwum Top Farms G�ubczyce
  ulica character(50), -- Ulica gdzie jest archiwum
  numerdomu character(10), -- Numer domu  gdzie jest archiwum
  numerlokalu character(10), -- Numer lokalu  gdzie jest archiwum
  miasto character(25), -- Miejscowo��  gdzie jest archiwum
  kodpocztowy character(6), -- Kod pocztowy  gdzie jest archiwum
  poczta character(25), -- Poczta  gdzie jest archiwum
  datamodify time with time zone, -- Data modyfikacji
  idoper character(30), -- Idetyfikator osoby dokonuj�cej wpisu
  idakcept character(30), -- Identyfikator osoby akceptuj�cej
  dataakcept time with time zone, -- Data akceptu przez idakcept
  CONSTRAINT "KatLokalPapier_pkey" PRIMARY KEY (id)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE public."KatLokalPapier"
  OWNER TO postgres;
COMMENT ON COLUMN public."KatLokalPapier".id IS 'Numer ID';
COMMENT ON COLUMN public."KatLokalPapier".symbolfirma IS 'Identyfikator firmy : np. TFG, TFNI itp. symbol z KatFirmy';
COMMENT ON COLUMN public."KatLokalPapier".symbol IS 'Symbol archiwum : np. TFG_piwnica';
COMMENT ON COLUMN public."KatLokalPapier".nazwa IS 'Nazwa, np. archiwum Top Farms G�ubczyce';
COMMENT ON COLUMN public."KatLokalPapier".ulica IS 'Ulica gdzie jest archiwum';
COMMENT ON COLUMN public."KatLokalPapier".numerdomu IS 'Numer domu  gdzie jest archiwum';
COMMENT ON COLUMN public."KatLokalPapier".numerlokalu IS 'Numer lokalu  gdzie jest archiwum';
COMMENT ON COLUMN public."KatLokalPapier".miasto IS 'Miejscowo��  gdzie jest archiwum';
COMMENT ON COLUMN public."KatLokalPapier".kodpocztowy IS 'Kod pocztowy  gdzie jest archiwum';
COMMENT ON COLUMN public."KatLokalPapier".poczta IS 'Poczta  gdzie jest archiwum';
COMMENT ON COLUMN public."KatLokalPapier".datamodify IS 'Data modyfikacji';
COMMENT ON COLUMN public."KatLokalPapier".idoper IS 'Idetyfikator osoby dokonuj�cej wpisu';
COMMENT ON COLUMN public."KatLokalPapier".idakcept IS 'Identyfikator osoby akceptuj�cej';
COMMENT ON COLUMN public."KatLokalPapier".dataakcept IS 'Data akceptu przez idakcept';



-- Table: public."KatLoginy"

-- DROP TABLE public."KatLoginy";

CREATE TABLE public."KatLoginy"
(
  id numeric(20,0) NOT NULL, -- Kolumna ID
  identyfikator character(30), -- Identyfikator - login
  nazwisko character(40), -- Nazwisko u�ytkownika
  imie character(20), -- Imi� u�ytkownika
  hasloshort character(50), -- Has�o kr�tkie minimum 6 znak�w do potwierdzania szybkiego
  haslolong character(50), -- Has�o d�ugie minimum 12 znak�w do logowania i operacji specjalnych
  rolareadonly boolean, -- Rola - uprawnienia tylko do odczytu
  rolaaddpracownik boolean, -- Rola - uprawnienia do dopisywania i modyfikacji danych pracownika
  rolamodifypracownik boolean, -- Rola - modyfikacja danych pracownika
  rolaaddfile boolean, -- Rola - dodanie pliku do systemu
  rolamodifyfile boolean, -- Rola - modyfikacja opisu pliku ju� istniej�cego w systemie
  rolaslowniki boolean, -- Rola - modyfikacja tabel s�ownikowych
  rolasendmail boolean, -- Rola - uprawnienie do wys�ania pliku mailem
  rolaraport boolean, -- Rola - uprawnienia do raport�w na drukar�
  rolaraportexport boolean, -- Rola uprawnienia do eksportu raport�w np. do xls
  roladoubleakcept boolean, -- Rola - uprawnienia do podw�jnego akceptu
  datamodify time with time zone, -- Data modyfikacji tabeli uprawnie�
  CONSTRAINT "KatLoginy_pkey" PRIMARY KEY (id)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE public."KatLoginy"
  OWNER TO postgres;
COMMENT ON TABLE public."KatLoginy"
  IS 'Katalog u�ytkownik�w systemu';
COMMENT ON COLUMN public."KatLoginy".id IS 'Kolumna ID';
COMMENT ON COLUMN public."KatLoginy".identyfikator IS 'Identyfikator - login';
COMMENT ON COLUMN public."KatLoginy".nazwisko IS 'Nazwisko u�ytkownika';
COMMENT ON COLUMN public."KatLoginy".imie IS 'Imi� u�ytkownika';
COMMENT ON COLUMN public."KatLoginy".hasloshort IS 'Has�o kr�tkie minimum 6 znak�w do potwierdzania szybkiego';
COMMENT ON COLUMN public."KatLoginy".haslolong IS 'Has�o d�ugie minimum 12 znak�w do logowania i operacji specjalnych';
COMMENT ON COLUMN public."KatLoginy".rolareadonly IS 'Rola - uprawnienia tylko do odczytu';
COMMENT ON COLUMN public."KatLoginy".rolaaddpracownik IS 'Rola - uprawnienia do dopisywania i modyfikacji danych pracownika';
COMMENT ON COLUMN public."KatLoginy".rolamodifypracownik IS 'Rola - modyfikacja danych pracownika';
COMMENT ON COLUMN public."KatLoginy".rolaaddfile IS 'Rola - dodanie pliku do systemu';
COMMENT ON COLUMN public."KatLoginy".rolamodifyfile IS 'Rola - modyfikacja opisu pliku ju� istniej�cego w systemie';
COMMENT ON COLUMN public."KatLoginy".rolaslowniki IS 'Rola - modyfikacja tabel s�ownikowych';
COMMENT ON COLUMN public."KatLoginy".rolasendmail IS 'Rola - uprawnienie do wys�ania pliku mailem';
COMMENT ON COLUMN public."KatLoginy".rolaraport IS 'Rola - uprawnienia do raport�w na drukar�';
COMMENT ON COLUMN public."KatLoginy".rolaraportexport IS 'Rola uprawnienia do eksportu raport�w np. do xls';
COMMENT ON COLUMN public."KatLoginy".roladoubleakcept IS 'Rola - uprawnienia do podw�jnego akceptu';
COMMENT ON COLUMN public."KatLoginy".datamodify IS 'Data modyfikacji tabeli uprawnie�';




-- Table: public."KatKategorieAkt"

-- DROP TABLE public."KatKategorieAkt";

CREATE TABLE public."KatKategorieAkt"
(
  id numeric(15,0) NOT NULL, -- Numer Id
  symbol character(20), -- kategorie typu A,B,BC,BE + ewentualne cyfry
  nazwa character(100), -- Nazwa kategorii
  idoper character(30), -- Identyfikator osoby dokonuj�cej wpisu
  datamodify timestamp without time zone, -- Data dokonania wpisu
  idakcept character(30), -- Identyfikator osoby akceptuj�cej
  dataakcept time with time zone, -- Data akceptu przez idakcept
  CONSTRAINT "KatKategorieAkt_pkey" PRIMARY KEY (id)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE public."KatKategorieAkt"
  OWNER TO postgres;
COMMENT ON COLUMN public."KatKategorieAkt".id IS 'Numer Id';
COMMENT ON COLUMN public."KatKategorieAkt".symbol IS 'kategorie typu A,B,BC,BE + ewentualne cyfry';
COMMENT ON COLUMN public."KatKategorieAkt".nazwa IS 'Nazwa kategorii';
COMMENT ON COLUMN public."KatKategorieAkt".idoper IS 'Identyfikator osoby dokonuj�cej wpisu';
COMMENT ON COLUMN public."KatKategorieAkt".datamodify IS 'Data dokonania wpisu';
COMMENT ON COLUMN public."KatKategorieAkt".idakcept IS 'Identyfikator osoby akceptuj�cej';
COMMENT ON COLUMN public."KatKategorieAkt".dataakcept IS 'Data akceptu przez idakcept';



        -- Table: public."KatJrwa"

-- DROP TABLE public."KatJrwa";

CREATE TABLE public."KatJrwa"
(
  id numeric(20,0) NOT NULL, -- Identyfikator ID
  slklas1 character(1), -- Symbol klasyfikacyjny I
  slklas2 character(2), -- Symbol klasyfikacyjny II
  slklas3 character(3), -- Symbol klasyfikacyjny III
  slklas4 character(4), -- Symbol klasyfikacyjny IV
  kategoria character(5), -- kategoria wed�ug s�ownika KategorieAkt
  nazwa character(100),
  opis character varying(300), -- Opis szczeg�owy
  idoper character(30), -- Identyfikator osoby dokonuj�cej wpisu
  datamodify timestamp without time zone, -- Data dokonania wpisu
  idakcept character(30), -- Identyfikator osoby akceptuj�cej
  dataakcept time with time zone, -- Data akceptu przez idakcept
  CONSTRAINT "KatJrwa_pkey" PRIMARY KEY (id)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE public."KatJrwa"
  OWNER TO postgres;
COMMENT ON TABLE public."KatJrwa"
  IS 'Jednolity Rzeczowy WykazAkt';
COMMENT ON COLUMN public."KatJrwa".id IS 'Identyfikator ID';
COMMENT ON COLUMN public."KatJrwa".slklas1 IS 'Symbol klasyfikacyjny I';
COMMENT ON COLUMN public."KatJrwa".slklas2 IS 'Symbol klasyfikacyjny II';
COMMENT ON COLUMN public."KatJrwa".slklas3 IS 'Symbol klasyfikacyjny III';
COMMENT ON COLUMN public."KatJrwa".slklas4 IS 'Symbol klasyfikacyjny IV';
COMMENT ON COLUMN public."KatJrwa".kategoria IS 'kategoria wed�ug s�ownika KategorieAkt';
COMMENT ON COLUMN public."KatJrwa".nazwa IS 'Opis klasyfikacyjny';
COMMENT ON COLUMN public."KatJrwa".opis IS 'Opis szczeg�owy';
COMMENT ON COLUMN public."KatJrwa".idoper IS 'Identyfikator osoby dokonuj�cej wpisu';
COMMENT ON COLUMN public."KatJrwa".datamodify IS 'Data dokonania wpisu';
COMMENT ON COLUMN public."KatJrwa".idakcept IS 'Identyfikator osoby akceptuj�cej';
COMMENT ON COLUMN public."KatJrwa".dataakcept IS 'Data akceptu przez idakcept';



     -- Table: public."KatFirmy"

-- DROP TABLE public."KatFirmy";

CREATE TABLE public."KatFirmy"
(
  id numeric(20,0) NOT NULL, -- Numer ID
  symbol character(20), -- Identyfikator firmy : np. TFG, TFNI itp.
  nazwa character varying(300), -- Nazwa, np. Top Farms G�ubczyce
  nazwaskrocona character(150), -- Nazwa skr�cona firmy u�ywana w systemie P�atnik
  ulica character(50), -- Ulica w adresie firmy
  numerdomu character(10), -- Numer domu w adresie firmy
  numerlokalu character(10), -- Numer lokalu w adresie firmy
  miasto character(25), -- Miejscowo�� w adresie firmy
  kodpocztowy character(6), -- Kod pocztowy w adresie firmy
  poczta character(25), -- Poczta w adresie firmy
  gmina character(25), -- Gmina w adresie firmy
  powiat character(25), -- Powiat w adresie firmy
  wojewodztwo character(25), -- Wojew�dztwo w adresie firmy
  kraj character(25), -- Kraj w kt�rym jest firma, np. PL lub Polska
  nip character(10), -- Numer NIP firmy
  regon character(20), -- Numer regon firmy
  krs character(25), -- Numer KRS firmy
  pesel character(11), -- Numer pesel w�a�ciciela firmy, je�eli firma jest jednoosobowa i jest to osoba fizyczna
  datamodify time with time zone, -- Data modyfikacji
  idoper character(30), -- Idetyfikator osoby dokonuj�cej wpisu
  idakcept character(30), -- Identyfikator osoby akceptuj�cej
  dataakcept time with time zone, -- Data akceptu przez idakcept
  lokalizacjapapier character(20), -- Lokalizacja dokument�w papierowych
  CONSTRAINT "KatFirmy_pkey" PRIMARY KEY (id)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE public."KatFirmy"
  OWNER TO postgres;
COMMENT ON COLUMN public."KatFirmy".id IS 'Numer ID';
COMMENT ON COLUMN public."KatFirmy".symbol IS 'Identyfikator firmy : np. TFG, TFNI itp.';
COMMENT ON COLUMN public."KatFirmy".nazwa IS 'Nazwa, np. Top Farms G�ubczyce';
COMMENT ON COLUMN public."KatFirmy".nazwaskrocona IS 'Nazwa skr�cona firmy u�ywana w systemie P�atnik';
COMMENT ON COLUMN public."KatFirmy".ulica IS 'Ulica w adresie firmy';
COMMENT ON COLUMN public."KatFirmy".numerdomu IS 'Numer domu w adresie firmy';
COMMENT ON COLUMN public."KatFirmy".numerlokalu IS 'Numer lokalu w adresie firmy';
COMMENT ON COLUMN public."KatFirmy".miasto IS 'Miejscowo�� w adresie firmy';
COMMENT ON COLUMN public."KatFirmy".kodpocztowy IS 'Kod pocztowy w adresie firmy';
COMMENT ON COLUMN public."KatFirmy".poczta IS 'Poczta w adresie firmy';
COMMENT ON COLUMN public."KatFirmy".gmina IS 'Gmina w adresie firmy';
COMMENT ON COLUMN public."KatFirmy".powiat IS 'Powiat w adresie firmy';
COMMENT ON COLUMN public."KatFirmy".wojewodztwo IS 'Wojew�dztwo w adresie firmy';
COMMENT ON COLUMN public."KatFirmy".kraj IS 'Kraj w kt�rym jest firma, np. PL lub Polska';
COMMENT ON COLUMN public."KatFirmy".nip IS 'Numer NIP firmy';
COMMENT ON COLUMN public."KatFirmy".regon IS 'Numer regon firmy';
COMMENT ON COLUMN public."KatFirmy".krs IS 'Numer KRS firmy';
COMMENT ON COLUMN public."KatFirmy".pesel IS 'Numer pesel w�a�ciciela firmy, je�eli firma jest jednoosobowa i jest to osoba fizyczna';
COMMENT ON COLUMN public."KatFirmy".datamodify IS 'Data modyfikacji';
COMMENT ON COLUMN public."KatFirmy".idoper IS 'Idetyfikator osoby dokonuj�cej wpisu';
COMMENT ON COLUMN public."KatFirmy".idakcept IS 'Identyfikator osoby akceptuj�cej';
COMMENT ON COLUMN public."KatFirmy".dataakcept IS 'Data akceptu przez idakcept';
COMMENT ON COLUMN public."KatFirmy".lokalizacjapapier IS 'Lokalizacja dokument�w papierowych';



     -- Table: public."KatDzialy"

-- DROP TABLE public."KatDzialy";

CREATE TABLE public."KatDzialy"
(
  id numeric(20,0) NOT NULL, -- Numer ID
  symbol character(20), -- Identyfikator Dzialy : np. TFG, TFNI itp.
  nazwa character(150), -- Nazwa, np. Top Farms G�ubczyce
  datamodify time with time zone, -- Data modyfikacji
  idoper character(30), -- Idetyfikator osoby dokonuj�cej wpisu
  idakcept character(30), -- Identyfikator osoby akceptuj�cej
  dataakcept time with time zone, -- Data akceptu przez idakcept
  CONSTRAINT "KatDzialy_pkey" PRIMARY KEY (id)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE public."KatDzialy"
  OWNER TO postgres;
COMMENT ON COLUMN public."KatDzialy".id IS 'Numer ID';
COMMENT ON COLUMN public."KatDzialy".symbol IS 'Identyfikator Dzialy : np. TFG, TFNI itp.';
COMMENT ON COLUMN public."KatDzialy".nazwa IS 'Nazwa, np. Top Farms G�ubczyce';
COMMENT ON COLUMN public."KatDzialy".datamodify IS 'Data modyfikacji';
COMMENT ON COLUMN public."KatDzialy".idoper IS 'Idetyfikator osoby dokonuj�cej wpisu';
COMMENT ON COLUMN public."KatDzialy".idakcept IS 'Identyfikator osoby akceptuj�cej';
COMMENT ON COLUMN public."KatDzialy".dataakcept IS 'Data akceptu przez idakcept';



-- Table: public."KatDokumentyRodzaj"

-- DROP TABLE public."KatDokumentyRodzaj";

CREATE TABLE public."KatDokumentyRodzaj"
(
  id numeric(10,0) NOT NULL, -- Numer id
  symbol character(20), -- Symbol dokumetu, np. SWPR - �wiadectwo pracy
  nazwa character(254), -- Nazwa dokumentu
  CONSTRAINT "KatDokumentyRodzaj_pkey" PRIMARY KEY (id)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE public."KatDokumentyRodzaj"
  OWNER TO postgres;
COMMENT ON TABLE public."KatDokumentyRodzaj"
  IS 'S�ownik rodzaj�w dokument�w, np. BDL - badania lekarskie, SWPR - �wiadectwa pracy itp.';
COMMENT ON COLUMN public."KatDokumentyRodzaj".id IS 'Numer id';
COMMENT ON COLUMN public."KatDokumentyRodzaj".symbol IS 'Symbol dokumetu, np. SWPR - �wiadectwo pracy';
COMMENT ON COLUMN public."KatDokumentyRodzaj".nazwa IS 'Nazwa dokumentu';


