-- Database: "E-Agropin-EAD"

-- DROP DATABASE "E-Agropin-EAD";

CREATE DATABASE "E-Agropin-EAD"
  WITH OWNER = postgres
       ENCODING = 'UTF8'
       TABLESPACE = pg_default
       LC_COLLATE = 'Polish_Poland.1250'
       LC_CTYPE = 'Polish_Poland.1250'
       CONNECTION LIMIT = -1;



  \connect E-Agropin-EAD


  CREATE TABLE "KatDokumentyRodzaj"
(
  id numeric(10,0) NOT NULL, -- Numer id
  symbol character(20), -- Symbol dokumetu, np. SWPR - œwiadectwo pracy
  nazwa character(254), -- Nazwa dokumentu
  CONSTRAINT "KatDokumentyRodzaj_pkey" PRIMARY KEY (id)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE "KatDokumentyRodzaj"
  OWNER TO postgres;
COMMENT ON TABLE "KatDokumentyRodzaj"
  IS 'Slownik rodzajów dokumentów, np. BDL - badania lekarskie, SWPR - œwiadectwa pracy itp.';
COMMENT ON COLUMN "KatDokumentyRodzaj".id IS 'Numer id';
COMMENT ON COLUMN "KatDokumentyRodzaj".symbol IS 'Symbol dokumetu, np. SWPR - œwiadectwo pracy';
COMMENT ON COLUMN "KatDokumentyRodzaj".nazwa IS 'Nazwa dokumentu';

-- Table: "KatDzialy"

-- DROP TABLE "KatDzialy";

CREATE TABLE "KatDzialy"
(
  id numeric(20,0) NOT NULL, -- Numer ID
  symbol character(20), -- Identyfikator Dzialy : np. TFG, TFNI itp.
  nazwa character(150), -- Nazwa, np. Top Farms G³ubczyce
  datamodify time with time zone, -- Data modyfikacji
  idoper character(30), -- Idetyfikator osoby dokonuj¹cej wpisu
  idakcept character(30), -- Identyfikator osoby akceptuj¹cej
  dataakcept time with time zone, -- Data akceptu przez idakcept
  CONSTRAINT "KatDzialy_pkey" PRIMARY KEY (id)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE "KatDzialy"
  OWNER TO postgres;
COMMENT ON COLUMN "KatDzialy".id IS 'Numer ID';
COMMENT ON COLUMN "KatDzialy".symbol IS 'Identyfikator Dzialy : np. TFG, TFNI itp.';
COMMENT ON COLUMN "KatDzialy".nazwa IS 'Nazwa, np. Top Farms G³ubczyce';
COMMENT ON COLUMN "KatDzialy".datamodify IS 'Data modyfikacji';
COMMENT ON COLUMN "KatDzialy".idoper IS 'Idetyfikator osoby dokonuj¹cej wpisu';
COMMENT ON COLUMN "KatDzialy".idakcept IS 'Identyfikator osoby akceptuj¹cej';
COMMENT ON COLUMN "KatDzialy".dataakcept IS 'Data akceptu przez idakcept';

-- Table: "KatFirmy"

-- DROP TABLE "KatFirmy";

CREATE TABLE "KatFirmy"
(
  id numeric(20,0) NOT NULL, -- Numer ID
  symbol character(20), -- Identyfikator firmy : np. TFG, TFNI itp.
  nazwa character varying(300), -- Nazwa, np. Top Farms G³ubczyce
  nazwaskrocona character(150), -- Nazwa skrócona firmy u¿ywana w systemie P³atnik
  ulica character(50), -- Ulica w adresie firmy
  numerdomu character(10), -- Numer domu w adresie firmy
  numerlokalu character(10), -- Numer lokalu w adresie firmy
  miasto character(25), -- Miejscowoœæ w adresie firmy
  kodpocztowy character(6), -- Kod pocztowy w adresie firmy
  poczta character(25), -- Poczta w adresie firmy
  gmina character(25), -- Gmina w adresie firmy
  powiat character(25), -- Powiat w adresie firmy
  wojewodztwo character(25), -- Województwo w adresie firmy
  kraj character(25), -- Kraj w którym jest firma, np. PL lub Polska
  nip character(10), -- Numer NIP firmy
  regon character(20), -- Numer regon firmy
  krs character(25), -- Numer KRS firmy
  pesel character(11), -- Numer pesel w³aœciciela firmy, je¿eli firma jest jednoosobowa i jest to osoba fizyczna
  datamodify time with time zone, -- Data modyfikacji
  idoper character(30), -- Idetyfikator osoby dokonuj¹cej wpisu
  idakcept character(30), -- Identyfikator osoby akceptuj¹cej
  dataakcept time with time zone, -- Data akceptu przez idakcept
  lokalizacjapapier character(20), -- Lokalizacja dokumentów papierowych
  CONSTRAINT "KatFirmy_pkey" PRIMARY KEY (id)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE "KatFirmy"
  OWNER TO postgres;
COMMENT ON COLUMN "KatFirmy".id IS 'Numer ID';
COMMENT ON COLUMN "KatFirmy".symbol IS 'Identyfikator firmy : np. TFG, TFNI itp.';
COMMENT ON COLUMN "KatFirmy".nazwa IS 'Nazwa, np. Top Farms G³ubczyce';
COMMENT ON COLUMN "KatFirmy".nazwaskrocona IS 'Nazwa skrócona firmy u¿ywana w systemie P³atnik';
COMMENT ON COLUMN "KatFirmy".ulica IS 'Ulica w adresie firmy';
COMMENT ON COLUMN "KatFirmy".numerdomu IS 'Numer domu w adresie firmy';
COMMENT ON COLUMN "KatFirmy".numerlokalu IS 'Numer lokalu w adresie firmy';
COMMENT ON COLUMN "KatFirmy".miasto IS 'Miejscowoœæ w adresie firmy';
COMMENT ON COLUMN "KatFirmy".kodpocztowy IS 'Kod pocztowy w adresie firmy';
COMMENT ON COLUMN "KatFirmy".poczta IS 'Poczta w adresie firmy';
COMMENT ON COLUMN "KatFirmy".gmina IS 'Gmina w adresie firmy';
COMMENT ON COLUMN "KatFirmy".powiat IS 'Powiat w adresie firmy';
COMMENT ON COLUMN "KatFirmy".wojewodztwo IS 'Województwo w adresie firmy';
COMMENT ON COLUMN "KatFirmy".kraj IS 'Kraj w którym jest firma, np. PL lub Polska';
COMMENT ON COLUMN "KatFirmy".nip IS 'Numer NIP firmy';
COMMENT ON COLUMN "KatFirmy".regon IS 'Numer regon firmy';
COMMENT ON COLUMN "KatFirmy".krs IS 'Numer KRS firmy';
COMMENT ON COLUMN "KatFirmy".pesel IS 'Numer pesel w³aœciciela firmy, je¿eli firma jest jednoosobowa i jest to osoba fizyczna';
COMMENT ON COLUMN "KatFirmy".datamodify IS 'Data modyfikacji';
COMMENT ON COLUMN "KatFirmy".idoper IS 'Idetyfikator osoby dokonuj¹cej wpisu';
COMMENT ON COLUMN "KatFirmy".idakcept IS 'Identyfikator osoby akceptuj¹cej';
COMMENT ON COLUMN "KatFirmy".dataakcept IS 'Data akceptu przez idakcept';
COMMENT ON COLUMN "KatFirmy".lokalizacjapapier IS 'Lokalizacja dokumentów papierowych';

-- Table: "KatJrwa"

-- DROP TABLE "KatJrwa";

CREATE TABLE "KatJrwa"
(
  id numeric(20,0) NOT NULL, -- Identyfikator ID
  slklas1 character(1), -- Symbol klasyfikacyjny I
  slklas2 character(2), -- Symbol klasyfikacyjny II
  slklas3 character(3), -- Symbol klasyfikacyjny III
  slklas4 character(4), -- Symbol klasyfikacyjny IV
  kategoria character(5), -- kategoria wed³ug s³ownika KategorieAkt
  nazwa character(100), -- Opis klasyfikacyjny
  opis character varying(300), -- Opis szczegó³owy
  idoper character(30), -- Identyfikator osoby dokonuj¹cej wpisu
  datamodify timestamp without time zone, -- Data dokonania wpisu
  idakcept character(30), -- Identyfikator osoby akceptuj¹cej
  dataakcept time with time zone, -- Data akceptu przez idakcept
  CONSTRAINT "KatJrwa_pkey" PRIMARY KEY (id)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE "KatJrwa"
  OWNER TO postgres;
COMMENT ON TABLE "KatJrwa"
  IS 'Jednolity Rzeczowy WykazAkt';
COMMENT ON COLUMN "KatJrwa".id IS 'Identyfikator ID';
COMMENT ON COLUMN "KatJrwa".slklas1 IS 'Symbol klasyfikacyjny I';
COMMENT ON COLUMN "KatJrwa".slklas2 IS 'Symbol klasyfikacyjny II';
COMMENT ON COLUMN "KatJrwa".slklas3 IS 'Symbol klasyfikacyjny III';
COMMENT ON COLUMN "KatJrwa".slklas4 IS 'Symbol klasyfikacyjny IV';
COMMENT ON COLUMN "KatJrwa".kategoria IS 'kategoria wed³ug s³ownika KategorieAkt';
COMMENT ON COLUMN "KatJrwa".nazwa IS 'Opis klasyfikacyjny';
COMMENT ON COLUMN "KatJrwa".opis IS 'Opis szczegó³owy';
COMMENT ON COLUMN "KatJrwa".idoper IS 'Identyfikator osoby dokonuj¹cej wpisu';
COMMENT ON COLUMN "KatJrwa".datamodify IS 'Data dokonania wpisu';
COMMENT ON COLUMN "KatJrwa".idakcept IS 'Identyfikator osoby akceptuj¹cej';
COMMENT ON COLUMN "KatJrwa".dataakcept IS 'Data akceptu przez idakcept';

       -- Table: "KatKategorieAkt"

-- DROP TABLE "KatKategorieAkt";

CREATE TABLE "KatKategorieAkt"
(
  id numeric(15,0) NOT NULL, -- Numer Id
  symbol character(20), -- kategorie typu A,B,BC,BE + ewentualne cyfry
  nazwa character(100), -- Nazwa kategorii
  idoper character(30), -- Identyfikator osoby dokonuj¹cej wpisu
  datamodify timestamp without time zone, -- Data dokonania wpisu
  idakcept character(30), -- Identyfikator osoby akceptuj¹cej
  dataakcept time with time zone, -- Data akceptu przez idakcept
  CONSTRAINT "KatKategorieAkt_pkey" PRIMARY KEY (id)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE "KatKategorieAkt"
  OWNER TO postgres;
COMMENT ON COLUMN "KatKategorieAkt".id IS 'Numer Id';
COMMENT ON COLUMN "KatKategorieAkt".symbol IS 'kategorie typu A,B,BC,BE + ewentualne cyfry';
COMMENT ON COLUMN "KatKategorieAkt".nazwa IS 'Nazwa kategorii';
COMMENT ON COLUMN "KatKategorieAkt".idoper IS 'Identyfikator osoby dokonuj¹cej wpisu';
COMMENT ON COLUMN "KatKategorieAkt".datamodify IS 'Data dokonania wpisu';
COMMENT ON COLUMN "KatKategorieAkt".idakcept IS 'Identyfikator osoby akceptuj¹cej';
COMMENT ON COLUMN "KatKategorieAkt".dataakcept IS 'Data akceptu przez idakcept';

-- Table: "KatLoginy"

-- DROP TABLE "KatLoginy";

CREATE TABLE "KatLoginy"
(
  id numeric(20,0) NOT NULL, -- Kolumna ID
  identyfikator character(30), -- Identyfikator - login
  nazwisko character(40), -- Nazwisko u¿ytkownika
  imie character(20), -- Imiê u¿ytkownika
  hasloshort character(50), -- Has³o krótkie minimum 6 znaków do potwierdzania szybkiego
  haslolong character(50), -- Has³o d³ugie minimum 12 znaków do logowania i operacji specjalnych
  rolareadonly boolean, -- Rola - uprawnienia tylko do odczytu
  rolaaddpracownik boolean, -- Rola - uprawnienia do dopisywania i modyfikacji danych pracownika
  rolamodifypracownik boolean, -- Rola - modyfikacja danych pracownika
  rolaaddfile boolean, -- Rola - dodanie pliku do systemu
  rolamodifyfile boolean, -- Rola - modyfikacja opisu pliku ju¿ istniej¹cego w systemie
  rolaslowniki boolean, -- Rola - modyfikacja tabel s³ownikowych
  rolasendmail boolean, -- Rola - uprawnienie do wys³ania pliku mailem
  rolaraport boolean, -- Rola - uprawnienia do raportów na drukarê
  rolaraportexport boolean, -- Rola uprawnienia do eksportu raportów np. do xls
  roladoubleakcept boolean, -- Rola - uprawnienia do podwójnego akceptu
  datamodify timestamp with time zone,
  firmasymbol character varying(10),
  "isAdmin" boolean,
  CONSTRAINT "KatLoginy_pkey" PRIMARY KEY (id)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE "KatLoginy"
  OWNER TO postgres;
COMMENT ON TABLE "KatLoginy"
  IS 'Katalog u¿ytkowników systemu';
COMMENT ON COLUMN "KatLoginy".id IS 'Kolumna ID';
COMMENT ON COLUMN "KatLoginy".identyfikator IS 'Identyfikator - login';
COMMENT ON COLUMN "KatLoginy".nazwisko IS 'Nazwisko u¿ytkownika';
COMMENT ON COLUMN "KatLoginy".imie IS 'Imiê u¿ytkownika';
COMMENT ON COLUMN "KatLoginy".hasloshort IS 'Has³o krótkie minimum 6 znaków do potwierdzania szybkiego';
COMMENT ON COLUMN "KatLoginy".haslolong IS 'Has³o d³ugie minimum 12 znaków do logowania i operacji specjalnych';
COMMENT ON COLUMN "KatLoginy".rolareadonly IS 'Rola - uprawnienia tylko do odczytu';
COMMENT ON COLUMN "KatLoginy".rolaaddpracownik IS 'Rola - uprawnienia do dopisywania i modyfikacji danych pracownika';
COMMENT ON COLUMN "KatLoginy".rolamodifypracownik IS 'Rola - modyfikacja danych pracownika';
COMMENT ON COLUMN "KatLoginy".rolaaddfile IS 'Rola - dodanie pliku do systemu';
COMMENT ON COLUMN "KatLoginy".rolamodifyfile IS 'Rola - modyfikacja opisu pliku ju¿ istniej¹cego w systemie';
COMMENT ON COLUMN "KatLoginy".rolaslowniki IS 'Rola - modyfikacja tabel s³ownikowych';
COMMENT ON COLUMN "KatLoginy".rolasendmail IS 'Rola - uprawnienie do wys³ania pliku mailem';
COMMENT ON COLUMN "KatLoginy".rolaraport IS 'Rola - uprawnienia do raportów na drukarê';
COMMENT ON COLUMN "KatLoginy".rolaraportexport IS 'Rola uprawnienia do eksportu raportów np. do xls';
COMMENT ON COLUMN "KatLoginy".roladoubleakcept IS 'Rola - uprawnienia do podwójnego akceptu';



-- Table: "KatLokalPapier"

-- DROP TABLE "KatLokalPapier";

CREATE TABLE "KatLokalPapier"
(
  id numeric(20,0) NOT NULL, -- Numer ID
  symbolfirma character(20), -- Identyfikator firmy : np. TFG, TFNI itp. symbol z KatFirmy
  symbol character(20), -- Symbol archiwum : np. TFG_piwnica
  nazwa character varying(300), -- Nazwa, np. archiwum Top Farms G³ubczyce
  ulica character(50), -- Ulica gdzie jest archiwum
  numerdomu character(10), -- Numer domu  gdzie jest archiwum
  numerlokalu character(10), -- Numer lokalu  gdzie jest archiwum
  miasto character(25), -- Miejscowoœæ  gdzie jest archiwum
  kodpocztowy character(6), -- Kod pocztowy  gdzie jest archiwum
  poczta character(25), -- Poczta  gdzie jest archiwum
  datamodify time with time zone, -- Data modyfikacji
  idoper character(30), -- Idetyfikator osoby dokonuj¹cej wpisu
  idakcept character(30), -- Identyfikator osoby akceptuj¹cej
  dataakcept time with time zone, -- Data akceptu przez idakcept
  CONSTRAINT "KatLokalPapier_pkey" PRIMARY KEY (id)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE "KatLokalPapier"
  OWNER TO postgres;
COMMENT ON COLUMN "KatLokalPapier".id IS 'Numer ID';
COMMENT ON COLUMN "KatLokalPapier".symbolfirma IS 'Identyfikator firmy : np. TFG, TFNI itp. symbol z KatFirmy';
COMMENT ON COLUMN "KatLokalPapier".symbol IS 'Symbol archiwum : np. TFG_piwnica';
COMMENT ON COLUMN "KatLokalPapier".nazwa IS 'Nazwa, np. archiwum Top Farms G³ubczyce';
COMMENT ON COLUMN "KatLokalPapier".ulica IS 'Ulica gdzie jest archiwum';
COMMENT ON COLUMN "KatLokalPapier".numerdomu IS 'Numer domu  gdzie jest archiwum';
COMMENT ON COLUMN "KatLokalPapier".numerlokalu IS 'Numer lokalu  gdzie jest archiwum';
COMMENT ON COLUMN "KatLokalPapier".miasto IS 'Miejscowoœæ  gdzie jest archiwum';
COMMENT ON COLUMN "KatLokalPapier".kodpocztowy IS 'Kod pocztowy  gdzie jest archiwum';
COMMENT ON COLUMN "KatLokalPapier".poczta IS 'Poczta  gdzie jest archiwum';
COMMENT ON COLUMN "KatLokalPapier".datamodify IS 'Data modyfikacji';
COMMENT ON COLUMN "KatLokalPapier".idoper IS 'Idetyfikator osoby dokonuj¹cej wpisu';
COMMENT ON COLUMN "KatLokalPapier".idakcept IS 'Identyfikator osoby akceptuj¹cej';
COMMENT ON COLUMN "KatLokalPapier".dataakcept IS 'Data akceptu przez idakcept';

-- Table: "KatPodWydzialy"

-- DROP TABLE "KatPodWydzialy";

CREATE TABLE "KatPodWydzialy"
(
  id numeric(20,0) NOT NULL, -- Numer ID
  symbol character(20), -- Identyfikator podwydzia³u : np. KDR (Kadry - p³ace).
  nazwa character(150), -- Nazwa, np. Top Farms G³ubczyce
  symboldzialy character(20), -- Identyfikator dzia³u nadrzêdnego : np. FA (tabela KatDzialy). Podwydzial KDR nale¿y do dzia³u FA
  datamodify time with time zone, -- Data modyfikacji
  idoper character(30), -- Idetyfikator osoby dokonuj¹cej wpisu
  idakcept character(30), -- Identyfikator osoby akceptuj¹cej
  dataakcept time with time zone, -- Data akceptu przez idakcept
  CONSTRAINT "KatPodDzialy_pkey" PRIMARY KEY (id)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE "KatPodWydzialy"
  OWNER TO postgres;
COMMENT ON COLUMN "KatPodWydzialy".id IS 'Numer ID';
COMMENT ON COLUMN "KatPodWydzialy".symbol IS 'Identyfikator podwydzia³u : np. KDR (Kadry - p³ace).';
COMMENT ON COLUMN "KatPodWydzialy".nazwa IS 'Nazwa, np. Top Farms G³ubczyce';
COMMENT ON COLUMN "KatPodWydzialy".symboldzialy IS 'Identyfikator dzia³u nadrzêdnego : np. FA (tabela KatDzialy). Podwydzial KDR nale¿y do dzia³u FA';
COMMENT ON COLUMN "KatPodWydzialy".datamodify IS 'Data modyfikacji';
COMMENT ON COLUMN "KatPodWydzialy".idoper IS 'Idetyfikator osoby dokonuj¹cej wpisu';
COMMENT ON COLUMN "KatPodWydzialy".idakcept IS 'Identyfikator osoby akceptuj¹cej';
COMMENT ON COLUMN "KatPodWydzialy".dataakcept IS 'Data akceptu przez idakcept';

-- Table: "KatPracownicy"

-- DROP TABLE "KatPracownicy";

CREATE TABLE "KatPracownicy"
(
  id numeric,
  imie character varying(50),
  nazwisko character varying(50),
  pesel character varying(11),
  dzialid numeric,
  data_urodzenia date,
  numerpracownika character varying
)
WITH (
  OIDS=FALSE
);
ALTER TABLE "KatPracownicy"
  OWNER TO postgres;
-- Table: "KatRejony"

-- DROP TABLE "KatRejony";

CREATE TABLE "KatRejony"
(
  id numeric(20,0) NOT NULL, -- Numer ID
  symbol character(20), -- Identyfikator Rejony : np. TFG, TFNI itp.
  nazwa character(150), -- Nazwa, np. Top Farms G³ubczyce
  idoper character(30), -- Idetyfikator osoby dokonuj¹cej wpisu
  idakcept character(30), -- Identyfikator osoby akceptuj¹cej
  dataakcept time with time zone, -- Data akceptu przez idakcept
  lokalizacjapapier character(20), -- Lokalizacja dokumentów papierowych
  datamodify timestamp with time zone,
  firmaid numeric,
  CONSTRAINT "KatRejony_pkey" PRIMARY KEY (id),
  CONSTRAINT firma FOREIGN KEY (firmaid)
      REFERENCES "KatFirmy" (id) MATCH SIMPLE
      ON UPDATE NO ACTION ON DELETE NO ACTION
)
WITH (
  OIDS=FALSE
);
ALTER TABLE "KatRejony"
  OWNER TO postgres;
COMMENT ON COLUMN "KatRejony".id IS 'Numer ID';
COMMENT ON COLUMN "KatRejony".symbol IS 'Identyfikator Rejony : np. TFG, TFNI itp.';
COMMENT ON COLUMN "KatRejony".nazwa IS 'Nazwa, np. Top Farms G³ubczyce';
COMMENT ON COLUMN "KatRejony".idoper IS 'Idetyfikator osoby dokonuj¹cej wpisu';
COMMENT ON COLUMN "KatRejony".idakcept IS 'Identyfikator osoby akceptuj¹cej';
COMMENT ON COLUMN "KatRejony".dataakcept IS 'Data akceptu przez idakcept';
COMMENT ON COLUMN "KatRejony".lokalizacjapapier IS 'Lokalizacja dokumentów papierowych';

-- Table: "KatTeczki"

-- DROP TABLE "KatTeczki";

CREATE TABLE "KatTeczki"
(
  id numeric,
  nazwa character varying(50),
  pelna_sciezka character varying(500),
  jrwa character varying(20),
  jrwa_id numeric,
  data_utworzenia timestamp with time zone,
  data_modyfikacji timestamp with time zone,
  typid numeric,
  pesel character varying(11)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE "KatTeczki"
  OWNER TO postgres;

  -- Table: "Pliki"

-- DROP TABLE "Pliki";

CREATE TABLE "Pliki"
(
  id numeric,
  nazwa character varying(50),
  rozszerzenie character varying(10),
  datautworzenia timestamp with time zone,
  datamodyfikacji timestamp with time zone,
  fizycznalokalizacja character varying(255),
  typid numeric,
  jrwa character varying(10)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE "Pliki"
  OWNER TO postgres;
