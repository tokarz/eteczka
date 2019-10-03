-- Database: "E-Agropin-EAD"
-- DROP DATABASE IF EXISTS "E-Agropin-EAD"; 

  -- TWORZYMY UZYTKOWNIKOW DLA SYSTEMU
  DROP ROLE IF EXISTS eadadmin;
  CREATE ROLE eadadmin LOGIN
  UNENCRYPTED PASSWORD 'f6fdffe48c908deb0f4c3bd36c032e72'
  SUPERUSER INHERIT CREATEDB CREATEROLE REPLICATION;

  DROP ROLE IF EXISTS ead;
  CREATE ROLE ead LOGIN
  UNENCRYPTED PASSWORD '1c76fe13b0ac4d8caffb0b39578f2e78';

  CREATE DATABASE "E-Agropin-EAD"
   WITH OWNER = eadadmin
       ENCODING = 'UTF8'
       TABLESPACE = pg_default
       CONNECTION LIMIT = -1;
  ALTER DEFAULT PRIVILEGES
    GRANT INSERT, SELECT, UPDATE, DELETE, TRUNCATE, REFERENCES, TRIGGER ON TABLES
    TO ead;

  \connect E-Agropin-EAD


  -- Table: "DbInfo"

-- DROP TABLE "DbInfo";

CREATE TABLE "DbInfo"
(
  wersja character(4) NOT NULL
)
WITH (
  OIDS=FALSE
);
ALTER TABLE "DbInfo"
  OWNER TO eadadmin;
  GRANT ALL ON TABLE "DbInfo" TO ead;


  CREATE SEQUENCE MiejscePracy_id_seq;
  GRANT USAGE ON SEQUENCE MiejscePracy_id_seq TO ead;

 -- Table: public."KatFirmy"

-- DROP TABLE public."KatFirmy";

