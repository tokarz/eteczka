-- Table: public."KatFirmy"

-- DROP TABLE public."KatFirmy";

CREATE TABLE public."KatFirmy"
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
ALTER TABLE public."KatFirmy"
  OWNER TO postgres;
COMMENT ON COLUMN public."KatFirmy".id IS 'Numer ID';
COMMENT ON COLUMN public."KatFirmy".symbol IS 'Identyfikator firmy : np. TFG, TFNI itp.';
COMMENT ON COLUMN public."KatFirmy".nazwa IS 'Nazwa, np. Top Farms G³ubczyce';
COMMENT ON COLUMN public."KatFirmy".nazwaskrocona IS 'Nazwa skrócona firmy u¿ywana w systemie P³atnik';
COMMENT ON COLUMN public."KatFirmy".ulica IS 'Ulica w adresie firmy';
COMMENT ON COLUMN public."KatFirmy".numerdomu IS 'Numer domu w adresie firmy';
COMMENT ON COLUMN public."KatFirmy".numerlokalu IS 'Numer lokalu w adresie firmy';
COMMENT ON COLUMN public."KatFirmy".miasto IS 'Miejscowoœæ w adresie firmy';
COMMENT ON COLUMN public."KatFirmy".kodpocztowy IS 'Kod pocztowy w adresie firmy';
COMMENT ON COLUMN public."KatFirmy".poczta IS 'Poczta w adresie firmy';
COMMENT ON COLUMN public."KatFirmy".gmina IS 'Gmina w adresie firmy';
COMMENT ON COLUMN public."KatFirmy".powiat IS 'Powiat w adresie firmy';
COMMENT ON COLUMN public."KatFirmy".wojewodztwo IS 'Województwo w adresie firmy';
COMMENT ON COLUMN public."KatFirmy".kraj IS 'Kraj w którym jest firma, np. PL lub Polska';
COMMENT ON COLUMN public."KatFirmy".nip IS 'Numer NIP firmy';
COMMENT ON COLUMN public."KatFirmy".regon IS 'Numer regon firmy';
COMMENT ON COLUMN public."KatFirmy".krs IS 'Numer KRS firmy';
COMMENT ON COLUMN public."KatFirmy".pesel IS 'Numer pesel w³aœciciela firmy, je¿eli firma jest jednoosobowa i jest to osoba fizyczna';
COMMENT ON COLUMN public."KatFirmy".datamodify IS 'Data modyfikacji';
COMMENT ON COLUMN public."KatFirmy".idoper IS 'Idetyfikator osoby dokonuj¹cej wpisu';
COMMENT ON COLUMN public."KatFirmy".idakcept IS 'Identyfikator osoby akceptuj¹cej';
COMMENT ON COLUMN public."KatFirmy".dataakcept IS 'Data akceptu przez idakcept';
COMMENT ON COLUMN public."KatFirmy".lokalizacjapapier IS 'Lokalizacja dokumentów papierowych';

