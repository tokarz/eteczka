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


  CREATE SEQUENCE MiejscePracy_id_seq;

  -- Table: public."KatFirmy"

-- DROP TABLE public."KatFirmy";

CREATE TABLE public."KatFirmy"
(
  firma character(20) NOT NULL, -- Identyfikator firmy : np. TFG, TFNI itp.
  nazwa character varying(300), -- Nazwa, np. Top Farms GĂ‚Ĺ‚ubczyce
  nazwaskrocona character(150), -- Nazwa skrÄ‚Ĺ‚cona firmy uĂ‚ĹĽywana w systemie PĂ‚Ĺ‚atnik
  ulica character(50), -- Ulica w adresie firmy
  numerdomu character(10), -- Numer domu w adresie firmy
  numerlokalu character(10), -- Numer lokalu w adresie firmy
  miasto character(25), -- MiejscowoÄąâ€śÄ‚Â¦ w adresie firmy
  kodpocztowy character(6), -- Kod pocztowy w adresie firmy
  poczta character(25), -- Poczta w adresie firmy
  gmina character(25), -- Gmina w adresie firmy
  powiat character(25), -- Powiat w adresie firmy
  wojewodztwo character(25), -- WojewÄ‚Ĺ‚dztwo w adresie firmy
  nip character(10) NOT NULL, -- Numer NIP firmy
  regon character(20), -- Numer regon firmy
  nazwa2 character varying(300), -- Druga czÄ™Ĺ›Ä‡ nazwy firmy
  pesel character(11), -- Numer pesel wĂ‚Ĺ‚aÄąâ€ściciela firmy, jeĂ‚ĹĽeli firma jest jednoosobowa i jest to osoba fizyczna
  idoper character(30), -- Idetyfikator osoby dokonujĂ‚Ä…cej wpisu
  idakcept character(30), -- Identyfikator osoby akceptujĂ‚Ä…cej
  nazwisko character(30), -- Nazwisko, jeĹĽeli jest to firma prywatna
  imie character(30), -- ImiÄ™ jeĹ›li firma prywatna
  datamodify timestamp without time zone,
  dataakcept timestamp without time zone,
  CONSTRAINT "KatFirmy_pkey" PRIMARY KEY (nip)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE public."KatFirmy"
  OWNER TO postgres;
COMMENT ON COLUMN public."KatFirmy".firma IS 'Identyfikator firmy : np. TFG, TFNI itp.';
COMMENT ON COLUMN public."KatFirmy".nazwa IS 'Nazwa, np. Top Farms GĂ‚Ĺ‚ubczyce';
COMMENT ON COLUMN public."KatFirmy".nazwaskrocona IS 'Nazwa skrÄ‚Ĺ‚cona firmy uĂ‚ĹĽywana w systemie PĂ‚Ĺ‚atnik';
COMMENT ON COLUMN public."KatFirmy".ulica IS 'Ulica w adresie firmy';
COMMENT ON COLUMN public."KatFirmy".numerdomu IS 'Numer domu w adresie firmy';
COMMENT ON COLUMN public."KatFirmy".numerlokalu IS 'Numer lokalu w adresie firmy';
COMMENT ON COLUMN public."KatFirmy".miasto IS 'MiejscowoÄąâ€śÄ‚Â¦ w adresie firmy';
COMMENT ON COLUMN public."KatFirmy".kodpocztowy IS 'Kod pocztowy w adresie firmy';
COMMENT ON COLUMN public."KatFirmy".poczta IS 'Poczta w adresie firmy';
COMMENT ON COLUMN public."KatFirmy".gmina IS 'Gmina w adresie firmy';
COMMENT ON COLUMN public."KatFirmy".powiat IS 'Powiat w adresie firmy';
COMMENT ON COLUMN public."KatFirmy".wojewodztwo IS 'WojewÄ‚Ĺ‚dztwo w adresie firmy';
COMMENT ON COLUMN public."KatFirmy".nip IS 'Numer NIP firmy';
COMMENT ON COLUMN public."KatFirmy".regon IS 'Numer regon firmy';
COMMENT ON COLUMN public."KatFirmy".nazwa2 IS 'Druga czÄ™Ĺ›Ä‡ nazwy firmy';
COMMENT ON COLUMN public."KatFirmy".pesel IS 'Numer pesel wĂ‚Ĺ‚aÄąâ€ściciela firmy, jeĂ‚ĹĽeli firma jest jednoosobowa i jest to osoba fizyczna';
COMMENT ON COLUMN public."KatFirmy".idoper IS 'Idetyfikator osoby dokonujĂ‚Ä…cej wpisu';
COMMENT ON COLUMN public."KatFirmy".idakcept IS 'Identyfikator osoby akceptujĂ‚Ä…cej';
COMMENT ON COLUMN public."KatFirmy".nazwisko IS 'Nazwisko, jeĹĽeli jest to firma prywatna';
COMMENT ON COLUMN public."KatFirmy".imie IS 'ImiÄ™ jeĹ›li firma prywatna';




-- Table: public."MiejscePracy"

-- DROP TABLE public."MiejscePracy";



CREATE TABLE public."MiejscePracy"
(
  firma character(20), -- Symbol firmy
  rejon character(20), -- Symbol rejonu w ramach firmy
  wydzial character(20), -- Symbol dziaĹ‚u
  podwydzial character(20), -- Symbol podwydziaĹ‚u
  konto5 character(20), -- Symbol konta ksiÄ™gowego
  datapocz character(10) NOT NULL, -- Data poczÄ…tkowa w miejscu pracy
  datakoniec character(10), -- Data koĹ„cowa w miejscu pracy
  idoper character(30), -- ID operatora
  idakcept character(30), -- Identyfikator osoby akceptujÄ…cej
  datamodify timestamp without time zone,
  dataakcept timestamp without time zone,
  numeread character(20) NOT NULL,
  id integer NOT NULL DEFAULT nextval('MiejscePracy_id_seq'::regclass),
  CONSTRAINT "MiejscePracy_pkey" PRIMARY KEY (id)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE public."MiejscePracy"
  OWNER TO postgres;
COMMENT ON TABLE public."MiejscePracy"
  IS 'Tabela ';
COMMENT ON COLUMN public."MiejscePracy".firma IS 'Symbol firmy';
COMMENT ON COLUMN public."MiejscePracy".rejon IS 'Symbol rejonu w ramach firmy';
COMMENT ON COLUMN public."MiejscePracy".wydzial IS 'Symbol dziaĹ‚u';
COMMENT ON COLUMN public."MiejscePracy".podwydzial IS 'Symbol podwydziaĹ‚u';
COMMENT ON COLUMN public."MiejscePracy".konto5 IS 'Symbol konta ksiÄ™gowego';
COMMENT ON COLUMN public."MiejscePracy".datapocz IS 'Data poczÄ…tkowa w miejscu pracy';
COMMENT ON COLUMN public."MiejscePracy".datakoniec IS 'Data koĹ„cowa w miejscu pracy';
COMMENT ON COLUMN public."MiejscePracy".idoper IS 'ID operatora';
COMMENT ON COLUMN public."MiejscePracy".idakcept IS 'Identyfikator osoby akceptujÄ…cej';



-- Table: public."KatRejony"

-- DROP TABLE public."KatRejony";

CREATE TABLE public."KatRejony"
(
  rejon character(20) NOT NULL, -- Identyfikator Rejony : np. DZ - DzbaĹ„ce, BG - Bogdanowice itp. w systemie pĹ‚acowym mam dwa znaki ale tu jest 20
  nazwa character(150), -- Nazwa, np. Top Farms GĂ‚Ĺ‚ubczyce
  idoper character(30), -- Idetyfikator osoby dokonujĂ‚Ä…cej wpisu
  idakcept character(30), -- Identyfikator osoby akceptujĂ‚Ä…cej
  firma character(20) NOT NULL, -- Symbol firmy do ktĂłrej naleĹĽy rejon
  datamodify timestamp without time zone,
  dataakcept timestamp without time zone,
  mnemonik character(20), -- Mnemonik, bo rejn nazwywa siÄ™ np. R1 a chcemy ĹĽebu to byĹ‚o bardziej czytelne, np BG - Bogdanowice
  CONSTRAINT "KatRejony_pkey" PRIMARY KEY (firma, rejon)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE public."KatRejony"
  OWNER TO postgres;
COMMENT ON COLUMN public."KatRejony".rejon IS 'Identyfikator Rejony : np. DZ - DzbaĹ„ce, BG - Bogdanowice itp. w systemie pĹ‚acowym mam dwa znaki ale tu jest 20';
COMMENT ON COLUMN public."KatRejony".nazwa IS 'Nazwa, np. Top Farms GĂ‚Ĺ‚ubczyce';
COMMENT ON COLUMN public."KatRejony".idoper IS 'Idetyfikator osoby dokonujĂ‚Ä…cej wpisu';
COMMENT ON COLUMN public."KatRejony".idakcept IS 'Identyfikator osoby akceptujĂ‚Ä…cej';
COMMENT ON COLUMN public."KatRejony".firma IS 'Symbol firmy do ktĂłrej naleĹĽy rejon';
COMMENT ON COLUMN public."KatRejony".mnemonik IS 'Mnemonik, bo rejn nazwywa siÄ™ np. R1 a chcemy ĹĽebu to byĹ‚o bardziej czytelne, np BG - Bogdanowice';



-- Table: public."KatPodWydzial"

-- DROP TABLE public."KatPodWydzial";

CREATE TABLE public."KatPodWydzial"
(
  podwydzial character(20) NOT NULL, -- Identyfikator podwydziaĂ‚Ĺ‚u : np. KDR (Kadry - pĂ‚Ĺ‚ace).
  nazwa character(150), -- Nazwa, np. Rachuba
  wydzial character(20) NOT NULL, -- Identyfikator dziaĂ‚Ĺ‚u nadrzÄ‚Ĺždnego : np. FA (tabela KatDzialy). Podwydzial KDR naleĂ‚ĹĽy do dziaĂ‚Ĺ‚u FA
  datamodify time without time zone, -- Data modyfikacji
  idoper character(30), -- Idetyfikator osoby dokonujĂ‚Ä…cej wpisu
  idakcept character(30), -- Identyfikator osoby akceptujĂ‚Ä…cej
  dataakcept time without time zone, -- Data akceptu przez idakcept
  firma character(20) NOT NULL, -- Symbol firmy, peĹ‚ny klucz to : firma+wydzial+podwydzial
  CONSTRAINT "KatPodWydzial_pkey" PRIMARY KEY (firma, wydzial, podwydzial)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE public."KatPodWydzial"
  OWNER TO postgres;
COMMENT ON COLUMN public."KatPodWydzial".podwydzial IS 'Identyfikator podwydziaĂ‚Ĺ‚u : np. KDR (Kadry - pĂ‚Ĺ‚ace).';
COMMENT ON COLUMN public."KatPodWydzial".nazwa IS 'Nazwa, np. Rachuba';
COMMENT ON COLUMN public."KatPodWydzial".wydzial IS 'Identyfikator dziaĂ‚Ĺ‚u nadrzÄ‚Ĺždnego : np. FA (tabela KatDzialy). Podwydzial KDR naleĂ‚ĹĽy do dziaĂ‚Ĺ‚u FA';
COMMENT ON COLUMN public."KatPodWydzial".datamodify IS 'Data modyfikacji';
COMMENT ON COLUMN public."KatPodWydzial".idoper IS 'Idetyfikator osoby dokonujĂ‚Ä…cej wpisu';
COMMENT ON COLUMN public."KatPodWydzial".idakcept IS 'Identyfikator osoby akceptujĂ‚Ä…cej';
COMMENT ON COLUMN public."KatPodWydzial".dataakcept IS 'Data akceptu przez idakcept';
COMMENT ON COLUMN public."KatPodWydzial".firma IS 'Symbol firmy, peĹ‚ny klucz to : firma+wydzial+podwydzial';




-- Table: public."KatLokalPapier"

-- DROP TABLE public."KatLokalPapier";

CREATE TABLE public."KatLokalPapier"
(
  firma character(20) NOT NULL, -- Identyfikator firmy : np. TFG, TFNI itp. symbol z KatFirmy
  lokalpapier character(20) NOT NULL, -- Symbol archiwum : np. TFG_piwnica
  nazwa character varying(300), -- Nazwa, np. archiwum Top Farms GÄ‚â€šÄąâ€šubczyce
  ulica character(50), -- Ulica gdzie jest archiwum
  numerdomu character(10), -- Numer domu  gdzie jest archiwum
  numerlokalu character(10), -- Numer lokalu  gdzie jest archiwum
  miasto character(25), -- MiejscowoĂ„Ä…Ă˘â‚¬Ĺ›Ă„â€šĂ‚Â¦  gdzie jest archiwum
  kodpocztowy character(6), -- Kod pocztowy  gdzie jest archiwum
  poczta character(25), -- Poczta  gdzie jest archiwum
  idoper character(30), -- Idetyfikator osoby dokonujÄ‚â€šĂ„â€¦cej wpisu
  idakcept character(30), -- Identyfikator osoby akceptujÄ‚â€šĂ„â€¦cej
  datamodify timestamp without time zone,
  dataakcept timestamp without time zone,
  CONSTRAINT "KatLokalPapier_pkey" PRIMARY KEY (firma, lokalpapier)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE public."KatLokalPapier"
  OWNER TO postgres;
COMMENT ON COLUMN public."KatLokalPapier".firma IS 'Identyfikator firmy : np. TFG, TFNI itp. symbol z KatFirmy';
COMMENT ON COLUMN public."KatLokalPapier".lokalpapier IS 'Symbol archiwum : np. TFG_piwnica';
COMMENT ON COLUMN public."KatLokalPapier".nazwa IS 'Nazwa, np. archiwum Top Farms GÄ‚â€šÄąâ€šubczyce';
COMMENT ON COLUMN public."KatLokalPapier".ulica IS 'Ulica gdzie jest archiwum';
COMMENT ON COLUMN public."KatLokalPapier".numerdomu IS 'Numer domu  gdzie jest archiwum';
COMMENT ON COLUMN public."KatLokalPapier".numerlokalu IS 'Numer lokalu  gdzie jest archiwum';
COMMENT ON COLUMN public."KatLokalPapier".miasto IS 'MiejscowoĂ„Ä…Ă˘â‚¬Ĺ›Ă„â€šĂ‚Â¦  gdzie jest archiwum';
COMMENT ON COLUMN public."KatLokalPapier".kodpocztowy IS 'Kod pocztowy  gdzie jest archiwum';
COMMENT ON COLUMN public."KatLokalPapier".poczta IS 'Poczta  gdzie jest archiwum';
COMMENT ON COLUMN public."KatLokalPapier".idoper IS 'Idetyfikator osoby dokonujÄ‚â€šĂ„â€¦cej wpisu';
COMMENT ON COLUMN public."KatLokalPapier".idakcept IS 'Identyfikator osoby akceptujÄ‚â€šĂ„â€¦cej';



-- Table: public."KatKonta5"

-- DROP TABLE public."KatKonta5";

CREATE TABLE public."KatKonta5"
(
  konto5 character(20) NOT NULL, -- Symbol konta ksiÄ™gowego
  nazwa character(150), -- Nazwa, np. Top Farms GĹ‚ubczyce
  idoper character(30), -- Idetyfikator osoby dokonujÄ…cej wpisu
  idakcept character(30), -- Identyfikator osoby akceptujÄ…cej
  firma character(20) NOT NULL, -- Symbol firmy w ktĂłrej zdefiniowano konto
  kontoskr character(20) NOT NULL, -- SkrĂłt konta
  datamodify timestamp without time zone,
  dataakcept timestamp without time zone,
  CONSTRAINT "KatKonta5_pkey" PRIMARY KEY (firma, konto5)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE public."KatKonta5"
  OWNER TO postgres;
COMMENT ON COLUMN public."KatKonta5".konto5 IS 'Symbol konta ksiÄ™gowego';
COMMENT ON COLUMN public."KatKonta5".nazwa IS 'Nazwa, np. Top Farms GĹ‚ubczyce';
COMMENT ON COLUMN public."KatKonta5".idoper IS 'Idetyfikator osoby dokonujÄ…cej wpisu';
COMMENT ON COLUMN public."KatKonta5".idakcept IS 'Identyfikator osoby akceptujÄ…cej';
COMMENT ON COLUMN public."KatKonta5".firma IS 'Symbol firmy w ktĂłrej zdefiniowano konto';
COMMENT ON COLUMN public."KatKonta5".kontoskr IS 'SkrĂłt konta';



-- Table: "KatLoginy"

-- DROP TABLE "KatLoginy";

CREATE TABLE "KatLoginy"
(
  id numeric(20,0) NOT NULL, -- Kolumna ID
  identyfikator character(30), -- Identyfikator - login
  nazwisko character(40), -- Nazwisko uÂżytkownika
  imie character(20), -- ImiĂŞ uÂżytkownika
  hasloshort character(50), -- HasÂło krĂłtkie minimum 6 znakĂłw do potwierdzania szybkiego
  haslolong character(50), -- HasÂło dÂługie minimum 12 znakĂłw do logowania i operacji specjalnych
  rolareadonly boolean, -- Rola - uprawnienia tylko do odczytu
  rolaaddpracownik boolean, -- Rola - uprawnienia do dopisywania i modyfikacji danych pracownika
  rolamodifypracownik boolean, -- Rola - modyfikacja danych pracownika
  rolaaddfile boolean, -- Rola - dodanie pliku do systemu
  rolamodifyfile boolean, -- Rola - modyfikacja opisu pliku juÂż istniejÂącego w systemie
  rolaslowniki boolean, -- Rola - modyfikacja tabel sÂłownikowych
  rolasendmail boolean, -- Rola - uprawnienie do wysÂłania pliku mailem
  rolaraport boolean, -- Rola - uprawnienia do raportĂłw na drukarĂŞ
  rolaraportexport boolean, -- Rola uprawnienia do eksportu raportĂłw np. do xls
  roladoubleakcept boolean, -- Rola - uprawnienia do podwĂłjnego akceptu
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
  IS 'Katalog uÂżytkownikĂłw systemu';
COMMENT ON COLUMN "KatLoginy".id IS 'Kolumna ID';
COMMENT ON COLUMN "KatLoginy".identyfikator IS 'Identyfikator - login';
COMMENT ON COLUMN "KatLoginy".nazwisko IS 'Nazwisko uÂżytkownika';
COMMENT ON COLUMN "KatLoginy".imie IS 'ImiĂŞ uÂżytkownika';
COMMENT ON COLUMN "KatLoginy".hasloshort IS 'HasÂło krĂłtkie minimum 6 znakĂłw do potwierdzania szybkiego';
COMMENT ON COLUMN "KatLoginy".haslolong IS 'HasÂło dÂługie minimum 12 znakĂłw do logowania i operacji specjalnych';
COMMENT ON COLUMN "KatLoginy".rolareadonly IS 'Rola - uprawnienia tylko do odczytu';
COMMENT ON COLUMN "KatLoginy".rolaaddpracownik IS 'Rola - uprawnienia do dopisywania i modyfikacji danych pracownika';
COMMENT ON COLUMN "KatLoginy".rolamodifypracownik IS 'Rola - modyfikacja danych pracownika';
COMMENT ON COLUMN "KatLoginy".rolaaddfile IS 'Rola - dodanie pliku do systemu';
COMMENT ON COLUMN "KatLoginy".rolamodifyfile IS 'Rola - modyfikacja opisu pliku juÂż istniejÂącego w systemie';
COMMENT ON COLUMN "KatLoginy".rolaslowniki IS 'Rola - modyfikacja tabel sÂłownikowych';
COMMENT ON COLUMN "KatLoginy".rolasendmail IS 'Rola - uprawnienie do wysÂłania pliku mailem';
COMMENT ON COLUMN "KatLoginy".rolaraport IS 'Rola - uprawnienia do raportĂłw na drukarĂŞ';
COMMENT ON COLUMN "KatLoginy".rolaraportexport IS 'Rola uprawnienia do eksportu raportĂłw np. do xls';
COMMENT ON COLUMN "KatLoginy".roladoubleakcept IS 'Rola - uprawnienia do podwĂłjnego akceptu';

CREATE TABLE public."KatDokumentyRodzaj"
(
  symbol character(20) NOT NULL, -- Symbol dokumetu, np. SWPR - Äąâ€świadectwo pracy
  nazwa character(254), -- Nazwa dokumentu
  dokwlasny boolean, -- OkreĹ›la czy dokument zostaĹ‚ wytworzony przez nas czy jest to dokument obcy True=wĹ‚asny
  jrwa character(10), -- PeĹ‚na klasyfikacja JRWA
  teczkadzial character(10), -- CzÄ™Ĺ›Ä‡ akt - dozwolone wartoĹ›ci : A,B,C
  typedycji character(2), -- OkreĹ›la pola ktĂłre majÄ… byÄ‡ wymagane w edycji, np data dokumentu, data waĹĽnoĹ›ci itp.
  idoper character(30),
  idakcept character(30),
  datamodify timestamp without time zone,
  dataakcept timestamp without time zone,
  CONSTRAINT "KatDokumentyRodzaj_pkey" PRIMARY KEY (symbol)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE public."KatDokumentyRodzaj"
  OWNER TO postgres;
COMMENT ON TABLE public."KatDokumentyRodzaj"
  IS 'Slownik rodzajÄ‚Ĺ‚w dokumentÄ‚Ĺ‚w, np. BDL - badania lekarskie, SWPR - Äąâ€świadectwa pracy itp.';
COMMENT ON COLUMN public."KatDokumentyRodzaj".symbol IS 'Symbol dokumetu, np. SWPR - Äąâ€świadectwo pracy';
COMMENT ON COLUMN public."KatDokumentyRodzaj".nazwa IS 'Nazwa dokumentu';
COMMENT ON COLUMN public."KatDokumentyRodzaj".dokwlasny IS 'OkreĹ›la czy dokument zostaĹ‚ wytworzony przez nas czy jest to dokument obcy True=wĹ‚asny';
COMMENT ON COLUMN public."KatDokumentyRodzaj".jrwa IS 'PeĹ‚na klasyfikacja JRWA';
COMMENT ON COLUMN public."KatDokumentyRodzaj".teczkadzial IS 'CzÄ™Ĺ›Ä‡ akt - dozwolone wartoĹ›ci : A,B,C';
COMMENT ON COLUMN public."KatDokumentyRodzaj".typedycji IS 'OkreĹ›la pola ktĂłre majÄ… byÄ‡ wymagane w edycji, np data dokumentu, data waĹĽnoĹ›ci itp.';



-- Table: public."KatWydzial"

-- DROP TABLE public."KatWydzial";

CREATE TABLE public."KatWydzial"
(
  wydzial character(20) NOT NULL, -- Identyfikator Dzialu : np. KSG - ksiÄ™gowoĹ›Ä‡, TRAK - traktorzyĹ›ci itp.
  nazwa character(150), -- Nazwa, np. Administracja, TraktorzyĹ›ci itp.
  datamodify time without time zone, -- Data modyfikacji
  idoper character(30), -- Idetyfikator osoby dokonujĂ‚Ä…cej wpisu
  idakcept character(30), -- Identyfikator osoby akceptujĂ‚Ä…cej
  dataakcept time without time zone, -- Data akceptu przez idakcept
  firma character(20) NOT NULL, -- Symbol firmydo ktĂłrej przypisano dziaĹ‚.
  CONSTRAINT "KatWydzial_pkey" PRIMARY KEY (firma, wydzial)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE public."KatWydzial"
  OWNER TO postgres;
COMMENT ON COLUMN public."KatWydzial".wydzial IS 'Identyfikator Dzialu : np. KSG - ksiÄ™gowoĹ›Ä‡, TRAK - traktorzyĹ›ci itp.';
COMMENT ON COLUMN public."KatWydzial".nazwa IS 'Nazwa, np. Administracja, TraktorzyĹ›ci itp.';
COMMENT ON COLUMN public."KatWydzial".datamodify IS 'Data modyfikacji';
COMMENT ON COLUMN public."KatWydzial".idoper IS 'Idetyfikator osoby dokonujĂ‚Ä…cej wpisu';
COMMENT ON COLUMN public."KatWydzial".idakcept IS 'Identyfikator osoby akceptujĂ‚Ä…cej';
COMMENT ON COLUMN public."KatWydzial".dataakcept IS 'Data akceptu przez idakcept';
COMMENT ON COLUMN public."KatWydzial".firma IS 'Symbol firmydo ktĂłrej przypisano dziaĹ‚.';


CREATE TABLE "KatJrwa"
(
  id numeric(20,0) NOT NULL, -- Identyfikator ID
  slklas1 character(1), -- Symbol klasyfikacyjny I
  slklas2 character(2), -- Symbol klasyfikacyjny II
  slklas3 character(3), -- Symbol klasyfikacyjny III
  slklas4 character(4), -- Symbol klasyfikacyjny IV
  kategoria character(5), -- kategoria wedÂług sÂłownika KategorieAkt
  nazwa character(100), -- Opis klasyfikacyjny
  opis character varying(300), -- Opis szczegĂłÂłowy
  idoper character(30), -- Identyfikator osoby dokonujÂącej wpisu
  datamodify timestamp without time zone, -- Data dokonania wpisu
  idakcept character(30), -- Identyfikator osoby akceptujÂącej
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
COMMENT ON COLUMN "KatJrwa".kategoria IS 'kategoria wedÂług sÂłownika KategorieAkt';
COMMENT ON COLUMN "KatJrwa".nazwa IS 'Opis klasyfikacyjny';
COMMENT ON COLUMN "KatJrwa".opis IS 'Opis szczegĂłÂłowy';
COMMENT ON COLUMN "KatJrwa".idoper IS 'Identyfikator osoby dokonujÂącej wpisu';
COMMENT ON COLUMN "KatJrwa".datamodify IS 'Data dokonania wpisu';
COMMENT ON COLUMN "KatJrwa".idakcept IS 'Identyfikator osoby akceptujÂącej';
COMMENT ON COLUMN "KatJrwa".dataakcept IS 'Data akceptu przez idakcept';

       -- Table: "KatKategorieAkt"

-- DROP TABLE "KatKategorieAkt";

CREATE TABLE "KatKategorieAkt"
(
  id numeric(15,0) NOT NULL, -- Numer Id
  symbol character(20), -- kategorie typu A,B,BC,BE + ewentualne cyfry
  nazwa character(100), -- Nazwa kategorii
  idoper character(30), -- Identyfikator osoby dokonujÂącej wpisu
  datamodify timestamp without time zone, -- Data dokonania wpisu
  idakcept character(30), -- Identyfikator osoby akceptujÂącej
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
COMMENT ON COLUMN "KatKategorieAkt".idoper IS 'Identyfikator osoby dokonujÂącej wpisu';
COMMENT ON COLUMN "KatKategorieAkt".datamodify IS 'Data dokonania wpisu';
COMMENT ON COLUMN "KatKategorieAkt".idakcept IS 'Identyfikator osoby akceptujÂącej';
COMMENT ON COLUMN "KatKategorieAkt".dataakcept IS 'Data akceptu przez idakcept';

-- Table: "KatPracownicy"

-- DROP TABLE "KatPracownicy";

CREATE TABLE public."KatPracownicy"
(
  imie character varying(50),
  nazwisko character varying(50),
  pesel character varying(11),
  numeread character(20) NOT NULL, -- ID w e-teczka
  kraj character(5), -- Kraj
  nazwiskorodowe character(50),
  imiematki character(30),
  imieojca character(30),
  peselinny character(20), -- Identyfikator dodatkowy, jeżeli nie ma numeru PESEL
  idoper character(30),
  idakcept character(30),
  datamodify timestamp without time zone,
  dataakcept timestamp without time zone,
  dataurodzenia character(10),
  imie2 character varying(30),
  CONSTRAINT "KatPracownicy_pkey" PRIMARY KEY (numeread)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE public."KatPracownicy"
  OWNER TO postgres;
COMMENT ON COLUMN public."KatPracownicy".numeread IS 'ID w e-teczka';
COMMENT ON COLUMN public."KatPracownicy".kraj IS 'Kraj';
COMMENT ON COLUMN public."KatPracownicy".peselinny IS 'Identyfikator dodatkowy, jeżeli nie ma numeru PESEL';




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

CREATE SEQUENCE Pliki_id_seq;

CREATE TABLE public."Pliki"
(
  symbol character(30),
  dataskanu character(10),
  datadokumentu character(10), -- Data wytworzenia papieru
  datapocz character(10),
  datakoniec character(10),
  nazwapliku character(254),
  pelnasciezka character(254),
  typpliku character(10),
  opisdodatkowy character(254),
  numeread character(20) NOT NULL,
  dokwlasny boolean,
  idoper character(30),
  idakcept character(30),
  datamodify timestamp without time zone,
  dataakcept timestamp without time zone,
  id integer NOT NULL DEFAULT nextval('Pliki_id_seq'::regclass),
  CONSTRAINT "Pliki_pkey" PRIMARY KEY (id)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE public."Pliki"
  OWNER TO postgres;
COMMENT ON COLUMN public."Pliki".datadokumentu IS 'Data wytworzenia papieru';



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
 
