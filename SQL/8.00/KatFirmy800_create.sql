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
  OWNER TO postgres;
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

