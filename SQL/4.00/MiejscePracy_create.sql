-- Table: public."MiejscePracy"

-- DROP TABLE public."MiejscePracy";

CREATE TABLE public."MiejscePracy"
(
  firma character(20), -- Symbol firmy
  rejon character(20), -- Symbol rejonu w ramach firmy
  wydzial character(20), -- Symbol dzia�u
  podwydzial character(20), -- Symbol podwydzia�u
  konto5 character(20), -- Symbol konta ksi�gowego
  datapocz character(10) NOT NULL, -- Data pocz�tkowa w miejscu pracy
  datakoniec character(10), -- Data ko�cowa w miejscu pracy
  idoper character(30), -- ID operatora
  idakcept character(30), -- Identyfikator osoby akceptuj�cej
  datamodify timestamp without time zone,
  dataakcept timestamp without time zone,
  numeread character(20) NOT NULL,
  id integer NOT NULL DEFAULT nextval('"MiejscePracy_id_seq"'::regclass),
  CONSTRAINT "MiejscePracy_pkey" PRIMARY KEY (id)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE public."MiejscePracy"
  OWNER TO postgres;
COMMENT ON TABLE public."MiejscePracy"
  IS 'Tabela ';
COMMENT ON COLUMN public."MiejscePracy".firma IS 'Symbol firmy';
COMMENT ON COLUMN public."MiejscePracy".rejon IS 'Symbol rejonu w ramach firmy';
COMMENT ON COLUMN public."MiejscePracy".wydzial IS 'Symbol dzia�u';
COMMENT ON COLUMN public."MiejscePracy".podwydzial IS 'Symbol podwydzia�u';
COMMENT ON COLUMN public."MiejscePracy".konto5 IS 'Symbol konta ksi�gowego';
COMMENT ON COLUMN public."MiejscePracy".datapocz IS 'Data pocz�tkowa w miejscu pracy';
COMMENT ON COLUMN public."MiejscePracy".datakoniec IS 'Data ko�cowa w miejscu pracy';
COMMENT ON COLUMN public."MiejscePracy".idoper IS 'ID operatora';
COMMENT ON COLUMN public."MiejscePracy".idakcept IS 'Identyfikator osoby akceptuj�cej';

