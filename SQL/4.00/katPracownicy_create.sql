-- Table: public."KatPracownicy"

-- DROP TABLE public."KatPracownicy";

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
  peselinny character(20), -- Identyfikator dodatkowy, je¿eli nie ma numeru PESEL
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
COMMENT ON COLUMN public."KatPracownicy".peselinny IS 'Identyfikator dodatkowy, je¿eli nie ma numeru PESEL';