CREATE TABLE public."KatFirmy"
(
  firma character(20) NOT NULL, -- Identyfikator firmy : np. TFG, TFNI itp.
  nazwa character varying(300), -- Nazwa, np. Top Farms Glubczyce
  nazwaskrocona character(150), -- Nazwa skrocona firmy uzywana w systemie Platnik
  ulica character(50), -- Ulica w adresie firmy
  numerdomu character(10), -- Numer domu w adresie firmy
  numerlokalu character(10), -- Numer lokalu w adresie firmy
  miasto character(25), -- Miejscowosc w adresie firmy
  kodpocztowy character(6), -- Kod pocztowy w adresie firmy
  poczta character(25), -- Poczta w adresie firmy
  gmina character(25), -- Gmina w adresie firmy
  powiat character(25), -- Powiat w adresie firmy
  wojewodztwo character(25), -- Wojewodztwo w adresie firmy
  nip character(10) NOT NULL, -- Numer NIP firmy
  regon character(20), -- Numer regon firmy
  nazwa2 character varying(300), -- Druga czesc nazwy firmy
  pesel character(11), -- Numer pesel wlasciciela firmy, jezeli firma jest jednoosobowa i jest to osoba fizyczna
  idoper character(30), -- Idetyfikator osoby dokonujacej wpisu
  idakcept character(30), -- Identyfikator osoby akceptujacej
  nazwisko character(30), -- Nazwisko, jezeli jest to firma prywatna
  imie character(30), -- Imie jezeli firma prywatna
  datamodify timestamp without time zone,
  dataakcept timestamp without time zone,
  systembazowy character(3) NOT NULL,
  usuniety boolean,
  waitingroom character(254) DEFAULT ''::bpchar, -- Zakrystia dla skanowanych plikow
  CONSTRAINT "KatFirmy_pkey" PRIMARY KEY (nip)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE public."KatFirmy"
    OWNER TO eadadmin;
    GRANT ALL ON TABLE "KatFirmy" TO ead;

COMMENT ON COLUMN public."KatFirmy".firma IS 'Identyfikator firmy : np. TFG, TFNI itp.';
COMMENT ON COLUMN public."KatFirmy".nazwa IS 'Nazwa, np. Top Farms Glubczyce';
COMMENT ON COLUMN public."KatFirmy".nazwaskrocona IS 'Nazwa skrocona firmy uzywana w systemie Platnik';
COMMENT ON COLUMN public."KatFirmy".ulica IS 'Ulica w adresie firmy';
COMMENT ON COLUMN public."KatFirmy".numerdomu IS 'Numer domu w adresie firmy';
COMMENT ON COLUMN public."KatFirmy".numerlokalu IS 'Numer lokalu w adresie firmy';
COMMENT ON COLUMN public."KatFirmy".miasto IS 'Miejscowosc w adresie firmy';
COMMENT ON COLUMN public."KatFirmy".kodpocztowy IS 'Kod pocztowy w adresie firmy';
COMMENT ON COLUMN public."KatFirmy".poczta IS 'Poczta w adresie firmy';
COMMENT ON COLUMN public."KatFirmy".gmina IS 'Gmina w adresie firmy';
COMMENT ON COLUMN public."KatFirmy".powiat IS 'Powiat w adresie firmy';
COMMENT ON COLUMN public."KatFirmy".wojewodztwo IS 'Wojewodztwo w adresie firmy';
COMMENT ON COLUMN public."KatFirmy".nip IS 'Numer NIP firmy';
COMMENT ON COLUMN public."KatFirmy".regon IS 'Numer regon firmy';
COMMENT ON COLUMN public."KatFirmy".nazwa2 IS 'Druga czesc nazwy firmy';
COMMENT ON COLUMN public."KatFirmy".pesel IS 'Numer pesel wlasciciela firmy, jezeli firma jest jednoosobowa i jest to osoba fizyczna';
COMMENT ON COLUMN public."KatFirmy".idoper IS 'Idetyfikator osoby dokonujacej wpisu';
COMMENT ON COLUMN public."KatFirmy".idakcept IS 'Identyfikator osoby akceptujacej';
COMMENT ON COLUMN public."KatFirmy".nazwisko IS 'Nazwisko, jezeli jest to firma prywatna';
COMMENT ON COLUMN public."KatFirmy".imie IS 'Imie jezeli firma prywatna';
COMMENT ON COLUMN public."KatFirmy".waitingroom IS 'Zakrystia dla skanowanych plikow';



-- Table: public."MiejscePracy"

-- DROP TABLE public."MiejscePracy";



CREATE TABLE "MiejscePracy"
(
  firma character(20), -- Symbol firmy
  rejon character(20), -- Symbol rejonu w ramach firmy
  wydzial character(20), -- Symbol dzialu
  podwydzial character(20) DEFAULT ''::bpchar, -- Symbol podwydzialu
  konto5 character(20), -- Symbol konta ksiegowego
  datapocz character(10) NOT NULL, -- Data poczatkowa w miejscu pracy
  datakoniec character(10), -- Data koncowa w miejscu pracy
  idoper character(30), -- ID operatora
  idakcept character(30), -- Identyfikator osoby akceptujacej
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
    OWNER TO eadadmin;
    GRANT ALL ON TABLE "MiejscePracy" TO ead;

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
  OWNER TO eadadmin;
    GRANT ALL ON TABLE "KatRejony" TO ead;
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
    OWNER TO eadadmin;
    GRANT ALL ON TABLE "KatPodWydzial" TO ead;

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
      OWNER TO eadadmin;
    GRANT ALL ON TABLE "KatLokalPapier" TO ead;

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
    OWNER TO eadadmin;
    GRANT ALL ON TABLE "KatKonta5" TO ead;

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
  datamodify timestamp without time zone,
  isadmin boolean,
  usuniety boolean,
  CONSTRAINT "KatLoginy_pkey" PRIMARY KEY (identyfikator)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE public."KatLoginy"
    OWNER TO eadadmin;
    GRANT ALL ON TABLE "KatLoginy" TO ead;

COMMENT ON TABLE public."KatLoginy"
  IS 'Katalog uÂżytkownikĂłw systemu';
COMMENT ON COLUMN public."KatLoginy".identyfikator IS 'Identyfikator - login';
COMMENT ON COLUMN public."KatLoginy".hasloshort IS 'Has�o kr�tkie';
COMMENT ON COLUMN public."KatLoginy".haslolong IS 'Has�o do logowania';


-- Table: public."KatLoginyFirmy"

-- DROP TABLE public."KatLoginyFirmy";

CREATE TABLE public."KatLoginyFirmy"
(
  identyfikator character(30) NOT NULL,
  firma character(20) NOT NULL,
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
  confidential numeric(2,0), -- Poziom poufności
  kodkierownik character(20), -- Kod kierownikado  któego jest przypisanypracownik
  CONSTRAINT "KatLoginyFirmy_pkey" PRIMARY KEY (identyfikator, firma)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE public."KatLoginyFirmy"
  OWNER TO eadadmin;
GRANT ALL ON TABLE public."KatLoginyFirmy" TO eadadmin;
GRANT ALL ON TABLE public."KatLoginyFirmy" TO ead;
COMMENT ON COLUMN public."KatLoginyFirmy".confidential IS 'Poziom poufności';
COMMENT ON COLUMN public."KatLoginyFirmy".kodkierownik IS 'Kod kierownikado  któego jest przypisanypracownik';




-- Table: public."KatLoginyDetale"

-- DROP TABLE public."KatLoginyDetale";

CREATE TABLE public."KatLoginyDetale"
(
  identyfikator character varying(30) NOT NULL,
  nazwisko character varying(40),
  imie character varying(20),
  pocztaemail character varying(30),
  CONSTRAINT "KatLoginyDetale_pkey" PRIMARY KEY (identyfikator)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE public."KatLoginyDetale"
  OWNER TO eadadmin;
GRANT ALL ON TABLE public."KatLoginyDetale" TO eadadmin;
GRANT ALL ON TABLE public."KatLoginyDetale" TO ead;

-- Table: public."KatDokumentyRodzaj"

-- DROP TABLE public."KatDokumentyRodzaj";

CREATE TABLE public."KatDokumentyRodzaj"
(
  symbol character(20) NOT NULL, -- Symbol dokumetu, np. SWPR - swiadectwo pracy
  nazwa character(254), -- Nazwa dokumentu
  dokwlasny boolean, -- Okresla czy dokument zostal wytworzony przez nas True, dokument obcy False
  jrwa character(10), -- Pelna klasyfikacja JRWA
  teczkadzial character(1), -- Czesc akt - dozwolone wartoscici : A,B,C
  typedycji character(2), -- Okresla pola ktore maja byc wymagane w edycji, np data dokumentu, data waznoci itp.
  idoper character(30),
  idakcept character(30),
  datamodify timestamp without time zone,
  dataakcept timestamp without time zone,
  systembazowy character(3) NOT NULL,
  usuniety boolean,
  confidential numeric(2,0), -- Poufnosc
  symbolead character(25) NOT NULL, -- Symbol w eAD
  audyt boolean DEFAULT false, -- Czy podlega audytowi
  CONSTRAINT "KatDokumentyRodzaj_pkey" PRIMARY KEY (symbolead)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE public."KatDokumentyRodzaj"
  OWNER TO eadadmin;
GRANT ALL ON TABLE public."KatDokumentyRodzaj" TO eadadmin;
GRANT ALL ON TABLE public."KatDokumentyRodzaj" TO ead;
COMMENT ON TABLE public."KatDokumentyRodzaj"
  IS 'Slownik rodzajĂłw, np. BDL - badania lekarskie, SWPR - swiadectwa pracy itp.';
COMMENT ON COLUMN public."KatDokumentyRodzaj".symbol IS 'Symbol dokumetu, np. SWPR - swiadectwo pracy';
COMMENT ON COLUMN public."KatDokumentyRodzaj".nazwa IS 'Nazwa dokumentu';
COMMENT ON COLUMN public."KatDokumentyRodzaj".dokwlasny IS 'Okresla czy dokument zostal wytworzony przez nas True, dokument obcy False';
COMMENT ON COLUMN public."KatDokumentyRodzaj".jrwa IS 'Pelna klasyfikacja JRWA';
COMMENT ON COLUMN public."KatDokumentyRodzaj".teczkadzial IS 'Czesc akt - dozwolone wartoscici : A,B,C';
COMMENT ON COLUMN public."KatDokumentyRodzaj".typedycji IS 'Okresla pola ktore maja byc wymagane w edycji, np data dokumentu, data waznoci itp.';
COMMENT ON COLUMN public."KatDokumentyRodzaj".confidential IS 'Poufnosc';
COMMENT ON COLUMN public."KatDokumentyRodzaj".symbolead IS 'Symbol w eAD';
COMMENT ON COLUMN public."KatDokumentyRodzaj".audyt IS 'Czy podlega audytowi';




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
  OWNER TO eadadmin;
    GRANT ALL ON TABLE "KatWydzial" TO ead;

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
  OWNER TO eadadmin;
    GRANT ALL ON TABLE "KatJrwa" TO ead;

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
  OWNER TO eadadmin;
    GRANT ALL ON TABLE "KatKategorieAkt" TO ead;

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
  peselinny character(20) DEFAULT ''::bpchar, -- Identyfikator dodatkowy, jezeli nie ma numeru PESEL
  idoper character(30) DEFAULT ''::bpchar,
  idakcept character(30) DEFAULT ''::bpchar,
  datamodify timestamp without time zone,
  dataakcept timestamp without time zone,
  dataurodzenia character(10) NOT NULL,
  imie2 character varying(30) DEFAULT ''::character varying,
  systembazowy character(3) NOT NULL,
  usuniety boolean NOT NULL,
  kodkierownik character varying(300) DEFAULT ''::character varying, -- Lista kierownik�w kt�rzy s� uprawnieni do wgl�du w teczk� pracownika
  confidential numeric(2,0) DEFAULT 0, -- Poufnosc
  CONSTRAINT "KatPracownicy_pkey" PRIMARY KEY (numeread)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE public."KatPracownicy"
  OWNER TO eadadmin;
    GRANT ALL ON TABLE "KatPracownicy" TO ead;

COMMENT ON COLUMN public."KatPracownicy".numeread IS 'ID w e-teczka';
COMMENT ON COLUMN public."KatPracownicy".kraj IS 'Kraj';
COMMENT ON COLUMN public."KatPracownicy".peselinny IS 'Identyfikator dodatkowy, jezeli nie ma numeru PESEL';
COMMENT ON COLUMN public."KatPracownicy".kodkierownik IS 'Lista kierownik�w kt�rzy s� uprawnieni do wgl�du w teczk� pracownika';
COMMENT ON COLUMN public."KatPracownicy".confidential IS 'Poufnosc';



-- Table: public."Pliki"

-- DROP TABLE public."Pliki";

CREATE SEQUENCE Pliki_id_seq;
GRANT USAGE ON SEQUENCE Pliki_id_seq TO ead;


CREATE TABLE public."Pliki"
(
  firma character(20) NOT NULL, -- np. TFW
  numeread character(20) NOT NULL, -- numeread
  symbol character(20) NOT NULL, -- symbol dokumentu z KatDokumentyRodzaj
  dataskanu character(10), -- 2016-09-28
  datadokumentu character(10), -- 1985-10-17
  datapocz character(10), -- 1984-10-30
  datakoniec character(10), -- 2017-09-13
  nazwascan character(254), -- Nazwa oryginalu po zeskanowaniu
  nazwaead character(254), -- Nazwa po opisaniu i przesunieciu do folderu pliki
  pelnasciezkaead character(254), -- Pelna sciezka do pliku w folderze pliki
  typpliku character(10), -- PDF, JPG itp.
  opisdodatkowy character(254), -- Wlasny, dowolny opis wprowadzony przez Kozel
  dokwlasny boolean, -- Dokument nasz lub obcy
  systembazowy character(3) DEFAULT 'EAD'::bpchar, -- zawsze EAD
  usuniety boolean DEFAULT false,
  idoper character(30),
  idakcept character(30),
  datamodify timestamp without time zone,
  dataakcept timestamp without time zone NOT NULL,
  id integer NOT NULL DEFAULT nextval('pliki_id_seq'::regclass),
  teczkadzial character(1), -- A, B, lub C wedlug KatDokumentyRodzaj
  symbolead character(25), -- Symbol eAD
  nrdokumentu numeric(5,0) NOT NULL DEFAULT 0,
  datadodania character(10),
  CONSTRAINT "Pliki_pkey" PRIMARY KEY (id)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE public."Pliki"
  OWNER TO eadadmin;
GRANT ALL ON TABLE public."Pliki" TO eadadmin;
GRANT ALL ON TABLE public."Pliki" TO ead;
COMMENT ON COLUMN public."Pliki".firma IS 'np. TFW';
COMMENT ON COLUMN public."Pliki".numeread IS 'numeread';
COMMENT ON COLUMN public."Pliki".symbol IS 'symbol dokumentu z KatDokumentyRodzaj';
COMMENT ON COLUMN public."Pliki".dataskanu IS '2016-09-28';
COMMENT ON COLUMN public."Pliki".datadokumentu IS '1985-10-17';
COMMENT ON COLUMN public."Pliki".datapocz IS '1984-10-30';
COMMENT ON COLUMN public."Pliki".datakoniec IS '2017-09-13';
COMMENT ON COLUMN public."Pliki".nazwascan IS 'Nazwa oryginalu po zeskanowaniu';
COMMENT ON COLUMN public."Pliki".nazwaead IS 'Nazwa po opisaniu i przesunieciu do folderu pliki';
COMMENT ON COLUMN public."Pliki".pelnasciezkaead IS 'Pelna sciezka do pliku w folderze pliki';
COMMENT ON COLUMN public."Pliki".typpliku IS 'PDF, JPG itp.';
COMMENT ON COLUMN public."Pliki".opisdodatkowy IS 'Wlasny, dowolny opis wprowadzony przez Kozel';
COMMENT ON COLUMN public."Pliki".dokwlasny IS 'Dokument nasz lub obcy';
COMMENT ON COLUMN public."Pliki".systembazowy IS 'zawsze EAD';
COMMENT ON COLUMN public."Pliki".teczkadzial IS 'A, B, lub C wedlug KatDokumentyRodzaj';
COMMENT ON COLUMN public."Pliki".symbolead IS 'Symbol eAD';




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
      OWNER TO eadadmin;
    GRANT ALL ON TABLE "SerwerSmtp" TO ead;

COMMENT ON COLUMN public."SerwerSmtp".smtpserwer IS 'Nazwa serwera';
COMMENT ON COLUMN public."SerwerSmtp".mailusername IS 'Np. kadry@poczta.pl';
COMMENT ON COLUMN public."SerwerSmtp".mailpassword IS 'Has�o do poczty';
COMMENT ON COLUMN public."SerwerSmtp".mailsender IS 'Np. kadry@poczta.pl';
COMMENT ON COLUMN public."SerwerSmtp".mailsubject IS 'Naglowek';
COMMENT ON COLUMN public."SerwerSmtp".mailbody IS 'Tresc maila';
COMMENT ON COLUMN public."SerwerSmtp".pdfmasterpassword IS 'Has�o administratora do pdf';
COMMENT ON COLUMN public."SerwerSmtp".smtpport IS 'Numer portu';


-- Table: public."Koszyk"

-- DROP TABLE public."Koszyk";

CREATE SEQUENCE Koszyk_id_seq;
GRANT USAGE ON SEQUENCE Koszyk_id_seq TO ead;

CREATE TABLE public."Koszyk"
(
  identyfikator character(30), -- Identyfikator z KatLoginy
  firma character(20), -- Firma której koszyk dotyczy
  idpliki bigint, -- Polaczenie z tabela Pliki. IdPliki to Id z tabeli pliki
  id bigint NOT NULL DEFAULT nextval('koszyk_id_seq'::regclass),
  CONSTRAINT "Koszyk_pkey" PRIMARY KEY (id)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE public."Koszyk"
  OWNER TO eadadmin;
  GRANT ALL ON TABLE "Koszyk" TO ead;
COMMENT ON COLUMN public."Koszyk".identyfikator IS 'Identyfikator z KatLoginy';
COMMENT ON COLUMN public."Koszyk".firma IS 'Firma której koszyk dotyczy';
COMMENT ON COLUMN public."Koszyk".idpliki IS 'Polaczenie z tabela Pliki. IdPliki to Id z tabeli pliki';



-- INSERT STATEMENTS

INSERT INTO "DbInfo"(wersja)
    VALUES ('9.30');


INSERT INTO "KatLoginy"(
            identyfikator, hasloshort, haslolong,
            datamodify, isadmin, usuniety)
    VALUES ('Administrator', '21232f297a57a5a743894a0e4a801fc3', 'f6fdffe48c908deb0f4c3bd36c032e72',
            '2017-05-02 13:44:00', true, false);

INSERT INTO "KatLoginy"(
            identyfikator,  hasloshort, haslolong,
            datamodify, isadmin, usuniety)
    VALUES ('isremska', 'cdbcfd094509576f1365b3bdcc0b4fe7', 'cdbcfd094509576f1365b3bdcc0b4fe7',
            '2017-05-02 13:44:00', false, false);

INSERT INTO "KatLoginy"(
            identyfikator,  hasloshort, haslolong,
            datamodify, isadmin, usuniety)
    VALUES ('lszwarc', '87e267df147c2ff89ae43ac739795960', '87e267df147c2ff89ae43ac739795960',
            '2017-05-02 13:44:00', false, false);

INSERT INTO "KatLoginy"(
            identyfikator, hasloshort, haslolong,
            datamodify, isadmin, usuniety)
    VALUES ('Agropin', 'b0ac5e33cf4cc3e0a2eae5eb43d9e5fe', 'b0ac5e33cf4cc3e0a2eae5eb43d9e5fe',
            '2017-05-02 13:44:00', false, false);


---KatLoginyDetale
		INSERT INTO public."KatLoginyDetale"(
            identyfikator, nazwisko, imie, pocztaemail)
    VALUES ('Administrator', 'Admin', 'TF', 'poczta@poczta.pl');
	---KatLoginyDetale
		INSERT INTO public."KatLoginyDetale"(
            identyfikator, nazwisko, imie, pocztaemail)
    VALUES ('isremska', '�remska', 'Izabella', 'paszcz@poczta.pl');
	---KatLoginyDetale
		INSERT INTO public."KatLoginyDetale"(
            identyfikator, nazwisko, imie, pocztaemail)
    VALUES ('lszwarc', 'Szwarc', 'Lilianna', 'burqin@poczta.pl');
				
			
---KatLoginyFirmy


INSERT INTO "KatLoginyFirmy"(
            identyfikator,firma, rolareadonly,
            rolaaddpracownik, rolamodifypracownik, rolaaddfile, rolamodifyfile,
            rolaslowniki, rolasendmail, rolaraport, rolaraportexport, roladoubleakcept,
            datamodify,  usuniety, confidential, kodkierownik)
    VALUES ('isremska', 'TopGen',
            false, false, false, false, false,
            false, false, false, false, false,
            '2017-05-02 13:44:00',  false, 10, '');


INSERT INTO "KatLoginyFirmy"(
            identyfikator, firma ,rolareadonly,
            rolaaddpracownik, rolamodifypracownik, rolaaddfile, rolamodifyfile,
            rolaslowniki, rolasendmail, rolaraport, rolaraportexport, roladoubleakcept,
            datamodify,  usuniety, confidential, kodkierownik)
    VALUES ('lszwarc', 'TopGen',
            false, false, false, false, false,
            false, false, false, false, false,
            '2017-05-02 13:44:00',  false, 10, '');

INSERT INTO "KatLoginyFirmy"(
            identyfikator, firma,rolareadonly,
            rolaaddpracownik, rolamodifypracownik, rolaaddfile, rolamodifyfile,
            rolaslowniki, rolasendmail, rolaraport, rolaraportexport, roladoubleakcept,
            datamodify,  usuniety, confidential, kodkierownik)
    VALUES ('Agropin', 'TopGen', 
            false, false, false, false, false,
            false, false, false, false, false,
            '2017-05-02 13:44:00',  false, 10, '');


            --DRUGA FIRMA

