-- Table: public."KatLoginyDetale"

-- DROP TABLE public."KatLoginyDetale";

CREATE TABLE public."KatLoginyDetale"
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
  confidential numeric(2,0), -- Poziom poufnoœci
  kodkierownik character(20), -- Kod kierownikado  któego jest przypisanypracownik
  CONSTRAINT "KatLoginyDetale_pkey" PRIMARY KEY (identyfikator, firma)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE public."KatLoginyDetale"
  OWNER TO postgres;
COMMENT ON COLUMN public."KatLoginyDetale".confidential IS 'Poziom poufnoœci';
COMMENT ON COLUMN public."KatLoginyDetale".kodkierownik IS 'Kod kierownikado  któego jest przypisanypracownik';

