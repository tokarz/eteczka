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

