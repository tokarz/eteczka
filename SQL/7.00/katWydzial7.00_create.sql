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

