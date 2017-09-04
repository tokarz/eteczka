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
  peselinny character(20) DEFAULT ''::bpchar, -- Identyfikator dodatkowy, jeÃ…Â¼eli nie ma numeru PESEL
  idoper character(30) DEFAULT ''::bpchar,
  idakcept character(30) DEFAULT ''::bpchar,
  datamodify timestamp without time zone,
  dataakcept timestamp without time zone,
  dataurodzenia character(20) NOT NULL,
  imie2 character varying(30) DEFAULT ''::character varying,
  systembazowy character(3) NOT NULL,
  usuniety boolean NOT NULL,
  kodkierownik character varying(300) DEFAULT ''::character varying, -- Lista kierowników którzy s¹ uprawnieni do wgl¹du w teczkê pracownika
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
COMMENT ON COLUMN public."KatPracownicy".peselinny IS 'Identyfikator dodatkowy, jeÃ…Â¼eli nie ma numeru PESEL';
COMMENT ON COLUMN public."KatPracownicy".kodkierownik IS 'Lista kierowników którzy s¹ uprawnieni do wgl¹du w teczkê pracownika';
COMMENT ON COLUMN public."KatPracownicy".confidential IS 'Poufnosc';

