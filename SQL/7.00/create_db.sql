-- Database: "E-Agropin-EAD"
-- DROP DATABASE IF EXISTS "E-Agropin-EAD"; 

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
  nazwa character varying(300), -- Nazwa, np. Top Farms GÂłubczyce
  nazwaskrocona character(150), -- Nazwa skrĂłcona firmy uÂżywana w systemie PÂłatnik
  ulica character(50), -- Ulica w adresie firmy
  numerdomu character(10), -- Numer domu w adresie firmy
  numerlokalu character(10), -- Numer lokalu w adresie firmy
  miasto character(25), -- MiejscowoĹ“Ă¦ w adresie firmy
  kodpocztowy character(6), -- Kod pocztowy w adresie firmy
  poczta character(25), -- Poczta w adresie firmy
  gmina character(25), -- Gmina w adresie firmy
  powiat character(25), -- Powiat w adresie firmy
  wojewodztwo character(25), -- WojewĂłdztwo w adresie firmy
  nip character(10) NOT NULL, -- Numer NIP firmy
  regon character(20), -- Numer regon firmy
  nazwa2 character varying(300), -- Druga część nazwy firmy
  pesel character(11), -- Numer pesel wÂłaĹ“ciciela firmy, jeÂżeli firma jest jednoosobowa i jest to osoba fizyczna
  idoper character(30), -- Idetyfikator osoby dokonujÂącej wpisu
  idakcept character(30), -- Identyfikator osoby akceptujÂącej
  nazwisko character(30), -- Nazwisko, jeżeli jest to firma prywatna
  imie character(30), -- Imię jeśli firma prywatna
  waitingroom character varying(254),
  datamodify timestamp without time zone,
  dataakcept timestamp without time zone,
  systembazowy  character(3) NOT NULL,
  usuniety boolean,
  CONSTRAINT "KatFirmy_pkey" PRIMARY KEY (nip)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE public."KatFirmy"
  OWNER TO postgres;
COMMENT ON COLUMN public."KatFirmy".firma IS 'Identyfikator firmy : np. TFG, TFNI itp.';
COMMENT ON COLUMN public."KatFirmy".nazwa IS 'Nazwa, np. Top Farms GÂłubczyce';
COMMENT ON COLUMN public."KatFirmy".nazwaskrocona IS 'Nazwa skrĂłcona firmy uÂżywana w systemie PÂłatnik';
COMMENT ON COLUMN public."KatFirmy".ulica IS 'Ulica w adresie firmy';
COMMENT ON COLUMN public."KatFirmy".numerdomu IS 'Numer domu w adresie firmy';
COMMENT ON COLUMN public."KatFirmy".numerlokalu IS 'Numer lokalu w adresie firmy';
COMMENT ON COLUMN public."KatFirmy".miasto IS 'MiejscowoĹ“Ă¦ w adresie firmy';
COMMENT ON COLUMN public."KatFirmy".kodpocztowy IS 'Kod pocztowy w adresie firmy';
COMMENT ON COLUMN public."KatFirmy".poczta IS 'Poczta w adresie firmy';
COMMENT ON COLUMN public."KatFirmy".gmina IS 'Gmina w adresie firmy';
COMMENT ON COLUMN public."KatFirmy".powiat IS 'Powiat w adresie firmy';
COMMENT ON COLUMN public."KatFirmy".wojewodztwo IS 'WojewĂłdztwo w adresie firmy';
COMMENT ON COLUMN public."KatFirmy".nip IS 'Numer NIP firmy';
COMMENT ON COLUMN public."KatFirmy".regon IS 'Numer regon firmy';
COMMENT ON COLUMN public."KatFirmy".nazwa2 IS 'Druga część nazwy firmy';
COMMENT ON COLUMN public."KatFirmy".pesel IS 'Numer pesel wÂłaĹ“ciciela firmy, jeÂżeli firma jest jednoosobowa i jest to osoba fizyczna';
COMMENT ON COLUMN public."KatFirmy".idoper IS 'Idetyfikator osoby dokonujÂącej wpisu';
COMMENT ON COLUMN public."KatFirmy".idakcept IS 'Identyfikator osoby akceptujÂącej';
COMMENT ON COLUMN public."KatFirmy".nazwisko IS 'Nazwisko, jeżeli jest to firma prywatna';
COMMENT ON COLUMN public."KatFirmy".imie IS 'Imię jeśli firma prywatna';




