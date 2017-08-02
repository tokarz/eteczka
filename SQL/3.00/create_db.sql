-- Database: "E-Agropin-EAD"

DROP DATABASE IF EXISTS "E-Agropin-EAD";

CREATE DATABASE "E-Agropin-EAD"
  WITH OWNER = postgres
       ENCODING = 'UTF8'
       TABLESPACE = pg_default
       LC_COLLATE = 'Polish_Poland.1250'
       LC_CTYPE = 'Polish_Poland.1250'
       CONNECTION LIMIT = -1;



  \connect E-Agropin-EAD


  -- Table: public."KatFirmy"

-- DROP TABLE public."KatFirmy";

CREATE TABLE public."KatFirmy"
(
  firma character(20) NOT NULL, -- Identyfikator firmy : np. TFG, TFNI itp.
  nazwa character varying(300), -- Nazwa, np. Top Farms GÂ³ubczyce
  nazwaskrocona character(150), -- Nazwa skrÃ³cona firmy uÂ¿ywana w systemie PÂ³atnik
  ulica character(50), -- Ulica w adresie firmy
  numerdomu character(10), -- Numer domu w adresie firmy
  numerlokalu character(10), -- Numer lokalu w adresie firmy
  miasto character(25), -- MiejscowoÅ“Ã¦ w adresie firmy
  kodpocztowy character(6), -- Kod pocztowy w adresie firmy
  poczta character(25), -- Poczta w adresie firmy
  gmina character(25), -- Gmina w adresie firmy
  powiat character(25), -- Powiat w adresie firmy
  wojewodztwo character(25), -- WojewÃ³dztwo w adresie firmy
  kraj character(25), -- Kraj w ktÃ³rym jest firma, np. PL lub Polska
  nip character(10), -- Numer NIP firmy
  regon character(20), -- Numer regon firmy
  krs character(25), -- Numer KRS firmy
  pesel character(11), -- Numer pesel wÂ³aÅ“ciciela firmy, jeÂ¿eli firma jest jednoosobowa i jest to osoba fizyczna
  datamodify timestamp without time zone, -- Data modyfikacji
  idoper character(30), -- Idetyfikator osoby dokonujÂ¹cej wpisu
  idakcept character(30), -- Identyfikator osoby akceptujÂ¹cej
  dataakcept timestamp without time zone, -- Data akceptu przez idakcept
  nazwisko character(30), -- Nazwisko, je¿eli jest to firma prywatna
  imie character(30), -- Imiê jeœli firma prywatna
  CONSTRAINT firma PRIMARY KEY (firma)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE public."KatFirmy"
  OWNER TO postgres;
COMMENT ON COLUMN public."KatFirmy".firma IS 'Identyfikator firmy : np. TFG, TFNI itp.';
COMMENT ON COLUMN public."KatFirmy".nazwa IS 'Nazwa, np. Top Farms GÂ³ubczyce';
COMMENT ON COLUMN public."KatFirmy".nazwaskrocona IS 'Nazwa skrÃ³cona firmy uÂ¿ywana w systemie PÂ³atnik';
COMMENT ON COLUMN public."KatFirmy".ulica IS 'Ulica w adresie firmy';
COMMENT ON COLUMN public."KatFirmy".numerdomu IS 'Numer domu w adresie firmy';
COMMENT ON COLUMN public."KatFirmy".numerlokalu IS 'Numer lokalu w adresie firmy';
COMMENT ON COLUMN public."KatFirmy".miasto IS 'MiejscowoÅ“Ã¦ w adresie firmy';
COMMENT ON COLUMN public."KatFirmy".kodpocztowy IS 'Kod pocztowy w adresie firmy';
COMMENT ON COLUMN public."KatFirmy".poczta IS 'Poczta w adresie firmy';
COMMENT ON COLUMN public."KatFirmy".gmina IS 'Gmina w adresie firmy';
COMMENT ON COLUMN public."KatFirmy".powiat IS 'Powiat w adresie firmy';
COMMENT ON COLUMN public."KatFirmy".wojewodztwo IS 'WojewÃ³dztwo w adresie firmy';
COMMENT ON COLUMN public."KatFirmy".kraj IS 'Kraj w ktÃ³rym jest firma, np. PL lub Polska';
COMMENT ON COLUMN public."KatFirmy".nip IS 'Numer NIP firmy';
COMMENT ON COLUMN public."KatFirmy".regon IS 'Numer regon firmy';
COMMENT ON COLUMN public."KatFirmy".krs IS 'Numer KRS firmy';
COMMENT ON COLUMN public."KatFirmy".pesel IS 'Numer pesel wÂ³aÅ“ciciela firmy, jeÂ¿eli firma jest jednoosobowa i jest to osoba fizyczna';
COMMENT ON COLUMN public."KatFirmy".datamodify IS 'Data modyfikacji';
COMMENT ON COLUMN public."KatFirmy".idoper IS 'Idetyfikator osoby dokonujÂ¹cej wpisu';
COMMENT ON COLUMN public."KatFirmy".idakcept IS 'Identyfikator osoby akceptujÂ¹cej';
COMMENT ON COLUMN public."KatFirmy".dataakcept IS 'Data akceptu przez idakcept';
COMMENT ON COLUMN public."KatFirmy".nazwisko IS 'Nazwisko, je¿eli jest to firma prywatna';
COMMENT ON COLUMN public."KatFirmy".imie IS 'Imiê jeœli firma prywatna';


-- Table: public."MiejscePracy"

-- DROP TABLE public."MiejscePracy";

CREATE TABLE public."MiejscePracy"
(
  pesel character(11) NOT NULL, -- Pesel pracownika
  firma character(20), -- Symbol firmy
  rejon character(20), -- Symbol rejonu w ramach firmy
  wydzial character(20), -- Symbol dzia³u
  podwydzial character(20), -- Symbol podwydzia³u
  konto5 character(20), -- Symbol konta ksiêgowego
  datapocz character(10) NOT NULL, -- Data pocz¹tkowa w miejscu pracy
  datakoniec character(10), -- Data koñcowa w miejscu pracy
  idoper character(30), -- ID operatora
  datamodify timestamp without time zone, -- Data operacji
  idakcept character(30), -- Identyfikator osoby akceptuj¹cej
  dataakcept timestamp without time zone, -- Data akceptu
  CONSTRAINT "MiejscePracy_pkey" PRIMARY KEY (pesel, datapocz)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE public."MiejscePracy"
  OWNER TO postgres;
COMMENT ON TABLE public."MiejscePracy"
  IS 'Tabela ';
COMMENT ON COLUMN public."MiejscePracy".pesel IS 'Pesel pracownika';
COMMENT ON COLUMN public."MiejscePracy".firma IS 'Symbol firmy';
COMMENT ON COLUMN public."MiejscePracy".rejon IS 'Symbol rejonu w ramach firmy';
COMMENT ON COLUMN public."MiejscePracy".wydzial IS 'Symbol dzia³u';
COMMENT ON COLUMN public."MiejscePracy".podwydzial IS 'Symbol podwydzia³u';
COMMENT ON COLUMN public."MiejscePracy".konto5 IS 'Symbol konta ksiêgowego';
COMMENT ON COLUMN public."MiejscePracy".datapocz IS 'Data pocz¹tkowa w miejscu pracy';
COMMENT ON COLUMN public."MiejscePracy".datakoniec IS 'Data koñcowa w miejscu pracy';
COMMENT ON COLUMN public."MiejscePracy".idoper IS 'ID operatora';
COMMENT ON COLUMN public."MiejscePracy".datamodify IS 'Data operacji';
COMMENT ON COLUMN public."MiejscePracy".idakcept IS 'Identyfikator osoby akceptuj¹cej';
COMMENT ON COLUMN public."MiejscePracy".dataakcept IS 'Data akceptu';

-- Table: public."KatWydzial"

-- DROP TABLE public."KatWydzial";

CREATE TABLE public."KatWydzial"
(
  wydzial character(20) NOT NULL, -- Identyfikator Dzialu : np. KSG - ksiêgowoœæ, TRAK - traktorzyœci itp.
  nazwa character(150), -- Nazwa, np. Administracja, Traktorzyœci itp.
  datamodify timestamp without time zone, -- Data modyfikacji
  idoper character(30), -- Idetyfikator osoby dokonujÂ¹cej wpisu
  idakcept character(30), -- Identyfikator osoby akceptujÂ¹cej
  dataakcept timestamp without time zone, -- Data akceptu przez idakcept
  firma character(20) NOT NULL, -- Symbol firmydo której przypisano dzia³.
  CONSTRAINT firmawydzial PRIMARY KEY (firma, wydzial)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE public."KatWydzial"
  OWNER TO postgres;
COMMENT ON COLUMN public."KatWydzial".wydzial IS 'Identyfikator Dzialu : np. KSG - ksiêgowoœæ, TRAK - traktorzyœci itp.';
COMMENT ON COLUMN public."KatWydzial".nazwa IS 'Nazwa, np. Administracja, Traktorzyœci itp.';
COMMENT ON COLUMN public."KatWydzial".datamodify IS 'Data modyfikacji';
COMMENT ON COLUMN public."KatWydzial".idoper IS 'Idetyfikator osoby dokonujÂ¹cej wpisu';
COMMENT ON COLUMN public."KatWydzial".idakcept IS 'Identyfikator osoby akceptujÂ¹cej';
COMMENT ON COLUMN public."KatWydzial".dataakcept IS 'Data akceptu przez idakcept';
COMMENT ON COLUMN public."KatWydzial".firma IS 'Symbol firmydo której przypisano dzia³.';

-- Table: public."KatRejony"

-- DROP TABLE public."KatRejony";

CREATE TABLE public."KatRejony"
(
  rejon character(20) NOT NULL, -- Identyfikator Rejony : np. DZ - Dzbañce, BG - Bogdanowice itp. w systemie p³acowym mam dwa znaki ale tu jest 20
  nazwa character(150), -- Nazwa, np. Top Farms GÂ³ubczyce
  idoper character(30), -- Idetyfikator osoby dokonujÂ¹cej wpisu
  idakcept character(30), -- Identyfikator osoby akceptujÂ¹cej
  dataakcept timestamp without time zone, -- Data akceptu przez idakcept
  datamodify timestamp without time zone,
  firma character(20) NOT NULL, -- Symbol firmy do której nale¿y rejon
  CONSTRAINT firmarejon PRIMARY KEY (firma, rejon)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE public."KatRejony"
  OWNER TO postgres;
COMMENT ON COLUMN public."KatRejony".rejon IS 'Identyfikator Rejony : np. DZ - Dzbañce, BG - Bogdanowice itp. w systemie p³acowym mam dwa znaki ale tu jest 20';
COMMENT ON COLUMN public."KatRejony".nazwa IS 'Nazwa, np. Top Farms GÂ³ubczyce';
COMMENT ON COLUMN public."KatRejony".idoper IS 'Idetyfikator osoby dokonujÂ¹cej wpisu';
COMMENT ON COLUMN public."KatRejony".idakcept IS 'Identyfikator osoby akceptujÂ¹cej';
COMMENT ON COLUMN public."KatRejony".dataakcept IS 'Data akceptu przez idakcept';
COMMENT ON COLUMN public."KatRejony".firma IS 'Symbol firmy do której nale¿y rejon';


-- Table: public."KatPodWydzial"

-- DROP TABLE public."KatPodWydzial";

CREATE TABLE public."KatPodWydzial"
(
  podwydzial character(20) NOT NULL, -- Identyfikator podwydziaÂ³u : np. KDR (Kadry - pÂ³ace).
  nazwa character(150), -- Nazwa, np. Rachuba
  wydzial character(20) NOT NULL, -- Identyfikator dziaÂ³u nadrzÃªdnego : np. FA (tabela KatDzialy). Podwydzial KDR naleÂ¿y do dziaÂ³u FA
  datamodify timestamp without time zone, -- Data modyfikacji
  idoper character(30), -- Idetyfikator osoby dokonujÂ¹cej wpisu
  idakcept character(30), -- Identyfikator osoby akceptujÂ¹cej
  dataakcept timestamp without time zone, -- Data akceptu przez idakcept
  firma character(20) NOT NULL, -- Symbol firmy, pe³ny klucz to : firma+wydzial+podwydzial
  CONSTRAINT frimawydzialpodwydzial PRIMARY KEY (firma, wydzial, podwydzial)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE public."KatPodWydzial"
  OWNER TO postgres;
COMMENT ON COLUMN public."KatPodWydzial".podwydzial IS 'Identyfikator podwydziaÂ³u : np. KDR (Kadry - pÂ³ace).';
COMMENT ON COLUMN public."KatPodWydzial".nazwa IS 'Nazwa, np. Rachuba';
COMMENT ON COLUMN public."KatPodWydzial".wydzial IS 'Identyfikator dziaÂ³u nadrzÃªdnego : np. FA (tabela KatDzialy). Podwydzial KDR naleÂ¿y do dziaÂ³u FA';
COMMENT ON COLUMN public."KatPodWydzial".datamodify IS 'Data modyfikacji';
COMMENT ON COLUMN public."KatPodWydzial".idoper IS 'Idetyfikator osoby dokonujÂ¹cej wpisu';
COMMENT ON COLUMN public."KatPodWydzial".idakcept IS 'Identyfikator osoby akceptujÂ¹cej';
COMMENT ON COLUMN public."KatPodWydzial".dataakcept IS 'Data akceptu przez idakcept';
COMMENT ON COLUMN public."KatPodWydzial".firma IS 'Symbol firmy, pe³ny klucz to : firma+wydzial+podwydzial';


-- Table: public."KatLokalPapier"

-- DROP TABLE public."KatLokalPapier";

CREATE TABLE public."KatLokalPapier"
(
  firma character(20) NOT NULL, -- Identyfikator firmy : np. TFG, TFNI itp. symbol z KatFirmy
  lokalpapier character(20) NOT NULL, -- Symbol archiwum : np. TFG_piwnica
  nazwa character varying(300), -- Nazwa, np. archiwum Top Farms GÂłubczyce
  ulica character(50), -- Ulica gdzie jest archiwum
  numerdomu character(10), -- Numer domu  gdzie jest archiwum
  numerlokalu character(10), -- Numer lokalu  gdzie jest archiwum
  miasto character(25), -- MiejscowoĹ“Ă¦  gdzie jest archiwum
  kodpocztowy character(6), -- Kod pocztowy  gdzie jest archiwum
  poczta character(25), -- Poczta  gdzie jest archiwum
  datamodify timestamp without time zone, -- Data modyfikacji
  idoper character(30), -- Idetyfikator osoby dokonujÂącej wpisu
  idakcept character(30), -- Identyfikator osoby akceptujÂącej
  dataakcept timestamp without time zone, -- Data akceptu przez idakcept
  CONSTRAINT "KatLokalPapier_pkey" PRIMARY KEY (firma, lokalpapier)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE public."KatLokalPapier"
  OWNER TO postgres;
COMMENT ON COLUMN public."KatLokalPapier".firma IS 'Identyfikator firmy : np. TFG, TFNI itp. symbol z KatFirmy';
COMMENT ON COLUMN public."KatLokalPapier".lokalpapier IS 'Symbol archiwum : np. TFG_piwnica';
COMMENT ON COLUMN public."KatLokalPapier".nazwa IS 'Nazwa, np. archiwum Top Farms GÂłubczyce';
COMMENT ON COLUMN public."KatLokalPapier".ulica IS 'Ulica gdzie jest archiwum';
COMMENT ON COLUMN public."KatLokalPapier".numerdomu IS 'Numer domu  gdzie jest archiwum';
COMMENT ON COLUMN public."KatLokalPapier".numerlokalu IS 'Numer lokalu  gdzie jest archiwum';
COMMENT ON COLUMN public."KatLokalPapier".miasto IS 'MiejscowoĹ“Ă¦  gdzie jest archiwum';
COMMENT ON COLUMN public."KatLokalPapier".kodpocztowy IS 'Kod pocztowy  gdzie jest archiwum';
COMMENT ON COLUMN public."KatLokalPapier".poczta IS 'Poczta  gdzie jest archiwum';
COMMENT ON COLUMN public."KatLokalPapier".datamodify IS 'Data modyfikacji';
COMMENT ON COLUMN public."KatLokalPapier".idoper IS 'Idetyfikator osoby dokonujÂącej wpisu';
COMMENT ON COLUMN public."KatLokalPapier".idakcept IS 'Identyfikator osoby akceptujÂącej';
COMMENT ON COLUMN public."KatLokalPapier".dataakcept IS 'Data akceptu przez idakcept';


-- Table: public."KatKonta5"

-- DROP TABLE public."KatKonta5";

CREATE TABLE public."KatKonta5"
(
  konto5 character(20) NOT NULL, -- Symbol konta ksiêgowego
  nazwa character(150), -- Nazwa, np. Top Farms G³ubczyce
  datamodify timestamp without time zone, -- Data modyfikacji
  idoper character(30), -- Idetyfikator osoby dokonuj¹cej wpisu
  idakcept character(30), -- Identyfikator osoby akceptuj¹cej
  dataakcept timestamp without time zone, -- Data akceptu przez idakcept
  firma character(20) NOT NULL, -- Symbol firmy w której zdefiniowano konto
  CONSTRAINT firmakonto PRIMARY KEY (firma, konto5)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE public."KatKonta5"
  OWNER TO postgres;
COMMENT ON COLUMN public."KatKonta5".konto5 IS 'Symbol konta ksiêgowego';
COMMENT ON COLUMN public."KatKonta5".nazwa IS 'Nazwa, np. Top Farms G³ubczyce';
COMMENT ON COLUMN public."KatKonta5".datamodify IS 'Data modyfikacji';
COMMENT ON COLUMN public."KatKonta5".idoper IS 'Idetyfikator osoby dokonuj¹cej wpisu';
COMMENT ON COLUMN public."KatKonta5".idakcept IS 'Identyfikator osoby akceptuj¹cej';
COMMENT ON COLUMN public."KatKonta5".dataakcept IS 'Data akceptu przez idakcept';
COMMENT ON COLUMN public."KatKonta5".firma IS 'Symbol firmy w której zdefiniowano konto';


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
  isadmin boolean,
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

-- INSERT STATEMENTS

INSERT INTO "KatLoginy"(
            id, identyfikator, nazwisko, imie, hasloshort, haslolong, rolareadonly,
            rolaaddpracownik, rolamodifypracownik, rolaaddfile, rolamodifyfile,
            rolaslowniki, rolasendmail, rolaraport, rolaraportexport, roladoubleakcept,
            datamodify, firmasymbol, isadmin)
    VALUES (0, 'Administrator', 'Krzysztof', 'Jarzyna', 'admin', 'adminadmin', false,
            false, false, false, false,
            false, false, false, false, false,
            '2017-05-02 13:44:00', 'AFM1', true);

INSERT INTO "KatLoginy"(
            id, identyfikator, nazwisko, imie, hasloshort, haslolong, rolareadonly,
            rolaaddpracownik, rolamodifypracownik, rolaaddfile, rolamodifyfile,
            rolaslowniki, rolasendmail, rolaraport, rolaraportexport, roladoubleakcept,
            datamodify, firmasymbol, isadmin)
    VALUES (1, 'Z.Tokarz', 'Zbigniew', 'Tokarz', 'ksglmp', 'ksglmpksglmp', false,
            false, false, false, false,
            false, false, false, false, false,
            '2017-05-02 13:44:00', 'AFM2', false);

INSERT INTO "KatLoginy"(
            id, identyfikator, nazwisko, imie, hasloshort, haslolong, rolareadonly,
            rolaaddpracownik, rolamodifypracownik, rolaaddfile, rolamodifyfile,
            rolaslowniki, rolasendmail, rolaraport, rolaraportexport, roladoubleakcept,
            datamodify, firmasymbol, isadmin)
    VALUES (2, 'M.Tokarz', 'Maciej', 'Tokarz', 'haslo', 'haslohaslo', false,
            false, false, false, false,
            false, false, false, false, false,
            '2017-05-02 13:44:00', 'AFM3', false);

INSERT INTO "KatLoginy"(
            id, identyfikator, nazwisko, imie, hasloshort, haslolong, rolareadonly,
            rolaaddpracownik, rolamodifypracownik, rolaaddfile, rolamodifyfile,
            rolaslowniki, rolasendmail, rolaraport, rolaraportexport, roladoubleakcept,
            datamodify, firmasymbol, isadmin)
    VALUES (3, 'A.Tokarz', 'Aleksandra', 'Tokarz', 'haslo', 'haslohaslo', false,
            false, false, false, false,
            false, false, false, false, false,
            '2017-05-02 13:44:00', 'AFM4', false);


INSERT INTO "KatLoginy"(
            id, identyfikator, nazwisko, imie, hasloshort, haslolong, rolareadonly,
            rolaaddpracownik, rolamodifypracownik, rolaaddfile, rolamodifyfile,
            rolaslowniki, rolasendmail, rolaraport, rolaraportexport, roladoubleakcept,
            datamodify, firmasymbol, isadmin)
    VALUES (4, 'M.Skalacki', 'Michal', 'Skalacki', 'haslo', 'haslohaslo', false,
            false, false, false, false,
            false, false, false, false, false,
            '2017-05-02 13:44:00', 'AFM5', false);