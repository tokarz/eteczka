-- Table: public."Koszyk"

-- DROP TABLE public."Koszyk";

CREATE SEQUENCE Koszyk_id_seq;

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
  OWNER TO postgres;
COMMENT ON COLUMN public."Koszyk".identyfikator IS 'Identyfikator z KatLoginy';
COMMENT ON COLUMN public."Koszyk".firma IS 'Firma której koszyk dotyczy';
COMMENT ON COLUMN public."Koszyk".idpliki IS 'Polaczenie z tabela Pliki. IdPliki to Id z tabeli pliki';