-- Table: public."MiejscePracy"

-- DROP TABLE public."MiejscePracy";



CREATE TABLE "MiejscePracy"
(
  firma character(20), -- Symbol firmy
  rejon character(20), -- Symbol rejonu w ramach firmy
  wydzial character(20), -- Symbol dzia�1‚u
  podwydzial character(20) DEFAULT ''::bpchar, -- Symbol podwydzia�1‚u
  konto5 character(20), -- Symbol konta ksiA��cgowego
  datapocz character(20) NOT NULL, -- Data poczA�…tkowa w miejscu pracy
  datakoniec character(20), -- Data ko�1„cowa w miejscu pracy
  idoper character(30), -- ID operatora
  idakcept character(30), -- Identyfikator osoby akceptujA�…cej
  datamodify timestamp without time zone,
  dataakcept timestamp without time zone,
  numeread character(20) NOT NULL,
  systembazowy character(3) NOT NULL,
  usuniety boolean,
  id integer NOT NULL DEFAULT nextval('miejscepracy_id_seq'::regclass),
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
COMMENT ON COLUMN public."MiejscePracy".wydzial IS 'Symbol działu';
COMMENT ON COLUMN public."MiejscePracy".podwydzial IS 'Symbol podwydziału';
COMMENT ON COLUMN public."MiejscePracy".konto5 IS 'Symbol konta księgowego';
COMMENT ON COLUMN public."MiejscePracy".datapocz IS 'Data początkowa w miejscu pracy';
COMMENT ON COLUMN public."MiejscePracy".datakoniec IS 'Data końcowa w miejscu pracy';
COMMENT ON COLUMN public."MiejscePracy".idoper IS 'ID operatora';
COMMENT ON COLUMN public."MiejscePracy".idakcept IS 'Identyfikator osoby akceptującej';



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
  systembazowy character(3) NOT NULL,
  usuniety boolean,
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
  podwydzial character(20) NOT NULL, -- Podwydzia�
  nazwa character(150), -- Nazwa, np. Rachuba
  wydzial character(20) NOT NULL, -- Wydzia� do kt�rego nale�y podwydzia�
  datamodify timestamp without time zone, -- Data modyfikacji
  idoper character(30), -- Id uzytkownika
  idakcept character(30), -- Identyfikator osoby akcept
  dataakcept timestamp without time zone, -- Data akceptu przez idakcept
  firma character(20) NOT NULL, -- Firma klucz to : firma+wydzial+podwydzial
  systembazowy character(3) NOT NULL,
  usuniety boolean,
  CONSTRAINT "KatPodWydzial_pkey" PRIMARY KEY (firma, wydzial, podwydzial)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE public."KatPodWydzial"
  OWNER TO postgres;
COMMENT ON COLUMN public."KatPodWydzial".podwydzial IS 'Podwydzia�';
COMMENT ON COLUMN public."KatPodWydzial".nazwa IS 'Nazwa, np. Rachuba';
COMMENT ON COLUMN public."KatPodWydzial".wydzial IS 'Wydzia� do kt�rego nale�y podwydzia�';
COMMENT ON COLUMN public."KatPodWydzial".datamodify IS 'Data modyfikacji';
COMMENT ON COLUMN public."KatPodWydzial".idoper IS 'Id uzytkownika';
COMMENT ON COLUMN public."KatPodWydzial".idakcept IS 'Identyfikator osoby akcept';
COMMENT ON COLUMN public."KatPodWydzial".dataakcept IS 'Data akceptu przez idakcept';
COMMENT ON COLUMN public."KatPodWydzial".firma IS 'Firma klucz to : firma+wydzial+podwydzial';




-- Table: public."KatLokalPapier"

-- DROP TABLE public."KatLokalPapier";

CREATE TABLE public."KatLokalPapier"
(
  firma character(20) NOT NULL, -- Identyfikator firmy : np. TFG, TFNI itp. symbol z KatFirmy
  lokalpapier character(20) NOT NULL, -- Symbol archiwum : np. TFG_piwnica
  nazwa character varying(300), -- Nazwa, np. archiwum Top Farms GĂ‚Ĺ‚ubczyce
  ulica character(50), -- Ulica gdzie jest archiwum
  numerdomu character(10), -- Numer domu  gdzie jest archiwum
  numerlokalu character(10), -- Numer lokalu  gdzie jest archiwum
  miasto character(25), -- MiejscowoÄąâ€śÄ‚Â¦  gdzie jest archiwum
  kodpocztowy character(6), -- Kod pocztowy  gdzie jest archiwum
  poczta character(25), -- Poczta  gdzie jest archiwum
  idoper character(30), -- Idetyfikator osoby dokonujĂ‚Ä…cej wpisu
  idakcept character(30), -- Identyfikator osoby akceptujĂ‚Ä…cej
  datamodify timestamp without time zone,
  dataakcept timestamp without time zone,
  systembazowy  character(3) NOT NULL,
  usuniety boolean,
  CONSTRAINT "KatLokalPapier_pkey" PRIMARY KEY (firma, lokalpapier)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE public."KatLokalPapier"
  OWNER TO postgres;
COMMENT ON COLUMN public."KatLokalPapier".firma IS 'Identyfikator firmy : np. TFG, TFNI itp. symbol z KatFirmy';
COMMENT ON COLUMN public."KatLokalPapier".lokalpapier IS 'Symbol archiwum : np. TFG_piwnica';
COMMENT ON COLUMN public."KatLokalPapier".nazwa IS 'Nazwa, np. archiwum Top Farms GĂ‚Ĺ‚ubczyce';
COMMENT ON COLUMN public."KatLokalPapier".ulica IS 'Ulica gdzie jest archiwum';
COMMENT ON COLUMN public."KatLokalPapier".numerdomu IS 'Numer domu  gdzie jest archiwum';
COMMENT ON COLUMN public."KatLokalPapier".numerlokalu IS 'Numer lokalu  gdzie jest archiwum';
COMMENT ON COLUMN public."KatLokalPapier".miasto IS 'MiejscowoÄąâ€śÄ‚Â¦  gdzie jest archiwum';
COMMENT ON COLUMN public."KatLokalPapier".kodpocztowy IS 'Kod pocztowy  gdzie jest archiwum';
COMMENT ON COLUMN public."KatLokalPapier".poczta IS 'Poczta  gdzie jest archiwum';
COMMENT ON COLUMN public."KatLokalPapier".idoper IS 'Idetyfikator osoby dokonujĂ‚Ä…cej wpisu';
COMMENT ON COLUMN public."KatLokalPapier".idakcept IS 'Identyfikator osoby akceptujĂ‚Ä…cej';



-- Table: public."KatKonta5"

-- DROP TABLE public."KatKonta5";

CREATE TABLE public."KatKonta5"
(
  konto5 character(20) NOT NULL, -- Symbol konta księgowego
  nazwa character(150), -- Nazwa, np. Top Farms Głubczyce
  idoper character(30), -- Idetyfikator osoby dokonującej wpisu
  idakcept character(30), -- Identyfikator osoby akceptującej
  firma character(20) NOT NULL, -- Symbol firmy w której zdefiniowano konto
  kontoskr character(20) NOT NULL, -- Skrót konta
  datamodify timestamp without time zone,
  dataakcept timestamp without time zone,
  systembazowy  character(3) NOT NULL,
  usuniety boolean,
  CONSTRAINT "KatKonta5_pkey" PRIMARY KEY (firma, konto5)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE public."KatKonta5"
  OWNER TO postgres;
COMMENT ON COLUMN public."KatKonta5".konto5 IS 'Symbol konta księgowego';
COMMENT ON COLUMN public."KatKonta5".nazwa IS 'Nazwa, np. Top Farms Głubczyce';
COMMENT ON COLUMN public."KatKonta5".idoper IS 'Idetyfikator osoby dokonującej wpisu';
COMMENT ON COLUMN public."KatKonta5".idakcept IS 'Identyfikator osoby akceptującej';
COMMENT ON COLUMN public."KatKonta5".firma IS 'Symbol firmy w której zdefiniowano konto';
COMMENT ON COLUMN public."KatKonta5".kontoskr IS 'Skrót konta';



-- Table: public."KatLoginy"

-- DROP TABLE public."KatLoginy";

CREATE TABLE public."KatLoginy"
(
  identyfikator character(30) NOT NULL, -- Identyfikator - login
  hasloshort character(50), -- Has�o kr�tkie
  haslolong character(50), -- Has�o do logowania
  datamodify time without time zone,
  isadmin boolean,
  usuniety boolean,
  CONSTRAINT "KatLoginy_pkey" PRIMARY KEY (identyfikator)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE public."KatLoginy"
  OWNER TO postgres;
COMMENT ON TABLE public."KatLoginy"
  IS 'Katalog uÂżytkownikĂłw systemu';
COMMENT ON COLUMN public."KatLoginy".identyfikator IS 'Identyfikator - login';
COMMENT ON COLUMN public."KatLoginy".hasloshort IS 'Has�o kr�tkie';
COMMENT ON COLUMN public."KatLoginy".haslolong IS 'Has�o do logowania';



-- Table: public."KatLoginyDetale"

-- DROP TABLE public."KatLoginyDetale";

CREATE TABLE public."KatLoginyDetale"
(
  identyfikator character(30) NOT NULL,
  nazwisko character(40), -- Nazwisko u�ytkownika
  imie character(20), -- Imi� u�ytkownika
  firma character(20) NOT NULL,
  pocztaemail character(30), -- E-mail u�ytkownika na wszelki wypadek
  rolareadonly boolean,
  rolaaddpracownik boolean,
  rolamodifypracownik boolean,
  rolaaddfile boolean,
  rolamodifyfile boolean,
  rolaslowniki boolean,
  rolasendmail boolean,
  rolaraport boolean,
  rolaraportexport boolean,
  roladoubleakcept boolean,
  datamodify timestamp without time zone,
  usuniety boolean,
  confidential numeric(2,0), -- Poziom poufno�ci
  kodkierownik character(20), -- Kod kierownikado  kt�ego jest przypisanypracownik
  CONSTRAINT "KatLoginyDetale_pkey" PRIMARY KEY (identyfikator, firma)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE public."KatLoginyDetale"
  OWNER TO postgres;
COMMENT ON COLUMN public."KatLoginyDetale".confidential IS 'Poziom poufno�ci';
COMMENT ON COLUMN public."KatLoginyDetale".kodkierownik IS 'Kod kierownikado  kt�ego jest przypisanypracownik';
COMMENT ON COLUMN public."KatLoginyDetale".pocztaemail IS 'E-mail u�ytkownika na wszelki wypadek';

-- Table: public."KatDokumentyRodzaj"

-- DROP TABLE public."KatDokumentyRodzaj";

CREATE TABLE public."KatDokumentyRodzaj"
(
  symbol character(20) NOT NULL, -- Symbol dokumetu, np. SWPR - serwerawiadectwo pracy
  nazwa character(254), -- Nazwa dokumentu
  dokwlasny boolean, -- OkreÄąâ€şla czy dokument zostaÄąâ€š wytworzony przez nas czy jest to dokument obcy True=wÄąâ€šasny
  jrwa character(10), -- PeÄąâ€šna klasyfikacja JRWA
  teczkadzial character(10), -- CzĂ„â„˘Äąâ€şĂ„â€ˇ akt - dozwolone wartoÄąâ€şci : A,B,C
  typedycji character(2), -- OkreÄąâ€şla pola ktÄ‚Ĺ‚re majĂ„â€¦ byĂ„â€ˇ wymagane w edycji, np data dokumentu, data waÄąÄ˝noÄąâ€şci itp.
  idoper character(30),
  idakcept character(30),
  datamodify timestamp without time zone,
  dataakcept timestamp without time zone,
  systembazowy character(3) NOT NULL,
  usuniety boolean,
  confidential numeric(2,0), -- Poufnosc
  CONSTRAINT "KatDokumentyRodzaj_pkey" PRIMARY KEY (symbol)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE public."KatDokumentyRodzaj"
  OWNER TO postgres;
COMMENT ON TABLE public."KatDokumentyRodzaj"
  IS 'Slownik rodzajów, np. BDL - badania lekarskie, SWPR - swiadectwa pracy itp.';
COMMENT ON COLUMN public."KatDokumentyRodzaj".symbol IS 'Symbol dokumetu, np. SWPR - swiadectwo pracy';
COMMENT ON COLUMN public."KatDokumentyRodzaj".nazwa IS 'Nazwa dokumentu';
COMMENT ON COLUMN public."KatDokumentyRodzaj".dokwlasny IS 'OkreÄąâ€şla czy dokument zostal wytworzony przez nas czy jest to dokument obcy True=wlasnyasny';
COMMENT ON COLUMN public."KatDokumentyRodzaj".jrwa IS 'PeÄąâ€šna klasyfikacja JRWA';
COMMENT ON COLUMN public."KatDokumentyRodzaj".teczkadzial IS 'Czesc akt - dozwolone wartoscici : A,B,C';
COMMENT ON COLUMN public."KatDokumentyRodzaj".typedycji IS 'OkreÄąâ€şla pola ktÄ‚Ĺ‚re maja byc wymagane w edycji, np data dokumentu, data waznoci itp.';
COMMENT ON COLUMN public."KatDokumentyRodzaj".confidential IS 'Poufnosc';


-- Table: public."KatWydzial"

-- DROP TABLE public."KatWydzial";

CREATE TABLE public."KatWydzial"
(
  wydzial character(20) NOT NULL, -- Identyfikator Dzialu
  nazwa character(150), -- Nazwa dzialu
  datamodify timestamp without time zone, -- Data modyfikacji
  idoper character(30), -- Idetyfikator ochodzkiej
  idakcept character(30), -- Identyfikator osoby akcept
  dataakcept timestamp without time zone, -- Data akceptu przez idakcept
  firma character(20) NOT NULL, -- Firma
  systembazowy character(3) NOT NULL,
  usuniety boolean,
  CONSTRAINT "KatWydzial_pkey" PRIMARY KEY (firma, wydzial)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE public."KatWydzial"
  OWNER TO postgres;
COMMENT ON COLUMN public."KatWydzial".wydzial IS 'Identyfikator Dzialu';
COMMENT ON COLUMN public."KatWydzial".nazwa IS 'Nazwa dzialu ';
COMMENT ON COLUMN public."KatWydzial".datamodify IS 'Data modyfikacji';
COMMENT ON COLUMN public."KatWydzial".idoper IS 'Idetyfikator ochodzkiej';
COMMENT ON COLUMN public."KatWydzial".idakcept IS 'Identyfikator osoby akcept';
COMMENT ON COLUMN public."KatWydzial".dataakcept IS 'Data akceptu przez idakcept';
COMMENT ON COLUMN public."KatWydzial".firma IS 'Firma';



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
  dataakcept timestamp without time zone, -- Data akceptu przez idakcept
  systembazowy  character(3) NOT NULL,
  usuniety boolean,
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

-- Table: public."KatKategorieAkt"

-- DROP TABLE public."KatKategorieAkt";

CREATE TABLE public."KatKategorieAkt"
(
  id numeric(15,0) NOT NULL, -- Numer Id
  symbol character(20), -- kategorie typu A,B,BC,BE + ewentualne cyfry
  nazwa character(100), -- Nazwa kategorii
  idoper character(30), -- Identyfikator osoby dokonujÂącej wpisu
  datamodify timestamp without time zone, -- Data dokonania wpisu
  idakcept character(30), -- Identyfikator osoby akceptujÂącej
  dataakcept timestamp without time zone, -- Data akceptu przez idakcept
  systembazowy character(3) NOT NULL,
  usuniety boolean,
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
COMMENT ON COLUMN public."KatKategorieAkt".idoper IS 'Identyfikator osoby dokonujÂącej wpisu';
COMMENT ON COLUMN public."KatKategorieAkt".datamodify IS 'Data dokonania wpisu';
COMMENT ON COLUMN public."KatKategorieAkt".idakcept IS 'Identyfikator osoby akceptujÂącej';
COMMENT ON COLUMN public."KatKategorieAkt".dataakcept IS 'Data akceptu przez idakcept';


-- Table: public."KatPracownicy"

-- DROP TABLE public."KatPracownicy";

CREATE TABLE public."KatPracownicy"
(
  imie character varying(50) NOT NULL,
  nazwisko character varying(50) NOT NULL,
  pesel character varying(11) NOT NULL,
  numeread character(20) NOT NULL, -- ID w e-teczka
  kraj character(5) DEFAULT ''::bpchar, -- Kraj
  nazwiskorodowe character(50) DEFAULT ''::bpchar,
  imiematki character(30) DEFAULT ''::bpchar,
  imieojca character(30) DEFAULT ''::bpchar,
  peselinny character(20) DEFAULT ''::bpchar, -- Identyfikator dodatkowy, jeÅ¼eli nie ma numeru PESEL
  idoper character(30) DEFAULT ''::bpchar,
  idakcept character(30) DEFAULT ''::bpchar,
  datamodify timestamp without time zone,
  dataakcept timestamp without time zone,
  dataurodzenia character(20) NOT NULL,
  imie2 character varying(30) DEFAULT ''::character varying,
  systembazowy character(3) NOT NULL,
  usuniety boolean NOT NULL,
  kodkierownik character varying(300) DEFAULT ''::character varying, -- Lista kierownik�w kt�rzy s� uprawnieni do wgl�du w teczk� pracownika
  confidential numeric(2,0), -- Poufnosc
  CONSTRAINT "KatPracownicy_pkey" PRIMARY KEY (numeread)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE public."KatPracownicy"
  OWNER TO postgres;
COMMENT ON COLUMN public."KatPracownicy".numeread IS 'ID w e-teczka';
COMMENT ON COLUMN public."KatPracownicy".kraj IS 'Kraj';
COMMENT ON COLUMN public."KatPracownicy".peselinny IS 'Identyfikator dodatkowy, jeÅ¼eli nie ma numeru PESEL';
COMMENT ON COLUMN public."KatPracownicy".kodkierownik IS 'Lista kierownik�w kt�rzy s� uprawnieni do wgl�du w teczk� pracownika';
COMMENT ON COLUMN public."KatPracownicy".confidential IS 'Poufnosc';


-- Table: public."Pliki"

-- DROP TABLE public."Pliki";

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
  systembazowy character(3) NOT NULL,
  usuniety boolean,
  id integer NOT NULL DEFAULT nextval('pliki_id_seq'::regclass),
  firma character(20), -- Identyfikator firmy
  CONSTRAINT "Pliki_pkey" PRIMARY KEY (id)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE public."Pliki"
  OWNER TO postgres;
COMMENT ON COLUMN public."Pliki".datadokumentu IS 'Data wytworzenia papieru';
COMMENT ON COLUMN public."Pliki".firma IS 'Identyfikator firmy';

-- Table: public."SerwerSmtp"

-- DROP TABLE public."SerwerSmtp";

CREATE TABLE public."SerwerSmtp"
(
  smtpserwer character(100) NOT NULL, -- Nazwa serwera
  mailusername character(100), -- Np. kadry@poczta.pl
  mailpassword character(100), -- Has�o do poczty
  mailsender character(100), -- Np. kadry@poczta.pl
  mailsubject character varying(300), -- Naglowek
  mailbody character varying(300), -- Tresc maila
  pdfmasterpassword character(100), -- Has�o administratora do pdf
  datamodify timestamp without time zone,
  smtpport numeric(5,0), -- Numer portu
  CONSTRAINT "SerwerSmtp_pkey" PRIMARY KEY (smtpserwer)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE public."SerwerSmtp"
  OWNER TO postgres;
COMMENT ON COLUMN public."SerwerSmtp".smtpserwer IS 'Nazwa serwera';
COMMENT ON COLUMN public."SerwerSmtp".mailusername IS 'Np. kadry@poczta.pl';
COMMENT ON COLUMN public."SerwerSmtp".mailpassword IS 'Has�o do poczty';
COMMENT ON COLUMN public."SerwerSmtp".mailsender IS 'Np. kadry@poczta.pl';
COMMENT ON COLUMN public."SerwerSmtp".mailsubject IS 'Naglowek';
COMMENT ON COLUMN public."SerwerSmtp".mailbody IS 'Tresc maila';
COMMENT ON COLUMN public."SerwerSmtp".pdfmasterpassword IS 'Has�o administratora do pdf';
COMMENT ON COLUMN public."SerwerSmtp".smtpport IS 'Numer portu';



-- INSERT STATEMENTS

INSERT INTO "KatLoginy"(
            identyfikator, hasloshort, haslolong,
            datamodify, isadmin, usuniety)
    VALUES ('Administrator', 'admin', 'adminadmin',
            '2017-05-02 13:44:00', true, false);

INSERT INTO "KatLoginy"(
            identyfikator,  hasloshort, haslolong,
            datamodify, isadmin, usuniety)
    VALUES ('Z.Tokarz', 'ksglmp', 'ksglmpksglmp',
            '2017-05-02 13:44:00', false, false);

INSERT INTO "KatLoginy"(
            identyfikator,  hasloshort, haslolong,
            datamodify, isadmin, usuniety)
    VALUES ('M.Tokarz', 'haslo', 'haslohaslo',
            '2017-05-02 13:44:00', false, false);

INSERT INTO "KatLoginy"(
            identyfikator, hasloshort, haslolong,
            datamodify, isadmin, usuniety)
    VALUES ('A.Tokarz', 'haslo', 'haslohaslo',
            '2017-05-02 13:44:00', false, false);

INSERT INTO "KatLoginy"(
            identyfikator, hasloshort, haslolong,
            datamodify, isadmin, usuniety)
    VALUES ('M.Skalacki', 'haslo', 'haslohaslo',
            '2017-05-02 13:44:00', false, false);


---KatLoginyDetale


INSERT INTO "KatLoginyDetale"(
            identyfikator, nazwisko, imie, firma, pocztaemail, rolareadonly,
            rolaaddpracownik, rolamodifypracownik, rolaaddfile, rolamodifyfile,
            rolaslowniki, rolasendmail, rolaraport, rolaraportexport, roladoubleakcept,
            datamodify,  usuniety, confidential, kodkierownik)
    VALUES ('Administrator', 'Jarzyna', 'Krzysztof', 'AFM', 'poczta@poczta.pl',
            false, false, false, false, false,
            false, false, false, false, false,
            '2017-05-02 13:44:00',  false, 0, '');


INSERT INTO "KatLoginyDetale"(
            identyfikator,nazwisko, imie, firma, pocztaemail,rolareadonly,
            rolaaddpracownik, rolamodifypracownik, rolaaddfile, rolamodifyfile,
            rolaslowniki, rolasendmail, rolaraport, rolaraportexport, roladoubleakcept,
            datamodify,  usuniety, confidential, kodkierownik)
    VALUES ('Z.Tokarz', 'Tokarz', 'Zbigniew', 'AFM', 'poczta@poczta.pl',
            false, false, false, false, false,
            false, false, false, false, false,
            '2017-05-02 13:44:00',  false, 0, '');


INSERT INTO "KatLoginyDetale"(
            identyfikator,nazwisko, imie, firma, pocztaemail,rolareadonly,
            rolaaddpracownik, rolamodifypracownik, rolaaddfile, rolamodifyfile,
            rolaslowniki, rolasendmail, rolaraport, rolaraportexport, roladoubleakcept,
            datamodify,  usuniety, confidential, kodkierownik)
    VALUES ('M.Tokarz', 'Tokarz', 'Maciej', 'AFM', 'poczta@poczta.pl',
            false, false, false, false, false,
            false, false, false, false, false,
            '2017-05-02 13:44:00',  false, 0, '');

INSERT INTO "KatLoginyDetale"(
            identyfikator,nazwisko, imie, firma, pocztaemail,rolareadonly,
            rolaaddpracownik, rolamodifypracownik, rolaaddfile, rolamodifyfile,
            rolaslowniki, rolasendmail, rolaraport, rolaraportexport, roladoubleakcept,
            datamodify,  usuniety, confidential, kodkierownik)
    VALUES ('A.Tokarz', 'Tokarz', 'Aleksandra', 'AFM', 'poczta@poczta.pl',
            false, false, false, false, false,
            false, false, false, false, false,
            '2017-05-02 13:44:00',  false, 0, '');

INSERT INTO "KatLoginyDetale"(
            identyfikator,nazwisko, imie, firma, pocztaemail,rolareadonly,
            rolaaddpracownik, rolamodifypracownik, rolaaddfile, rolamodifyfile,
            rolaslowniki, rolasendmail, rolaraport, rolaraportexport, roladoubleakcept,
            datamodify,  usuniety, confidential, kodkierownik)
    VALUES ('M.Skalacki', 'SKalacki', 'Michal','AFM', 'poczta@poczta.pl',
            false, false, false, false, false,
            false, false, false, false, false,
            '2017-05-02 13:44:00',  false, 0, '');
