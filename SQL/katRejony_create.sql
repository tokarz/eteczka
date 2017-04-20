-- Table: public."KatRejony"

-- DROP TABLE public."KatRejony";

CREATE TABLE public."KatRejony"
(
  id numeric(20,0) NOT NULL, -- Numer ID
  symbol character(20), -- Identyfikator firmy : np. TFG, TFNI itp.
  nazwa character(150), -- Nazwa, np. Top Farms G³ubczyce
  nazwaskrocona character(30), -- Nazwa skrócona firmy u¿ywana w systemie P³atnik
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
  CONSTRAINT "KatRejony_pkey" PRIMARY KEY (id)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE public."KatRejony"
  OWNER TO postgres;
COMMENT ON COLUMN public."KatRejony".symbol IS 'Identyfikator Rejony : np. TFG, TFNI itp.';
COMMENT ON COLUMN public."KatRejony".nazwa IS 'Nazwa, np. Top Farms G³ubczyce';
COMMENT ON COLUMN public."KatRejony".nazwaskrocona IS 'Nazwa skrócona Rejony u¿ywana w systemie P³atnik';
COMMENT ON COLUMN public."KatRejony".ulica IS 'Ulica w adresie Rejony';
COMMENT ON COLUMN public."KatRejony".numerdomu IS 'Numer domu w adresie Rejony';
COMMENT ON COLUMN public."KatRejony".numerlokalu IS 'Numer lokalu w adresie Rejony';
COMMENT ON COLUMN public."KatRejony".miasto IS 'Miejscowoœæ w adresie Rejony';
COMMENT ON COLUMN public."KatRejony".kodpocztowy IS 'Kod pocztowy w adresie Rejony';
COMMENT ON COLUMN public."KatRejony".poczta IS 'Poczta w adresie Rejony';
COMMENT ON COLUMN public."KatRejony".gmina IS 'Gmina w adresie Rejony';
COMMENT ON COLUMN public."KatRejony".powiat IS 'Powiat w adresie Rejony';
COMMENT ON COLUMN public."KatRejony".wojewodztwo IS 'Województwo w adresie Rejony';
COMMENT ON COLUMN public."KatRejony".kraj IS 'Kraj w którym jest firma, np. PL lub Polska';
COMMENT ON COLUMN public."KatRejony".nip IS 'Numer NIP Rejony';
COMMENT ON COLUMN public."KatRejony".regon IS 'Numer regon Rejony';
COMMENT ON COLUMN public."KatRejony".krs IS 'Numer KRS Rejony';
COMMENT ON COLUMN public."KatRejony".pesel IS 'Numer pesel w³aœciciela Rejony, je¿eli firma jest jednoosobowa i jest to osoba fizyczna';
COMMENT ON COLUMN public."KatRejony".datamodify IS 'Data modyfikacji';
COMMENT ON COLUMN public."KatRejony".idoper IS 'Idetyfikator osoby dokonuj¹cej wpisu';
COMMENT ON COLUMN public."KatRejony".idakcept IS 'Identyfikator osoby akceptuj¹cej';
COMMENT ON COLUMN public."KatRejony".dataakcept IS 'Data akceptu przez idakcept';
COMMENT ON COLUMN public."KatRejony".id IS 'Numer ID';
COMMENT ON COLUMN public."KatRejony".lokalizacjapapier IS 'Lokalizacja dokumentów papierowych';

